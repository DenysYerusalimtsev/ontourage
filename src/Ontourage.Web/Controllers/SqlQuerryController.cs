using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ontourage.Web.Controllers
{
    public class SqlQuerryController : Controller
    {
        [HttpGet]
        public IActionResult GetQuerryPlace()
        {
            return View();
        }
    }
}