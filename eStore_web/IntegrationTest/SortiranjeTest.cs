using eStore_web.Common;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTest
{
    [TestClass]
    public class SortiranjeTest
    {
        PretragaIgra pretragaIgre = new eStore_web.Common.PretragaIgra();
        eContext db = new eContext();
        [TestMethod]
        public void SortiranjeIgrica()
        {
            var igre = new List<Igra>();
            var dbIgre = db.Igra.ToList();
            Assert.AreEqual("Generation Zero",pretragaIgre.Sortiranje(dbIgre,2,db)[0].Naziv);
            Assert.AreEqual("Rocket League", pretragaIgre.Sortiranje(dbIgre, 3, db)[0].Naziv);
            Assert.AreEqual("Generation Zero", pretragaIgre.Sortiranje(dbIgre, 4, db)[0].Naziv);
            Assert.AreEqual("Rocket League", pretragaIgre.Sortiranje(dbIgre, 5, db)[0].Naziv);
        }
    }
}
