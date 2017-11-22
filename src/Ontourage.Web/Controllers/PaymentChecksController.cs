using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Linq;

namespace Ontourage.Web.Controllers
{
    public class PaymentChecksController : Controller
    {
        private readonly IPaymentChecksRepository _paymentChecks;
        private readonly IClientRepository _clients;
        private readonly IVoucherRepository _vouchers;

        public PaymentChecksController(IPaymentChecksRepository paymentChecks,
            IClientRepository clients, IVoucherRepository vouchers)
        {
            _paymentChecks = paymentChecks;
            _clients = clients;
            _vouchers = vouchers;
        }

        [HttpGet]
        public IActionResult GetAllPaymentChecks()
        {
            var model = new PaymentChecksStoreViewModel
            {
                PaymentChecks = _paymentChecks.GetAllPaymentChecks()
                .Select(p =>
                {
                    var paymentCheckModel = new PaymentCheckViewModel
                    {
                        ClientFirstName = _clients.GetClientById(p.ClientId).FirstName,
                        ClientLastName = _clients.GetClientById(p.ClientId).LastName,
                        ClientPassport = _clients.GetClientById(p.ClientId).Passport,
                        VoucherName = _vouchers.GetVoucherById(p.VoucherId).TourName
                    };
                    paymentCheckModel.BindFromModel(p);
                    return paymentCheckModel;
                }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var paymentCheckToDetails = _paymentChecks.GetPaymentCheckById(id);
            var model = new PaymentCheckViewModel
            {
                ClientFirstName = _clients.GetClientById(paymentCheckToDetails.ClientId).FirstName,
                ClientLastName = _clients.GetClientById(paymentCheckToDetails.ClientId).LastName,
                ClientPassport = _clients.GetClientById(paymentCheckToDetails.ClientId).Passport,
                VoucherName = _vouchers.GetVoucherById(paymentCheckToDetails.VoucherId).TourName
            };
            model.BindFromModel(paymentCheckToDetails);
            return View("ViewDetails", model);
        }
    }
}