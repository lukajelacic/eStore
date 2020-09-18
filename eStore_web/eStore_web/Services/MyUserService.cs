using eStore_web.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Services
{
    public class MyUserService : IMyUser
    {
        eContext db;
        public MyUserService()
        {
            db = new eContext();
        }
        public async Task<SignalRUser> getUserById(int id)
        {
            var user = db.LoginInfo.Where(x => x.OsobaID == id).Select(x => new SignalRUser()
            {
                OsobaID = x.OsobaID,
                Ime = x.Osoba.Ime,
                Prezime = x.Osoba.Prezime,
                SignalRToken = x.SignalRToken

            }).FirstOrDefault();

            return user;
        }

        public async Task<bool> updateUser(int id, string token)
        {
            var user = db.LoginInfo.Where(x => x.OsobaID == id).FirstOrDefault();
            if (user != null)
            {
                user.SignalRToken = token;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
