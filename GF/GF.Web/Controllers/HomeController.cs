using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GF.Data.Models;

namespace GF.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGFRepository _gfRepository;

        public HomeController() { }
        public HomeController(IGFRepository GfRepository)
        {
            _gfRepository = new GFDBInMemoryEntities();
        }

        public ActionResult Index()
        {
            return RedirectToAction("HistoryDetail", new { CustomerID = 1 });
            
        }

        public ActionResult HistoryDetail(int? CustomerID)
        {
            var HistoryDetails = _gfRepository.GetOrderRolls(CustomerID.Value); 
            return View(HistoryDetails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        
    }
}
