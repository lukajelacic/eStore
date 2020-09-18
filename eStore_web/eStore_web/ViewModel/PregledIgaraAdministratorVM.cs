using eStore_web.EF_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class PregledIgaraAdministratorVM
    {
        public List<Igra> Igre { get; set; }
        public string Developer { get; set; }
    }
}
