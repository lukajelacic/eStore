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
    public class PrikazWishlist : ViewComponent
    {
        private eContext db = new eContext();
        public async Task<IViewComponentResult> InvokeAsync(int k,int IsSame)
        {
            List<WishList> wishlist = await GetItemsAsync(k);
            ViewBag.wishlist = wishlist;
            ViewBag.IsSame = IsSame;
            return View();
        }

        private Task<List<WishList>> GetItemsAsync(int k)
        {
            return db.WishList.Where(kupac => kupac.KupacID == k)
                         .Include(igra => igra.Igra)
                         .Include(kupac => kupac.Kupac)
                         .ToListAsync();
        }
    }
}