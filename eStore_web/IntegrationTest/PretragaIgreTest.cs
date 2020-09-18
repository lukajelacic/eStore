using eStore_web.Common;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest
{
    [TestClass]
    public class PretragaIgreTest
    {
        PretragaIgra pretragaIgre = new eStore_web.Common.PretragaIgra();
        eContext db = new eContext();
        [TestMethod]
        public void PretragaIgrica()
        {
            var igre = new List<Igra>();
            igre.Add(db.Igra.Where(i => i.Naziv == "Grand Theft Auto V").FirstOrDefault());
            igre.Add(db.Igra.Where(i => i.Naziv == "Counter-Strike: Global Offensive").FirstOrDefault());
            Assert.AreEqual(igre[0].Naziv, pretragaIgre.PretragaIgrica("Grand Theft Auto V", db)[0].Naziv);
            Assert.AreEqual(igre[1].Naziv, pretragaIgre.PretragaIgrica("Counter-Strike: Global Offensive", db)[0].Naziv);
        }
    }
}
