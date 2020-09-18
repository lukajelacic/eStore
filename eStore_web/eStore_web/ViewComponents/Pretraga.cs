using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eStore_web.EF;
using eStore_web.EF_Models;

using eStore_web.Common;

namespace eStore_web.ViewComponents
{
    public class Pretraga : ViewComponent
    {
        private eContext db = new eContext();
        public async Task<IViewComponentResult> InvokeAsync(string pretraga)
        {
            List<Igra> igre = await GetItemsAsync(pretraga);
            ViewBag.igre = igre;
            ViewBag.pretraga = pretraga;
            return View();
        }
        private Task<List<Igra>> GetItemsAsync(string pretraga)
        {
            PretragaIgra PretragaIgra = new PretragaIgra();
            return Task.Run(() => PretragaIgra.PretragaIgrica(pretraga, db));
        }
    }
}