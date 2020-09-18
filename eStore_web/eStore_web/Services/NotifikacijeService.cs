using eStore_web.EF;
using eStore_web.EF_Models;
using eStore_web.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Services
{
    public class NotifikacijeService : INotifikacije
    {

        private readonly eContext _db = new eContext();
        private readonly IHubContext<SignalRHub> _hub;
        private readonly IMyUser _myUser;

        public NotifikacijeService(IHubContext<SignalRHub> hub, IMyUser myUser)
        {
            _hub = hub;
            _myUser = myUser;
        }
        public Task getAllNotifikacije(int br, string connectionId)
        {
            var userTip = _db.LoginInfo.Where(x => x.SignalRToken == connectionId).Select(x => x.TipKorisnika).FirstOrDefault();
            List<NotifikacijaVM> notifikacije = null;
            if (userTip == 3)
            {
                notifikacije = _db.Notifikacija.OrderBy(x => x.Vrijeme).Where(x => x.UserTo == null).Select(x => new NotifikacijaVM()
                {
                    NotifikacijaId = x.NotifikacijaID,
                    Poruka = x.Poruka,
                    User = x.UserFrom.Ime + " " + x.UserFrom.Prezime,
                    Vrijeme = x.Vrijeme.ToString("hh:mm"),
                    Url = x.URL
                }).Take(br).ToList();
            }
            else
            {
                notifikacije = _db.Notifikacija.OrderBy(x => x.Vrijeme).Where(x => x.UserTo.LoginInfo.SignalRToken == connectionId).Select(x => new NotifikacijaVM()
                {
                    NotifikacijaId = x.NotifikacijaID,
                    Poruka = x.Poruka,
                    User = x.UserFrom.Ime + " " + x.UserFrom.Prezime,
                    Vrijeme = x.Vrijeme.ToString("hh:mm"),
                    Url = x.URL
                }).Take(br).ToList();
            }

            return _hub.Clients.Client(connectionId).SendAsync("getAllNotifikacije", notifikacije);
        }

        public async void posaljiNotifikacije(int to, int from, NotifikacijaVM message)
        {
            var userTo = await _myUser.getUserById(to);
            var userFrom = await _myUser.getUserById(from);

            var vrijeme = DateTime.Now;
            message.User = userFrom.Ime + " " + userFrom.Prezime;
            message.Vrijeme = vrijeme.ToString("hh:mm:ss");
            var temp = new Notifikacije()
            {
                Poruka = message.Poruka,
                URL = message.Url,
                UserFromID = from,
                UserToID = to,
                Vrijeme = vrijeme
            };
            _db.Notifikacija.Add(temp);

            _db.SaveChanges();

            message.NotifikacijaId = temp.NotifikacijaID;
            await _hub.Clients.Clients(userTo.SignalRToken).SendAsync("GetNotification", message);
        }

        public async void posaljiNotifikacijeAdminu(int from, NotifikacijaVM message)
        {
            List<string> admins = _db.LoginInfo.Where(x => x.TipKorisnika == 3).Select(x => x.SignalRToken).ToList();
            var userFrom = await _myUser.getUserById(from);

            var vrijeme = DateTime.Now;
            message.User = userFrom.Ime + " " + userFrom.Prezime;
            message.Vrijeme = vrijeme.ToString("hh:mm:ss");
            var temp = new Notifikacije()
            {
                Poruka = message.Poruka,
                URL = message.Url,
                UserFromID = from,
                UserToID = null,
                Vrijeme = vrijeme
            };
            _db.Notifikacija.Add(temp);

            _db.SaveChanges();

            //message.NotifikacijaId = temp.NotifikacijaID;
            await _hub.Clients.Clients(admins).SendAsync("GetNotification", message);
        }
    }
}