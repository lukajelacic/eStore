using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eStore_web.Models;
using Microsoft.AspNetCore.Http;
using eStore_web.EF_Models;
using eStore_web.EF;
using eStore_web.Common;
namespace eStore_web.Controllers
{

    public class HomeController : Controller
    {
         eContext db = new eContext();
      
        public IActionResult Index()
        {
            Kupac Kupac = db.Kupac.FirstOrDefault();
            
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Kupac = HttpContext.Session.GetObjectFromJson<Kupac>("Test");
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
