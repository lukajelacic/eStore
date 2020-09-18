using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using eStore_web.Common;
using Microsoft.AspNetCore.Http;
using eStore_web.EF;

namespace eStore_web.Common
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool Kupac, bool Developer, bool Administrator, bool Svi)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { Kupac, Developer, Administrator, Svi };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool Kupac, bool Developer, bool Administrator, bool Svi)
        {
            _Kupac = Kupac;
            _Developer = Developer;
            _Administrator = Administrator;
            _Svi = Svi;
        }
        private readonly bool _Kupac;
        private readonly bool _Developer;
        private readonly bool _Administrator;
        private readonly bool _Svi;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            LoginInfo k = filterContext.HttpContext.Session.GetObjectFromJson<LoginInfo>("LoggedUser");



            if (_Svi == true)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            if (k == null && _Svi != true)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Login", "Autentifikacija", new { @area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            
            eContext db = new eContext();

            //kupci
            if (_Kupac == true && db.Kupac.Where(s => s.OsobaID == k.OsobaID).FirstOrDefault() != null)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //developeri
            if (_Developer && db.Developer.Where(s => s.OsobaID == k.OsobaID).FirstOrDefault() != null)
            {
                await next();//ok - ima pravo pristupa
                return;
            }
            if (_Administrator && db.Administrator.Where(s => s.OsobaID == k.OsobaID).FirstOrDefault() != null)
            {
                await next();//ok - ima pravo pristupa
                return;
            }




            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Kupac", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }



    }
}