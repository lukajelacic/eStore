using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eStore_web.ViewModel;
namespace eStore_web.Common
{
    public class PretragaIgra
    {
        public double cijenaSaPopustom(int igraID, eContext db)
        {
            
            Igra igra = db.Igra.Where(i => i.IgraID == igraID).FirstOrDefault();
            Popust popust = db.Popust.Where(p => p.PocetakPopusta <= DateTime.Now && p.KrajPopusta >= DateTime.Now && p.IgraID == igraID).FirstOrDefault();
            if (igra == null)
                return 0;
           
                

            if(popust!=null)
            {

                float procent = 1 - (float)popust.PopustProcent / 100;

               return Math.Round((igra.Cijena * procent), 2);


            }



            return igra.Cijena;
        }
        public int IgraAktivanPopust(int igraID, eContext db)
        {

            Igra igra = db.Igra.Where(i => i.IgraID == igraID).FirstOrDefault();
            Popust popust = db.Popust.Where(p => p.PocetakPopusta <= DateTime.Now && p.KrajPopusta >= DateTime.Now && p.IgraID == igraID).FirstOrDefault();
            if (igra == null || popust==null)
                return 0;
            return popust.PopustProcent;
        }
        public List<Igra> PretragaZanrKategorija(List<Igra> igre, int zanr = 0, int kategorija = 0, eContext db=null)
        {
            if(db==null)
            {
                db = new eContext();
            }
            if (zanr != 0 && kategorija != 0)
            {
                var query =
                    from igra in igre
                    where igra.GameGenreID == zanr && igra.RatingCategorieID == kategorija
                    select igra;
                igre = query.ToList();
            }

            else
            {
                if (zanr != 0 && kategorija == 0)
                {
                    var query =
                     from igra in igre
                     where igra.GameGenreID == zanr
                     select igra;
                    igre = query.ToList();
                }
                else
                {
                    if (zanr == 0 && kategorija != 0)
                    {
                        var query =
                         from igra in igre
                         where igra.RatingCategorieID == kategorija
                         select igra;
                        igre = query.ToList();
                    }

                }
            }
            return igre;
        }
        public List<Igra> PretragaIgrica(string pretraga,eContext db)
        {
            if(pretraga==null || pretraga=="")
            {
                return db.Igra
                    .Include(i=>i.IgricaImage)
                    .ToList();
            }
            List<Igra> igre = db.Igra
                .Include(i => i.IgricaImage)
                .ToList();
            List<int> PretragaPogodatak = new List<int>();
            List<Igra> PretragaIgra = new List<Igra>();
            int brojac = 0, PP;
            Igra PI;
            if (pretraga != null && igre.Count != 0)
            {

                List<Regex> r = new List<Regex>();
                pretraga = pretraga.ToLower();
                string[] k = Regex.Split(pretraga, "\\s+");

                foreach (string x in k)
                {
                    r.Add(new Regex("\\b" + x + "\\b"));
                }

                //Prebrojavamo koliko naziv svake igre ima istih rjeci kao pretraga string
                for (int i = 0; i < igre.Count; i++)
                {
                    brojac = 0;
                    foreach (Regex reg in r)
                    {
                        if (reg.Match(igre[i].Naziv.ToLower()).Success)
                        {
                            if (brojac == 0)
                            {
                                PretragaIgra.Add(igre[i]);
                            }
                            brojac++;

                        }
                    }
                    if (brojac != 0)
                        PretragaPogodatak.Add(brojac);
                }

                for (int i = 0; i < PretragaIgra.Count; i++)
                {
                    for (int j = i + 1; j < PretragaIgra.Count; j++)
                    {
                        if (PretragaPogodatak[j] > PretragaPogodatak[i])
                        {
                            PP = PretragaPogodatak[i];
                            PretragaPogodatak[i] = PretragaPogodatak[j];
                            PretragaPogodatak[j] = PP;

                            //-------------------------------------

                            PI = PretragaIgra[i];
                            PretragaIgra[i] = PretragaIgra[j];
                            PretragaIgra[j] = PI;

                        }
                    }
                }

                return PretragaIgra;
            }
            return new List<Igra>();
        }
        public List<Igra> Sortiranje(List<Igra> igre, int sortirajPo = 1, eContext db=null)
        {
            if(db==null)
            {
                db = new eContext();
            }
            switch (sortirajPo)
            {
                case 2:
                    {
                        var queryIgra =
                        from igra in igre
                        where igra.Odobrena == true
                        orderby igra.DatumObjave descending
                        select igra;


                        igre = queryIgra.ToList();

                    }
                    break;
                case 1:
                    {
                        var queryIgra =
                        from igra in igre
                        join kupacKupuje in db.KupacKupuje on igra.IgraID equals kupacKupuje.IgraID
                        into pk
                        from kupacKupuje in pk.DefaultIfEmpty()
                        where igra.Odobrena == true
                        group igra by igra into g
                        orderby g.Count() descending
                        select g.Key;


                        igre = queryIgra.ToList();
                    }
                    break;

                case 3:
                    {
                        var queryIgra =
                        from igra in igre
                        where igra.Odobrena == true
                        orderby cijenaSaPopustom(igra.IgraID, db) ascending
                        select igra;

                        igre = queryIgra.ToList();
                    }
                    break;
                case 4:
                    {
                        var queryIgra =
                        from igra in igre
                        where igra.Odobrena == true
                        orderby cijenaSaPopustom(igra.IgraID,db) descending
                        select igra;

                        igre = queryIgra.ToList();
                    }
                    break;


                case 5:
                    {
                        var queryIgra =
                        from igra in igre
                        where igra.Odobrena == true
                        orderby IgraAktivanPopust(igra.IgraID, db) descending
                        select igra;

                        igre = queryIgra.ToList();
                    }
                    break;



                default: break;
            }
            return igre;
        }


        public KupacIndexVM GetKupacIndexVM(string pretraga = null, int sortirajPo = 1, int zanr = 0, int kategorija = 0, int from = 0, int to = 20, eContext db=null)
        {
            KupacIndexVM kupacIndexVM = new KupacIndexVM()
            {
                pretraga = pretraga,
                sortirajPo = sortirajPo,
                zanr = zanr,
                kategorija = kategorija,
                from = from,
                to = to
            };
            PretragaIgra p = new PretragaIgra();
            //pretrazivanje
            List<Igra> igre = p.PretragaIgrica(kupacIndexVM.pretraga, db);
            //filtiranje
            igre = p.PretragaZanrKategorija(igre, kupacIndexVM.zanr, kupacIndexVM.kategorija, db);
           //sortiranje
            igre = p.Sortiranje(igre, kupacIndexVM.sortirajPo, db);



            //to -= from;
            to = 20;
            if (from + to < igre.Count)
            {
                igre = igre.GetRange(from, to);
            }
            else
            {
                to = igre.Count() - from;
                if (to > 0)
                    igre = igre.GetRange(from, to);
                else
                    igre = new List<Igra>();
            }
            kupacIndexVM.Igre = igre;

            foreach (Igra x in igre)
            {
                kupacIndexVM.PopustProcent.Add(p.IgraAktivanPopust(x.IgraID, db));
                kupacIndexVM.novaCijena.Add(p.cijenaSaPopustom(x.IgraID, db));
            }

            return kupacIndexVM;
        }

    }
}
