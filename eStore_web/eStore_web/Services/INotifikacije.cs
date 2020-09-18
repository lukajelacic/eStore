using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.Services
{
    public interface INotifikacije
    {
        void posaljiNotifikacije(int to, int from, NotifikacijaVM message);
        void posaljiNotifikacijeAdminu(int from, NotifikacijaVM message);
        Task getAllNotifikacije(int br, string connectionID);

    }
    public class NotifikacijaVM
    {
        public string Poruka { get; set; }
        public string Vrijeme { get; set; }
        public string User { get; set; }
        public string Url { get; internal set; }
        public int NotifikacijaId { get; internal set; }
    }
}
