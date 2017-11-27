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
                .Select(p => new PaymentCheckViewModel(p)).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var paymentCheckToDetails = _paymentChecks.GetPaymentCheckById(id);
            var model = new PaymentCheckViewModel(paymentCheckToDetails);
            return View("ViewDetails", model);
        }
    }
}