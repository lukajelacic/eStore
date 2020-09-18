using System;
using System.Linq;
using eStore_web.EF;
using eStore_web.EF_Models;

namespace eStore_web.Common
{
    public class PratiManagment
    {
        eContext db = new eContext();



        public bool PratiIgru(int IgraID,int KupacID)
        {
            Prati pratiIgru = db.Prati.Where(p => p.IgraID == IgraID && p.KupacID == KupacID).FirstOrDefault();

            if (pratiIgru !=null)
                return true;
            return false;
        }
        public bool AddPrati(int IgraID, int KupacID)
        {
            if (PratiIgru(IgraID, KupacID) == true)
                return false;
            Prati pratiIgru = new Prati
            {
                IgraID = IgraID,
                KupacID = KupacID
            };
            db.Prati.Add(pratiIgru);
            db.SaveChanges();
            db.Dispose();
            return true;
        }
        public bool RemovePrati(int IgraID, int KupacID)
        {
            if (PratiIgru(IgraID, KupacID) == false)
                return false;

            db.Prati.Remove(db.Prati.Where(p => p.IgraID == IgraID && p.KupacID == KupacID).FirstOrDefault());
            db.SaveChanges();
            db.Dispose();
            return true;
        }
    }
}
