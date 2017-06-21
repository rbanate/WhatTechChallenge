
using System.Linq;
using System.Web.Mvc;
using WhateTechChallenge.Models;
using WhateTechChallenge.Utils;

namespace WhateTechChallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var settled = CsvParser.ParseCustomerBetInCsv(Server.MapPath(@"~/Content/csv/Settled.csv"), true);
            var unsettled = CsvParser.ParseCustomerBetInCsv(Server.MapPath(@"~/Content/csv/Unsettled.csv"));

            var customerBettingProfile = new CustomersBettingProfile(settled, unsettled);

            

            return View(customerBettingProfile.CustomerBets.OrderBy(o=>o.CustomerId).ToList());
        }

        
    }
}