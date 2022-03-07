using NewFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewFP.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public ActionResult Index()
        {
            return View(new Calculator());
        }

        [HttpPost]
        public ActionResult Index(Calculator C, string calculate)
        {
            if (calculate == "teaspoons")
            {
                C.Answer = C.UserNumber * C.Teaspoons;
            }
            else
            {
                C.Answer = C.UserNumber * C.Tablespoons;
            }

            return View(model: C);
        }
    }
}