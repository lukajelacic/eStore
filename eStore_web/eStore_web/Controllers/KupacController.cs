using System;
using System.Collections.Generic;
using System.Linq;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStore_web.Common;
using eStore_web.ViewModel;
using eStore_web.ViewComponents;

namespace eStore_web.Controllers
{
    public class KupacController : Controller
    {
        eContext db = new eContext();
      
        WishListManagment WishListManagment = new WishListManagment();
        PratiManagment PratiManagment = new PratiManagment();

        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult Index(string pretraga = null, int sortirajPo = 1, int zanr = 0, int kategorija = 0,int from=0,int to=20)
        {

            


            #region DodjelVrijednosti

            KupacIndexVM kupacIndexVM = new KupacIndexVM()
            {
                pretraga = pretraga,
                sortirajPo = sortirajPo,
                zanr = zanr,
                kategorija = kategorija,
                from = from,
                to = to,
                gameGenre = db.GameGenre.ToList(),
                ratingCategorie=db.RatingCategorie.ToList()
            };
            #endregion

            #region PovratneVrijednosti
            ViewBag.kupacIndexVM = kupacIndexVM;
            return View("Index");
            #endregion
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult LoadGames(string pretraga = null, int sortirajPo = 1, int zanr = 0, int kategorija = 0, int from = 0, int to = 20)
        {
            return ViewComponent("LoadGames", new { pretraga, sortirajPo, zanr, kategorija, from, to, db });
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult Search(string pretraga)
        {

            return ViewComponent("Pretraga", new { pretraga = pretraga });
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult DodajWishList(Kupac kupac, Igra igra)
        {
            List<WishList> WishList = db.WishList.Where(x => x.KupacID == kupac.KupacID).ToList();
            List<Recenzija> recenzija = db.Recenzija.Where(y => y.KupacID == kupac.KupacID).ToList();
            List<int> BrojElemenata = new List<int>();
            foreach (WishList x in WishList)
            {
                if (x.Igra.IgraID == x.Igra.IgraID)
                {
                    //Igra vec postoji
                }
            }
            WishList v = new WishList();
            v.KupacID = kupac.KupacID;
            v.IgraID = igra.IgraID;
            WishList.Add(v);
            return View();
        }

        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult GameDetailes(int igraID)
        {
            #region dodjelaVrijednosti
            GameDetailesVM GameDetailesVM = new GameDetailesVM();
            double totalOcjena = 0;
            int brojRecenzija = 0;
            double novaCijena = 0;
            int KupacID = -1;
            #endregion dodjelaVrijednosti

            #region preuzimanjeKupca
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            if (LoginInfo != null)
            {
                if (LoginInfo.TipKorisnika == 1)
                {
                    Kupac Kupac = db.Kupac.Where(k => k.OsobaID == LoginInfo.OsobaID).FirstOrDefault();
                    if (Kupac != null)
                    {
                        KupacID = Kupac.KupacID;
                    }
                }
            }
            #endregion preuzimanjeKupca

            #region Preuzimanje_Igra_i_Popust
            Igra igra = db.Igra.Where(i => i.IgraID == igraID)
                .FirstOrDefault();
            Popust popust = db.Popust.Where(p => p.PocetakPopusta <= DateTime.Now && p.KrajPopusta>=DateTime.Now && p.IgraID==igraID).FirstOrDefault();


            if (popust != null && igra != null)
            {
                float procent = 1 - (float)popust.PopustProcent / 100;

                novaCijena = Math.Round((igra.Cijena * procent), 2);
            }


            #endregion Preuzimanje_Igra_i_Popust

            #region recenzije
            totalOcjena = (from ig in db.Recenzija
                           where ig.IgraID == igraID
                           select ig.Ocjena).Sum();

            brojRecenzija = (from ig in db.Recenzija
                             where ig.IgraID == igraID
                             select ig.Ocjena).Count();

            totalOcjena /= brojRecenzija;

            

            List<Recenzija> recenzije = db.Recenzija.Where(r => r.IgraID == igra.IgraID)
                .Include(r=>r.Kupac)
                .ThenInclude(k=>k.Osoba)
                .ThenInclude(o=>o.OsobaImage)
                .ToList();
            GameDetailesVM.Recenzije = recenzije;
            #endregion recenzije

            #region punjenjeVM
            GameDetailesVM.Igra = igra;
            GameDetailesVM.Popust = popust;
            GameDetailesVM.NovaCijena = novaCijena;
            GameDetailesVM.Ocjena = totalOcjena;
            GameDetailesVM.ImaIgru = ImaIgru(KupacID, igraID);
            GameDetailesVM.ImaWishList = WishListManagment.ImaWishList(igraID, KupacID);
            GameDetailesVM.ImaRecenziju = ImaRecenziju(KupacID, igraID);
            GameDetailesVM.GameTrailer = igra.TrailerUrl;
            //.....
            GameDetailesVM.PratiIgru = PratiManagment.PratiIgru(igraID,KupacID);

            #endregion punjenjeVM



            ViewBag.GameDetailesVM = GameDetailesVM;
            return View("GameDetailes");
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool  DodajRecenzija(int KupacID,int IgraID,int Ocjena,string textarea)
        {
            Kupac Kupac = db.Kupac.Where(k => k.KupacID == KupacID).FirstOrDefault();
            Igra Igra = db.Igra.Where(i => i.IgraID == IgraID).FirstOrDefault();
            if (Kupac == null || Igra == null || (Ocjena < 1 || Ocjena > 5 || textarea == null))
                return false;


            Recenzija recenzija = new Recenzija()
            {
                Kupac = Kupac,
            Igra=Igra,
            Ocjena=Ocjena,
            RecenzijaText=textarea,
            //Treba dodat u bp
            //DatumObjave=DateTime.Now
            
            };
            db.Recenzija.Add(recenzija);
            db.SaveChanges();
            db.Dispose();

           
            return true;
        }

        //idk
        public bool Upload(int IgraID,IFormFile Image)
        {
            FileManagment p = new FileManagment();
            return p.DodajSliku(IgraID, 0, Image, db);

        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult KupiIgru(int KupacID,int IgraID)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID).FirstOrDefault();

            KupacID = kupac.KupacID;

            if (ImaIgru(KupacID,IgraID))
            {
                return RedirectToAction("GameDetailes", new { IgraID });
            }
           
        
            Kupac Kupac = db.Kupac.Where(k => k.KupacID == KupacID)
                .Include(w=>w.Wallet)
                .FirstOrDefault();
            Igra Igra = db.Igra.Where(i => i.IgraID == IgraID).FirstOrDefault();
            //Popust Popust = db.Popust.Where(p => p.Aktivan == true && p.IgraID == IgraID).FirstOrDefault();

            if (Igra.Cijena < Kupac.Wallet.balance)
            {
                KupacKupuje kupacKupuje = new KupacKupuje()
                {
                    Igra = Igra,
                    Kupac = Kupac,
                    Cijena = Igra.Cijena,
                    VrijemeKupovine = DateTime.Now
                };
                WalletHistory walletHistory = new WalletHistory()
                {
                    WalletID = Kupac.Wallet.WalletID,
                    IsIgra = true,
                    TransactionAmount = (double)Igra.Cijena * (-1),
                    CurrentBalance = Kupac.Wallet.balance - Igra.Cijena,
                    IgraID=IgraID
                };



                db.WalletHistory.Add(walletHistory);

                RemoveWishList(IgraID, KupacID);
                Kupac.Wallet.balance -= Igra.Cijena;
                db.KupacKupuje.Add(kupacKupuje);
                db.SaveChanges();
                db.Dispose();
            }
            else
            {
                TempData["error-key"] = "<br> No monie boi";
            }
            return RedirectToAction("GameDetailes", new { IgraID });
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult PreuzmiIgru(int KupacID,int IgraID)
        {
            if (ImaIgru(KupacID, IgraID))
            {
                return RedirectToAction("GameDetailes", new { IgraID });
            }

            Kupac Kupac = db.Kupac.Where(k => k.KupacID == KupacID)
                .Include(k=>k.Wallet)
                .FirstOrDefault();
            Igra Igra = db.Igra.Where(i => i.IgraID == IgraID).FirstOrDefault();

            if(Igra.PremiumStatus==true && Kupac.PretplacenNaPremium==true)
            {
                PreuzimanjeIgre preuzimanjeIgre = new PreuzimanjeIgre()
                {
                    Igra = Igra,
                    Kupac = Kupac,
                    VrijemePreuzimanja = DateTime.Now
                };

                WalletHistory walletHistory = new WalletHistory()
                {
                    WalletID = Kupac.Wallet.WalletID,
                    IsIgra = true,
                    TransactionAmount = 0,
                    CurrentBalance =Kupac.Wallet.balance,
                    IgraID = IgraID
                };
                db.WalletHistory.Add(walletHistory);

                db.PreuzimanjeIgre.Add(preuzimanjeIgre);
                db.SaveChanges();
                db.Dispose();
            }


            return RedirectToAction("GameDetailes", new { IgraID });
        }



        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool ImaIgru(int KupacID,int IgraID)
        {
            KupacKupuje kupacKupuje = db.KupacKupuje.Where(kk => kk.KupacID == KupacID && kk.IgraID == IgraID).FirstOrDefault();
            PreuzimanjeIgre preuzimanjeIgre = db.PreuzimanjeIgre.Where(pi => pi.KupacID == KupacID && pi.IgraID == IgraID).FirstOrDefault();
            if (kupacKupuje!=null || preuzimanjeIgre!=null)
            {
                return true;
            }
            return false;
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool ImaRecenziju(int KupacID, int IgraID)
        {
            Recenzija recenzija = db.Recenzija.Where(r => r.KupacID == KupacID && r.IgraID == IgraID).FirstOrDefault();

            if (recenzija != null)
                return true;
            return false;
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool ImaWishList(int KupacID,int IgraID)
        {
            return WishListManagment.ImaWishList(IgraID, KupacID);
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool AddWishList(int IgraID,int KupacID)
        {
            return WishListManagment.AddWishList(IgraID, KupacID);
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool RemoveWishList(int IgraID,int KupacID)
        {
            return WishListManagment.RemoveWishList(IgraID, KupacID);
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool AddPrati(int IgraID, int KupacID)
        {
            return PratiManagment.AddPrati(IgraID, KupacID);
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool RemovePrati(int IgraID, int KupacID)
        {
            return PratiManagment.RemovePrati(IgraID, KupacID);
        }
        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public void AddReport(int RecenzijaID,int KupacID,string text)
        {
            PrijavaRecenzije prijavaRecenzije = new PrijavaRecenzije()
            {
                KupacID = KupacID,
                RecenzijaID = RecenzijaID,
                Razlog = text
            };
            db.Add(prijavaRecenzije);
            db.SaveChanges();
            db.Dispose();
        }


    }
}




