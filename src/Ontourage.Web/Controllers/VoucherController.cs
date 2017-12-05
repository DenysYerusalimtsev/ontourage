using System;
using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ontourage.Core.Email;

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
        private readonly IEmailSender _emailSender;

        public VoucherController(IVoucherRepository voucherRepository,
            IFoodTypeRepository foodTypeRepository,
            ICountryRepository countryRepository,
            IHotelRepository hotelRepository,
            ITourOperatorRepository tourOperatorRepository,
            IClientRepository clientRepository,
            IPaymentChecksRepository paymentChecksRepository,
            IDiscountRepository discountRepository,
            IEmailSender emailSender)
        {
            _voucherRepository = voucherRepository;
            _foodTypeRepository = foodTypeRepository;
            _countryRepository = countryRepository;
            _hotelRepository = hotelRepository;
            _tourOperatorRepository = tourOperatorRepository;
            _clientRepository = clientRepository;
            _paymentChecksRepository = paymentChecksRepository;
            _emailSender = emailSender;
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
        public async Task<IActionResult> EditVoucher(VoucherViewModel editModel)
        {
            var oldVoucher = _voucherRepository.GetVoucherById(editModel.Id);
            var clients = _paymentChecksRepository.GetSameEmailClients(editModel.Id);
            if (ModelState.IsValid)
            {
                Voucher voucher = editModel.CreateFromViewModel();
                if (IsUpdated(voucher, oldVoucher))
                {
                    foreach (var c in clients)
                    {
                        await _emailSender.SendEmail(
                            email: c.Email,
                            subject: "Изменение времени",
                            message: "Добрый день, уважаемый пользователь Ontourage! " + "\n" +
                                     "Хотим известить Вас о том, что время вашего отправления Вашего тура " + voucher.TourName + " "
                                     + voucher.DepartureTime +
                                     " из " + voucher.DeparturePlace + "." +
                                     "Время Вашего прибытия в " + voucher.ArrivalPlace + " " + voucher.ArrivalTime + "." +
                                     "Спасибо, что пользуетесь Ontourage!" +
                                     "<href = http://localhost:49781/Voucher/ViewDetails/" + "editModel.Id/>");
                    }
                }
                _voucherRepository.EditVoucher(voucher);
                return RedirectToAction("GetAllVouchers");
            }
            return RedirectToAction("EditVoucher");
        }

        [HttpPost]
        public IActionResult GetHotVouchers()
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.GetHotVouchers()
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
        }

        [HttpPost]
        public IActionResult GetLowerCostVoucher()
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.GetLowCostVouchers()
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
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
            return View("ViewDetails", model);
        }

        [HttpPost]
        public IActionResult SearchVoucher(SearchCountryViewModel search)
        {
            if (String.IsNullOrEmpty(search.Country))
            {
                return RedirectToAction("GetAllVouchers");
            }
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.SearchVoucher(search.Country.ToLower())
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
        }

        [HttpPost]
        public IActionResult SearchByCost(SearchByCostViewModel modelCost)
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.SearchByCost(modelCost.Price)
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
        }

        [HttpPost]
        public IActionResult FilterByCost(PriceFilterViewModel price)
        {
            ; var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.FilterByCost(price.CostFrom, price.CostTo)
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
        }

        [HttpPost]
        public IActionResult SearchByDates(DateSearchViewModel dateModel)
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.VouchersBetweenDates(dateModel.FirstDate, dateModel.SecondDate)
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
        }


        [HttpGet]
        public IActionResult SortVouchers()
        {
            var model = new VoucherStoreViewModel
            {
                Vouchers = _voucherRepository.GetAllVouchers().OrderBy(v => v.TourName)
                    .Select(v => new VoucherAggregateViewModel(v)).ToList()
            };
            return View("GetAllVouchers", model);
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
                return RedirectToAction("ViewDetails", "PaymentChecks", new { Id = id });
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

        private bool IsUpdated(Voucher updatedVoucher, VoucherAggregate voucher)
        {
            return updatedVoucher.ArrivalTime != voucher.ArrivalTime ||
                    updatedVoucher.DepartureTime != voucher.DepartureTime;
        }
    }
}