using System;
using System.Linq;
using eStore_web.EF;
using eStore_web.EF_Models;



namespace eStore_web.Common
{
    public class WishListManagment
    {
        eContext db = new eContext();

        public bool ImaWishList(int IgraID,int KupacID)
        {
            WishList wishList = db.WishList.Where(w => w.IgraID == IgraID && w.KupacID == KupacID).FirstOrDefault();

            if (wishList != null)
                return true;
            return false;
        }

        public bool RemoveWishList(int IgraID,int KupacID)
        {
            if (!ImaWishList(IgraID, KupacID))
                return false;
            db.WishList.Remove(db.WishList.Where(w => w.IgraID == IgraID && w.KupacID == KupacID).FirstOrDefault());
            db.SaveChanges();
            db.Dispose();
            return true;
        }

        public bool AddWishList(int IgraID, int KupacID)
        {
            if (ImaWishList(IgraID, KupacID))
                return false;

            WishList wishList = new WishList()
            {
                DatumDodavanja = DateTime.Now,
                KupacID = KupacID,
                IgraID = IgraID
            };
            db.WishList.Add(wishList);
            db.SaveChanges();
            db.Dispose();
            return true;
        }
    }
}
