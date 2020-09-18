using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;





namespace eStore_web.ViewComponents
{
    public class PrikazRecenzija : ViewComponent
    {
        private eContext db = new eContext();
        public async Task<IViewComponentResult> InvokeAsync(int k,int IsSame)
        {

            List<Recenzija> recenzije = await GetItemsAsync(k);
            ViewBag.recenzije = recenzije;
            ViewBag.IsSame = IsSame;
            return View();
        }

        private Task<List<Recenzija>> GetItemsAsync(int k)
        {
            return db.Recenzija.Where(kupac => kupac.KupacID == k)
             .Include(igra => igra.Igra)
             .ToListAsync();
        }
    }
}