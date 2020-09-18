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
using eStore_web.Common;
using eStore_web.Services;

namespace eStore_web.Controllers
{
    public class DeveloperController: Controller
    {
        eContext db = new eContext();
        

        private INotifikacije _notifikacije;

        public DeveloperController(INotifikacije notifikacije)
        {
            _notifikacije = notifikacije;
            
        }
        public IActionResult Index()
        {
            //ViewData["username"] = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser").AccountName;
            return View();
        }

        public IActionResult UploadGame()
        {
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            ViewData["zanrovi"] = db.GameGenre.ToList();
            ViewData["dev"] = LoginInfo.OsobaID; //osobaID ne treba biti u developera, nego treba promjeniti tako da se gleda samo DeveloperID
            ViewData["developeri"] = db.Developer.ToList();
            return View("UploadGame");
        }

        public IActionResult RequestPublish(int devID, string naziv, int Genre, string opis, string igra)
        {
            Igra igraZahtjev = new Igra();

            igraZahtjev.Naziv = naziv;
            igraZahtjev.GameGenreID = Genre;
            igraZahtjev.Opis = opis;
            igraZahtjev.DeveloperID = devID;
            //igraZahtjev.LinkIgre = igra;
            igraZahtjev.Odobrena = false;
            igraZahtjev.TrailerUrl = igra;
            igraZahtjev.DatumObjave = DateTime.Now;
            igraZahtjev.Cijena = 10;

            db.Igra.Add(igraZahtjev);
            db.SaveChanges();
            _notifikacije.posaljiNotifikacijeAdminu(Autentifikacija.GetLogiraniKorisnik(HttpContext).OsobaID, new NotifikacijaVM()
            {
                Poruka = "Novi zahtjev za igricu!",
                Url = "/Administrator/OdobriGet"
            });
            return View("Index");
        }
        
        public IActionResult ViewGames()
        {
            ViewData["listaIgara"] = db.Igra.ToList();
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");

            List<Developer>developeri=db.Developer.ToList();
            foreach(Developer d in developeri)
            {
                if (d.OsobaID == LoginInfo.OsobaID)
                {
                    ViewData["dev"] = d.DeveloperID;
                }
            }
            return View();
        }

        public IActionResult EditGame(int igraID)
        {
            ViewData["zanrovi"] = db.GameGenre.ToList();
            List<Igra> listaIgara= db.Igra.ToList();
            foreach (Igra i in listaIgara)
            {
                if (i.IgraID == igraID)
                {
                    ViewData["igra"] = i;
                    break;
                }
            }
            return View();
        }

        public IActionResult SaveChanges(int id, string naziv, int GenreID, string opis, string igra)
        {
            List<Igra> listaIgara = db.Igra.ToList();
            foreach (Igra i in listaIgara)
            {
                if (i.IgraID == id)
                {
                    i.Naziv = naziv;
                    i.GameGenreID = GenreID;
                    i.Opis = opis;
                }
            }

            db.SaveChanges();

            return RedirectToAction("ViewGames");
        }

        public IActionResult ViewGameDetails(int igraID)
        {
            ViewData["zanrovi"] = db.GameGenre.ToList();
            List<Igra> listaIgara = db.Igra.ToList();
            foreach (Igra i in listaIgara)
            {
                if (i.IgraID == igraID)
                {
                    ViewData["igra"] = i;
                    break;
                }
            }
            return View("ViewGameDetails");
        }

        public IActionResult DeleteGame(int igraID)
        {
            List<Igra> listaIgara = db.Igra.ToList();

            foreach (Igra i in listaIgara)
            {
                if (i.IgraID == igraID)
                {
                    listaIgara.Remove(i);
                }
            }

            return RedirectToAction("ViewGames");
        }

        public IActionResult ViewProfile()
        {
            LoginInfo LoginInfo = HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");
            List<Developer> developeri = db.Developer.ToList();
            foreach (Developer d in developeri)
            {
                if (d.OsobaID == LoginInfo.OsobaID)
                {
                    ViewData["dev"] = d;
                }
            }

            return View();
        }

        public IActionResult EditProfile(int devID, string nazivKompanije, string ime, string prezime, DateTime datumRodenja, string email, string grad, string regija, string drzava)
        {
            List<Developer> developers = db.Developer.ToList();
            foreach(Developer d in developers)
            {
                if (d.DeveloperID == devID)
                {
                    d.NazivKompanije = nazivKompanije;
                    d.Osoba.Ime = ime;
                    d.Osoba.Prezime = prezime;
                    d.Osoba.DatumRodenja = datumRodenja;
               

                    db.SaveChanges();
                }
            }
            return View("Index");
        }
    }
}
