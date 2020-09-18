using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.Common;
using eStore_web.EF;
using eStore_web.EF_Models;
using eStore_web.Services;
using eStore_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStore_web.Controllers
{
    public class AdministratorController : Controller
    {

        eContext db = new eContext();
    
        private INotifikacije _notifikacije;

        public AdministratorController(INotifikacije notifikacije)
        {
            _notifikacije = notifikacije;
            
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OdobriGet()
        {
            var model = db.Igra.Where(x => x.Odobrena == false).ToList();
            return View( model);
        }
        public IActionResult OdobriSet(int id)
        {

            var igra = db.Igra.Include(x => x.Developer).Where(x => x.IgraID == id).SingleOrDefault();
            if (igra == null)
                return Content("nema");
            igra.Odobrena = true;
            db.Update(igra);
            db.SaveChanges();
            _notifikacije.posaljiNotifikacije(igra.Developer.OsobaID, Autentifikacija.GetLogiraniKorisnik(HttpContext).OsobaID, new NotifikacijaVM()
            {
                Poruka = "Vasa igrica " + igra.Naziv + " je odobrena",
                Url = "/Developer/ViewGameDetails?igraID=" + igra.IgraID
            });
            return RedirectToAction(nameof(OdobriGet));
        }
        //akcija u kontroleru Administrator za poziv viewa za ispis developera

        public IActionResult Prikaz()
        {
            List<Developer> lista = db.Developer.Include("Osoba").ToList();
            ViewData["lista-developera"] = lista;
            return View();
        }
        //akcija za banovanje developera
        public IActionResult Banuj(int DeveloperID)
        {
            
            Developer developer = db.Developer.Find(DeveloperID);
            if (developer == null)
            {
                return Content("Doslo je do greske. Molim vas pokusajte kasnije.");
            }
            developer.Banovan = true;
            db.Update(developer);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction(nameof(Prikaz));
        }
        public IActionResult Unban(int DeveloperID)
        {
            Developer developer = db.Developer.Find(DeveloperID);
            if (developer == null)
            {
                return Content("Doslo je do greske. Molim vas pokusajte kasnije.");
            }
            developer.Banovan = false;
            db.Update(developer);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction(nameof(Prikaz));
        }
        //akcija za pregled igara odredjenog developera
        public IActionResult PregledIgara(int DeveloperID)
        {
            var model = new PregledIgaraAdministratorVM
            {
                Igre = db.Igra.Where(x => x.DeveloperID == DeveloperID).ToList(),
                Developer = db.Developer.Where(x => x.DeveloperID == DeveloperID).Include(x=>x.Osoba).SingleOrDefault().Osoba.Ime + " " + db.Developer.Where(x => x.DeveloperID == DeveloperID).SingleOrDefault().Osoba.Prezime
            };
            var lista = db.Igra.Where(x => x.DeveloperID == DeveloperID).ToList();
            return View(model);

           
        }
        //akcija za prikaz igara sa njihovim osnovnim podacima
        public IActionResult PrikazIgara()
        {
            
            var listaIgara = db.Igra.Include(x => x.GameGenre).Include(x => x.RatingCategorie).ToList();

            return View(listaIgara);
            
        }
        public IActionResult PremiumOn(int id)
        {
            var igra = db.Igra.Where(x => x.IgraID == id).SingleOrDefault();
            igra.PremiumStatus = true;
            db.Update(igra);
            db.SaveChanges();
            return RedirectToAction(nameof(PrikazIgara));
        }
        public IActionResult PremiumOff(int id)
        {
            var igra = db.Igra.Where(x => x.IgraID == id).SingleOrDefault();
            igra.PremiumStatus = false;
            db.Update(igra);
            db.SaveChanges();
            return RedirectToAction(nameof(PrikazIgara));
        }
        public IActionResult Detalji(int DeveloperID)
        {
            
            var developer = db.Developer.Where(x => x.DeveloperID == DeveloperID).Include(x => x.Osoba).Include(x => x.Osoba.EmailAddress).Include(x => x.Osoba.Grad).SingleOrDefault();
            var model = new AdministratorDetaljiVM
            {
                DeveloperID=developer.DeveloperID,
                ImePrezime = developer.Osoba.Ime + " " + developer.Osoba.Prezime,
                Email = developer.Osoba.EmailAddress.Email,
                DatumRodjenja = developer.Osoba.DatumRodenja,
                Kompanija = developer.NazivKompanije,
                Grad = developer.Osoba.Grad.Naziv
            };
            return View(model);
        }
        //akcija koja se poziva prilikom klika na dugme uredi, pronalazi datu igru i prosledjuje je u formu za editovanje
        [HttpGet]
        public IActionResult Uredi(int id)
        {


            var igra = db.Igra.SingleOrDefault(x => x.IgraID == id);
            var model = new AdministratorUrediVM
            {
                IgraId = igra.IgraID,
                Naziv = igra.Naziv,
                Opis = igra.Opis,
                Cijena = igra.Cijena,
                DatumObjave = igra.DatumObjave,
                Odobrena = igra.Odobrena,
                PremiumStatus = igra.PremiumStatus,
                Zanrovi = db.GameGenre.Select(x => new SelectListItem
                {
                    Value = x.GameGenreID.ToString(),
                    Text = x.NazivZanra
                }).ToList(),
                Kategorije = db.RatingCategorie.Select(x => new SelectListItem
                {
                    Value = x.RatingCategorieID.ToString(),
                    Text = x.NazivKategorije
                }).ToList()


            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(AdministratorUrediVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Zanrovi = db.GameGenre.Select(x => new SelectListItem
                {
                    Value = x.GameGenreID.ToString(),
                    Text = x.NazivZanra
                }).ToList();
                model.Kategorije = db.RatingCategorie.Select(x => new SelectListItem
                {
                    Value = x.RatingCategorieID.ToString(),
                    Text = x.NazivKategorije
                }).ToList();
                return View(model);
            }
            Igra i = db.Igra.Find(model.IgraId);
            i.Naziv = model.Naziv;
            i.Cijena = model.Cijena;
            i.DatumObjave = model.DatumObjave;
            i.Opis = model.Opis;
            i.PremiumStatus = model.PremiumStatus;
            i.RatingCategorieID = model.KategorijaId;
            i.GameGenreID = model.ZanrId;
            db.Update(i);
            db.SaveChanges();
            db.Dispose();

            return RedirectToAction(nameof(PrikazIgara));
        }

        public IActionResult PrikazRefundova()
        {
            var lista = db.Refund.Include(x => x.Kupac).Include(x => x.Kupac.Osoba).Include(x => x.Igra).ToList();
            return View(lista);
        }
        public IActionResult Odbij(int id)
        {
            var refund = db.Refund.SingleOrDefault(x => x.RefundID == id);
            db.Refund.Remove(refund);
            db.SaveChanges();
            return RedirectToAction(nameof(PrikazRefundova));

        }
        public IActionResult Prihvati(int id)
        {
            var refund = db.Refund.Include(x => x.Igra).Where(x => x.RefundID == id).SingleOrDefault();
            var wallet = db.Wallet.SingleOrDefault(x => x.KupacID == refund.KupacID);
            wallet.balance += refund.Igra.Cijena;
            db.Remove(refund);
            db.Update(wallet);
            db.SaveChanges();
            return RedirectToAction(nameof(PrikazRefundova));

        }
        public IActionResult Odobri(int id)
        {
            var igra = db.Igra.SingleOrDefault(x => x.IgraID == id);
            igra.Odobrena = true;
            db.Update(igra);
            db.SaveChanges();
            return RedirectToAction(nameof(PregledIgara), new { DeveloperID = igra.DeveloperID });
        }

        public IActionResult Zabrani(int id)
        {
            var igra = db.Igra.SingleOrDefault(x => x.IgraID == id);
            igra.Odobrena = false;
            db.Update(igra);
            db.SaveChanges();
            return RedirectToAction(nameof(PregledIgara), new { DeveloperID = igra.DeveloperID });
        }

        public IActionResult GetZahtjeveZaPremium()
        {
            var lista = db.PrijavaPremium.Include(x => x.Kupac).ToList();
            return View(lista);
        }
        public IActionResult PrihvatiPremium(int id)
        {
            var zahtjev = db.PrijavaPremium.SingleOrDefault(x => x.Id == id);
            var kupac = db.Kupac.SingleOrDefault(x => x.KupacID == zahtjev.KupacId);
            kupac.PretplacenNaPremium = true;
            db.Update(kupac);
            db.Remove(zahtjev);
            db.SaveChanges();
            return RedirectToAction(nameof(GetZahtjeveZaPremium));
        }
        public IActionResult OdbijPremium(int id)
        {
            var zahtjev = db.PrijavaPremium.SingleOrDefault(x => x.Id == id);
            var kupac = db.Kupac.SingleOrDefault(x => x.KupacID == zahtjev.KupacId);
            kupac.PretplacenNaPremium = false;
            db.Update(kupac);
            db.Remove(zahtjev);
            db.SaveChanges();
            return RedirectToAction(nameof(GetZahtjeveZaPremium));
        }
        public IActionResult PregledReporta()
        {
            var reporti = db.Report.Select(x => new AdministratorPregledReportaVM
            {
                Id = x.Id,
                ReportovaniKupac = x.ReportovaniKupac.Osoba.Ime + " " + x.ReportovaniKupac.Osoba.Prezime,
                RazlogReporta = x.RazlogReporta,
                Reputacija = x.ReportovaniKupac.Reputacija,
                KupacId = x.ReportovaniKupac.KupacID
            }).ToList();
            return View(reporti);
        }
        public IActionResult BanujKupca(int id, int reportId)
        {
            var report = db.Report.SingleOrDefault(x => x.Id == reportId);
            var kupac = db.Kupac.SingleOrDefault(x => x.KupacID == id);
            kupac.BanStatus = true;
            db.Update(kupac);
            db.Remove(report);
            db.SaveChanges();
            return RedirectToAction(nameof(PregledReporta));
        }
        public IActionResult SmanjiReputaciju(int id, int reportId)
        {
            var report = db.Report.SingleOrDefault(x => x.Id == reportId);
            var kupac = db.Kupac.SingleOrDefault(x => x.KupacID == id);
            kupac.Reputacija -= 10;
            db.Update(kupac);
            db.Remove(report);
            db.SaveChanges();
            return RedirectToAction(nameof(PregledReporta));
        }
        
    }
}
