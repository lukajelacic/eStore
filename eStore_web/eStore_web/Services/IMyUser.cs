using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Services
{
    public interface IMyUser
    {
        Task<SignalRUser> getUserById(int id);
        Task<bool> updateUser(int id, string token);
    }

    public class SignalRUser
    {
        public int OsobaID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string SignalRToken { get; set; }

    }
}
