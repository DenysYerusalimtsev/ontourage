using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Linq;

namespace Ontourage.Web.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly ITourOperatorRepository _tourOperatorRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPaymentChecksRepository _paymentChecksRepository;

        public VoucherController(IVoucherRepository voucherRepository,
            IFoodTypeRepository foodTypeRepository,
            ICountryRepository countryRepository,
            IHotelRepository hotelRepository,
            ITourOperatorRepository tourOperatorRepository,
            IClientRepository clientRepository,
            IPaymentChecksRepository paymentChecksRepository)
        {
            _voucherRepository = voucherRepository;
            _foodTypeRepository = foodTypeRepository;
            _countryRepository = countryRepository;
            _hotelRepository = hotelRepository;
            _tourOperatorRepository = tourOperatorRepository;
            _clientRepository = clientRepository;
            _paymentChecksRepository = paymentChecksRepository;
        }

        [HttpGet]
        public IActionResult AddVoucher()
        {
            var model = new VoucherViewModel
            {
                Header = new HeaderViewModel("Добавление тура", "AddVoucher"),
                FoodTypes = _foodTypeRepository.GetAllFoodTypes(),
                Countries = _countryRepository.GetAllCoutries(),
                Hotels = _hotelRepository.GetAllHotels(),
                TourOperators = _tourOperatorRepository.GetAllTourOperators()
            };
            return View("AddEditVoucher", model);
        }

        [HttpPost]
        public IActionResult AddVoucher(VoucherViewModel addModel)
        {
            if (ModelState.IsValid)
            {
                Voucher voucher = addModel.CreateFromViewModel();
                _voucherRepository.AddVoucher(voucher);
            }
            return RedirectToAction("GetAllVouchers");
        }

        [HttpGet]
        public IActionResult EditVoucher(int id)
        {
            var voucherToEdit = _voucherRepository.GetVoucherById(id);
            var model = new VoucherViewModel
            {
                Header = new HeaderViewModel("Редактирование тура", "EditVoucher"),
                Hotels = _hotelRepository.GetAllHotels(),
                Countries = _countryRepository.GetAllCoutries(),
                FoodTypes = _foodTypeRepository.GetAllFoodTypes(),
                TourOperators = _tourOperatorRepository.GetAllTourOperators()
            };
            model.BindFromModel(voucherToEdit);
            return View("AddEditVoucher", model);
        }

        [HttpPost]
        public IActionResult EditVoucher(VoucherViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                Voucher voucher = editModel.CreateFromViewModel();
                _voucherRepository.EditVoucher(voucher);
                return RedirectToAction("GetAllVouchers");
            }
            return RedirectToAction("EditVoucher");
        }

        [HttpGet]
        public IActionResult DeleteVoucher(int id)
        {
            _voucherRepository.DeleteVoucher(id);
            return RedirectToAction("GetAllVouchers");
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var voucherToDetails = _voucherRepository.GetVoucherById(id);
            var model = new VoucherAggregateViewModel(voucherToDetails);
            model.BindFromModel(voucherToDetails);
            return View("ViewDetails", model);
        }

        [HttpGet]
        public IActionResult BuyVoucher(int id)
        {
            var voucherToBuy = _voucherRepository.GetVoucherById(id);
            var buyVoucherModel = new BuyVoucherViewModel
            {
                Clients = _clientRepository.GetAllClients(),
                CountFreeVouchers = voucherToBuy.CountFreeVouchers,
            };
            buyVoucherModel.BindFromModel(voucherToBuy);
            return View("BuyVoucher", buyVoucherModel);
        }

        [HttpPost]
        public IActionResult BuyVoucher(BuyVoucherViewModel buyModel)
        {
            if (ModelState.IsValid)
            {
                BuyVoucherModel buyVoucher = buyModel.CreateFromViewModel();

                _voucherRepository.BuyVoucher(buyVoucher);
                int id = _paymentChecksRepository.AddPaymentCheck(buyVoucher);

                return RedirectToAction("GetAllPaymentChecks", "PaymentChecks");
            }
            return RedirectToAction("BuyVoucher");
        }

        [HttpGet]
        public IActionResult GetAllVouchers()
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.GetAllVouchers()
                .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View(model);
        }
    }
}