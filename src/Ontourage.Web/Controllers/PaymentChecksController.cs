using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Linq;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Controllers
{
    public class PaymentChecksController : Controller
    {
        private readonly IPaymentChecksRepository _paymentChecks;
        private readonly IRefundRepository _refundRepository;
        private readonly IVoucherRepository _voucherRepository;

        public PaymentChecksController(IPaymentChecksRepository paymentChecks, 
            IRefundRepository refundRepository, 
            IVoucherRepository voucherRepository)
        {
            _paymentChecks = paymentChecks;
            _refundRepository = refundRepository;
            _voucherRepository = voucherRepository;
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

        [HttpPost]
        public IActionResult GetThisDayChecks()
        {
            var model = new PaymentChecksStoreViewModel
            {
                PaymentChecks = _paymentChecks.GetThisDayChecks()
                    .Select(p => new PaymentCheckViewModel(p)).ToList()
            };
            return View("GetAllPaymentChecks", model);
        }

        [HttpGet]
        public IActionResult DoRefund(int id)
        {
            var paymentCheck = _paymentChecks.GetPaymentCheckById(id);
            _voucherRepository.AddRefundVouchers(paymentCheck);
            var refundId = _refundRepository.AddRefund(paymentCheck);
            var refund = _refundRepository.GetRefundById(refundId);

            var refundModel = new RefundViewModel();
            refundModel.BindFromModel(refund);

            _paymentChecks.DoRefund(refund.PaymentCheck.Id);
            return RedirectToAction("GetAllRefunds", "Refund");
        }
    }
}