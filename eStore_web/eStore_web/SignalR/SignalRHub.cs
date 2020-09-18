using eStore_web.Common;
using eStore_web.EF;
using eStore_web.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.SignalR
{

    public class SignalRHub : Hub
    {
        private readonly IMyUser _user;
        private readonly eContext _db = new eContext();
        private readonly INotifikacije _notifikacijaService;


        public SignalRHub(IMyUser user, INotifikacije notifikacija)
        {
            _notifikacijaService = notifikacija;

            _user = user;
        }


        public Task getNotification(int brNoti)
        {
            return _notifikacijaService.getAllNotifikacije(brNoti, Context.ConnectionId);
        }


        public override Task OnConnectedAsync()
        {
            var Userid = Autentifikacija.GetLogiraniKorisnik(Context.GetHttpContext()).OsobaID;
            //var y = Context.User;
            var ConnectionId = Context.ConnectionId;

            var x = _user.updateUser(Userid, ConnectionId).Result;

            if (x == true)
            {
                return base.OnConnectedAsync();

            }
            return null;
        }

    }
}