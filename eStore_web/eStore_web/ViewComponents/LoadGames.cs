using eStore_web.EF;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eStore_web.Common;

namespace eStore_web.ViewComponents
{
    public class LoadGames: ViewComponent
    {
        private eContext db = new eContext();
        public async Task<IViewComponentResult> InvokeAsync(string pretraga = null, int sortirajPo = 1, int zanr = 0, int kategorija = 0, int from = 0, int to = 20)
        {
            KupacIndexVM kupacIndexVM = await GetItemsAsync(pretraga, sortirajPo, zanr, kategorija, from, to, db);

            ViewBag.VM = kupacIndexVM;
            
           
            return View();
        }
        private Task<KupacIndexVM> GetItemsAsync(string pretraga, int sortirajPo, int zanr, int kategorija, int from, int to,eContext db)
        {
            PretragaIgra p = new PretragaIgra();

            return Task.Run(() => p.GetKupacIndexVM(pretraga,sortirajPo,zanr,kategorija,from,to,db));


        }
    }
}
