using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStore_web.ViewModel;
using eStore_web.Common;
using System.Web;

namespace eStore_web.Controllers
{
    public class ProfileController : Controller
    {
        eContext db = new eContext();
       
        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult Index(int KupacID = 1)
        {
            #region IsUserLoggedIn
            int IsSame = 0;
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            if (LoginInfo != null)
            {
                Kupac Kupac = db.Kupac.Where(kup => kup.OsobaID == LoginInfo.OsobaID).FirstOrDefault();
                if (Kupac != null)
                {
                    if (Kupac.KupacID == KupacID)
                        IsSame = 1;
                }
            }
            ViewBag.IsSame = IsSame;
            #endregion IsUserLoggedIn




            var k = db.Kupac.Where(kupac => kupac.KupacID == KupacID)
               .Include(o => o.Osoba)
               .ThenInclude(oi=>oi.OsobaImage)
               .Include(o => o.Osoba)
               .ThenInclude(li => li.LoginInfo)
            
               .FirstOrDefault();
            if (k == null)
            {
                return View("ProfileNotFound");
            }
            ViewBag.kupac = k;
            return View("profile");
        }


        [Autorizacija(Kupac: true, Developer: true, Administrator: true, Svi: true)]
        public IActionResult vcProfile(int KupacID, string VC,int IsSame)
        {
            return ViewComponent(VC, new { k = KupacID, IsSame });
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public void RemoveWishList(int KupacID, int IgraID)
        {

            Kupac kupac = db.Kupac.Where(k => k.KupacID == KupacID).FirstOrDefault();
            WishList wish = db.WishList.Where(wl => wl.Kupac.KupacID == kupac.KupacID && wl.IgraID == IgraID).FirstOrDefault();
            if (kupac == null)
                throw Exception();
            if (wish != null)
            {
                db.WishList.Remove(wish);
                db.SaveChanges();
                db.Dispose();
            }


        }

        private Exception Exception()
        {
            throw new NotImplementedException();
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public void RemoveRecenzija(int KupacID, int IgraID)
        {
            Kupac kupac = db.Kupac.Where(k => k.KupacID == KupacID).FirstOrDefault();
            Recenzija recenzija = db.Recenzija.Where(wl => wl.Kupac.KupacID == kupac.KupacID && wl.IgraID == IgraID).FirstOrDefault();
            if (recenzija != null)
            {
                db.Recenzija.Remove(recenzija);
                db.SaveChanges();
                db.Dispose();
            }
        }



        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult EditProfile(int KupacID=1)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID)
               .Include(o => o.Osoba)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.Osoba)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.Osoba)
                  .ThenInclude(oi => oi.OsobaImage)
                .Include(o => o.Osoba)
                  .ThenInclude(g => g.Grad)
                   .ThenInclude(r => r.Regija)
                    .ThenInclude(d => d.Drzava)

                 .FirstOrDefault();


            OsobaImage ooo = db.OsobaImage.FirstOrDefault();

            List<Grad> gradovi = db.Grad.ToList();
            List<Regija> regije = db.Regija.ToList();
            List<Drzava> drzave = db.Drzava.ToList();

            ViewBag.gradovi = gradovi;
            ViewBag.regije = regije;
            ViewBag.drzave = drzave;
            ViewBag.kupac = kupac;

            TempData.Peek("success-key");
            TempData.Peek("error-key");
            
            return View("EditProfile",kupac);
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult EditProfileSet(int KupacID,string Ime,string Prezime,string DatumRodenja,string Email,string AccountName, IFormFile ProfilSlika,int Drzava,int Regija,int Grad,string UserName)

        {
           
            string success = null;
            string error = null;


            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID)
                .Include(o => o.Osoba)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.Osoba)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.Osoba)
                  .ThenInclude(g => g.Grad)
                   .ThenInclude(r => r.Regija)
                    .ThenInclude(d => d.Drzava)
                 .Include(o => o.Osoba)
                  .ThenInclude(oi => oi.OsobaImage)
                 .FirstOrDefault();
            if (kupac != null)
            {
                
                if (kupac.Osoba.Ime != Ime)
                {
                    kupac.Osoba.Ime = Ime;
                    success += "<br>- Ime";
                }
                if (kupac.Osoba.Prezime != Prezime)
                {
                    kupac.Osoba.Prezime = Prezime;
                    success += "<br>- Prezime";
                }
                if (kupac.Osoba.DatumRodenja != DateTime.Parse(DatumRodenja))
                {
                    kupac.Osoba.DatumRodenja = DateTime.Parse(DatumRodenja);
                    success += "<br>- Datum rodenja";
                }
                    if (kupac.Osoba.EmailAddress == null)
                    kupac.Osoba.EmailAddress = new EmailAddress();
                if (kupac.Osoba.EmailAddress.Email != Email)
                {
                    
                    EmailAddress EmailAddress = db.EmailAddress.Where(ea => ea.Email == Email).FirstOrDefault();
                    if(EmailAddress!=null)
                    {
                        error += "<br>-Email adresa <b>" + Email + "</b> vec postoji";
                    }
                    else
                    {
                        kupac.Osoba.EmailAddress.Email = Email;
                        success += "<br>- Email adresu";
                    }
                    //Sta ako ima vec ta email adresa ??????
                }
                    if(kupac.Osoba.LoginInfo == null)
                    kupac.Osoba.LoginInfo = new LoginInfo();
                if (kupac.Osoba.LoginInfo.AccountName != AccountName)
                {
                    LoginInfo LoginInfo = db.LoginInfo.Where(li => li.AccountName == AccountName).FirstOrDefault();
                    if (LoginInfo != null)
                    {
                        error += "<br>-Account name <b>" + AccountName + "</b> vec postoji";
                    }
                    else
                    {
                        kupac.Osoba.LoginInfo.AccountName = AccountName;
                        success += "<br>- Account name";
                    }
                }
                if(kupac.UserName!= UserName)
                {
                    kupac.UserName = UserName;
                    success += "<br>- Korisnicko ime";
                }
                if (ProfilSlika != null)
                {
                    if (DodajSliku(kupac.OsobaID, ProfilSlika))
                    {
                        success += "<br>- profilna slika ";
                    }
                    else
                    {
                        error += "<br>- Greska pri mjenjanju profilne slike";
                    }
                }

                if (kupac.Osoba.Grad != null)
                {
                    //kupac.Osoba.GradID = Grad;
                }
             
            }
            db.SaveChanges();
            db.Dispose();

            TempData["success-key"] = success;
            TempData["error-key"] = error;
            return RedirectToAction("EditProfile",new { KupacID });
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult ChangePassword(int KupacID)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac Kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID)
                .Include(k => k.Osoba)
                  .ThenInclude(o => o.LoginInfo)
                .FirstOrDefault();
            if (Kupac != null)
            {

                return View("ChangePassword", Kupac);

            }
            else
            {

                return RedirectToAction("/Kupac/Index");
            }
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult ChangePasswordSet(int KupacID,string StariPassword,string NoviPassword1)
        {
            Kupac Kupac = db.Kupac.Where(k => k.KupacID == KupacID)
                .Include(o => o.Osoba)
                  .ThenInclude(l => l.LoginInfo)
                .Include(o => o.Osoba)
                  .ThenInclude(e => e.EmailAddress)
                .Include(o => o.Osoba)
                  .ThenInclude(g => g.Grad)
                   .ThenInclude(r => r.Regija)
                    .ThenInclude(d => d.Drzava)
                    .FirstOrDefault();
            if (Kupac != null)
            {
                
                    if (Kupac.Osoba.LoginInfo.Password == StariPassword)
                    {
                        Kupac.Osoba.LoginInfo.Password = NoviPassword1;
                    db.SaveChanges();
                    db.Dispose();
                        TempData["success-key"] = "<br>Password uspjesno promjenjen<br>";
                        return View("EditProfile", Kupac);
                    }

                    TempData["error-key"] = "<br>Stari Password nije validan<br>";
                    return View("ChangePassword", Kupac);
                
               

            }
            else
            {
                TempData["error-key"] = "<br>Greska u sistemu,pokusajte kasnije<br>";
                return RedirectToAction("/Kupac/Index");
            }
        }




        public bool IsEmailValid(int OsobaID,string Email)
        {
            EmailAddress EmailAddress = db.EmailAddress.Where(ea => ea.Email == Email && ea.OsobaID!=OsobaID).FirstOrDefault();
            if (EmailAddress != null)
                return false;
            return true;
        }
        public bool IsAccountNameValid(int OsobaID,string AccountName)
        {
            LoginInfo LoginInfo = db.LoginInfo.Where(li => li.AccountName == AccountName && li.OsobaID!=OsobaID).FirstOrDefault();
            if (LoginInfo != null)
                return false;
            return true;
        }


        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool DodajSliku(int OsobaID=0, IFormFile Image = null)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            

            if (Log.OsobaID == 0 || Image == null)
            {
                return false;
            }
            else
            {
                FileManagment FileManagment = new FileManagment();
                return FileManagment.DodajSliku(0, OsobaID, Image);
            }

        }



        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public IActionResult WalletHistory(int KupacID)
        {
            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID)
                .Include(k=>k.Wallet)
                .FirstOrDefault();

            List<WalletHistory> wh = db.WalletHistory.Where(w => w.WalletID == kupac.Wallet.WalletID)
                .Include(w=>w.Igra)
                 .ToList();
            ViewBag.w = kupac.WalletID;
            ViewBag.wh = wh;
            ViewBag.k = kupac;


                return View();
        }

        [Autorizacija(Kupac: true, Developer: false, Administrator: false, Svi: false)]
        public bool RequestRefund(int KupacID,int IgraID,string text)
        {
            Refund refund = db.Refund.Where(k => k.KupacID == KupacID && k.IgraID==IgraID).FirstOrDefault();

            LoginInfo Log = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            Kupac kupac = db.Kupac.Where(k => k.OsobaID == Log.OsobaID).FirstOrDefault();
            
            if (refund != null)
                return false;
            refund = new Refund() {
                KupacID = kupac.KupacID,
                IgraID=IgraID,
                RazlogRefunda=text,
                VrijemeZahtijeva=DateTime.Now,
                VrijemeKupovine=DateTime.Now,
                VrijemeOdgovora=DateTime.Now
            };

            db.Refund.Add(refund);
            db.SaveChanges();
            db.Dispose();
            return true;

           
        }

    } 
}
