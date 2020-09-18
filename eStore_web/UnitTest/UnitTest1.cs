using Microsoft.VisualStudio.TestTools.UnitTesting;
using eStore_web.Controllers;
using System.Collections.Generic;
using eStore_web.EF_Models;
using System;
using eStore_web.Common;

namespace UnitTest
{
    [TestClass]
    public class ControllerTest
    {
        List<Igra> TestList = new List<Igra>() {

            new Igra(){

                Cijena=9.99f,
                DatumObjave=DateTime.Now,
                IgraID=1,
                Naziv="TestGame 1",
                Opis="test",
                Odobrena=true,
                PremiumStatus=false,
            },
            new Igra(){

                Cijena=19.99f,
                DatumObjave=DateTime.MaxValue,
                IgraID=2,
                Naziv="TestGame 2",
                Opis="test",
                Odobrena=true,
                PremiumStatus=false,
            },
            new Igra(){

                Cijena=29.99f,
                DatumObjave=DateTime.MinValue,
                IgraID=3,
                Naziv="TestGame 3",
                Opis="test",
                Odobrena=true,
                PremiumStatus=false,
            },
        };

        [TestMethod]
        public void Sortiranje()
        {
            PretragaIgra p = new PretragaIgra();

            List<Igra> igre = p.Sortiranje(TestList, 1);
            Assert.AreEqual(TestList[1].IgraID, igre[0].IgraID);
        }
        [TestMethod]
        public void Valid()
        {
            Assert.AreEqual(1, 1);
        }

    }
}
