using System;
using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models.Sql;
using Ontourage.Core.Entities.Sql;

namespace Ontourage.Web.Controllers
{
    public class SqlController : Controller
    {
        private readonly IDbQueryRepository _queryRepository;

        public SqlController(IDbQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet]
        public IActionResult GetResult()
        {
            return View(new SqlQueryViewModel());
        }

        [HttpPost]
        public IActionResult GetResult(SqlQueryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Query.StartsWith("select"))
                    {
                        SelectResult result = _queryRepository.ExecuteQuery(model.Query);
                        return View("SelectResults", new SelectResultsViewModel(result));
                    }

                    _queryRepository.ExecuteNonQuery(model.Query);
                    return View("GetResult", new SqlQueryViewModel());
                }
                catch (Exception e)
                {
                    return View("GetResult", new SqlQueryViewModel
                    {
                        ErrorMessage = e.Message
                    });
                }
            }
            return RedirectToAction("GetResult");
        }
    }
}