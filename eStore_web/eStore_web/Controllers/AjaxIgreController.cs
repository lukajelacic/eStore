using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore_web.Controllers
{
    public class AjaxIgreController : Controller
    {
        eContext db = new eContext();
       
        public IActionResult Index(int id)
        {
            var model = db.Igra.Where(x => x.DeveloperID == id).Include(x => x.GameGenre).ToList();
            return PartialView(model);
        }
    }
}