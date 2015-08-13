using Core.Application;
using Core.DI;
using Core.Security;
using Core.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace WebApplicaton
{
    public class MvcApplication : System.Web.HttpApplication, IApplicaton
    {

        private static IAppSession _appSession;


        public IAppSession AppSession
        {
            get
            {
                if (_appSession == null)
                {
                    if (HttpContext.Current.Session != null)
                    {
                        _appSession = new AppSession();
                    }
                }

                return _appSession;
            }
        }

        protected void Application_Start()
        {
            UnityBootstrapper.Initialize();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            _appSession = new AppSession();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            if (_appSession != null)
                _appSession.Dispose();

            _appSession = null;
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;

                        // Get the stored user-data, in this case, GreenhouseCookieData
                        string userData = ticket.UserData;
                        AppCookieData data = JsonConvert.DeserializeObject<AppCookieData>(ticket.UserData);

                        //To do: create converter for azzemblyCookie data which contains application user data also
                        //data.GhaUser = JsonConvert.DeserializeObject<UserDO>(ticket.UserData, new JsonUserConverter());

                        //Condition to check the null reference of azzembly cookie data
                        if (data != null)
                        {
                            AppIdentity user = new AppIdentity(id, data);
                            HttpContext.Current.User = user;
                            System.Threading.Thread.CurrentPrincipal = user;
                            Context.User = user;
                        }

                    }
                }
            }
        }
    }
}
