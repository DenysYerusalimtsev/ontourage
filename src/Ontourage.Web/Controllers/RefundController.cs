using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;

namespace Ontourage.Web.Controllers
{
    public class RefundController : Controller
    {
        private readonly IRefundRepository _refundRepository;

        public RefundController(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        [HttpGet]
        public IActionResult GetAllRefunds()
        {
            var model = new RefundBaseViewModel()
            {
                Refunds = _refundRepository.GetAllRefunds()
                    .Select(r => new RefundViewModel()).ToList()
            };
            return View(model);
        }
    }
}