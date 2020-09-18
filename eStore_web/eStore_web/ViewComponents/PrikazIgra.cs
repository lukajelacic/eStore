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
    public class PrikazIgra : ViewComponent
    {

        private eContext db = new eContext();
        public async Task<IViewComponentResult> InvokeAsync(int k = 1)
        {
            List<KupacKupuje> igre = await GetItemsAsync(k);
            List<PreuzimanjeIgre> PreuzeteIgre = await GetPreuzeteIgreAsync(k);
            List<Refund> refunds = await GetRefundsAsync(k);
            List<int> HasRefundRequest = new List<int>();
            foreach(KupacKupuje x in igre)
            {
                HasRefundRequest.Add(0);
                for(int i=0;i<refunds.Count;i++)
                {
                    if(refunds[i].IgraID==x.IgraID)
                    {
                        //HasRefundRequest[HasRefundRequest.Count - 1] = 1;
                        HasRefundRequest[i] = 1;
                        break;
                    }
                }
            }


            ViewBag.PreuzeteIgre = PreuzeteIgre;
            ViewBag.igre = igre;
            ViewBag.refunds = HasRefundRequest;
            ViewBag.k = k;

            return View();
        }

        private Task<List<KupacKupuje>> GetItemsAsync(int k)
        {
            return db.KupacKupuje.Where(kupac => kupac.KupacID == k)
                         .Include(kk => kk.Igra)
                         .ThenInclude(i => i.IgricaImage)
                         .ToListAsync();
        }

        private Task<List<PreuzimanjeIgre>> GetPreuzeteIgreAsync(int k)
        {
            return db.PreuzimanjeIgre.Where(kupac => kupac.KupacID == k)
                         .Include(kk => kk.Igra)
                         .ThenInclude(i => i.IgricaImage)
                         .ToListAsync();
        }

        private Task<List<Refund>> GetRefundsAsync(int k)
        {
            return db.Refund.Where(kupac => kupac.KupacID == k)
                         .ToListAsync();
        }
    }
}
