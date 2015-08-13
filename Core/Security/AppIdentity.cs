using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public partial class AppIdentity : IPrincipal
    {
        public AppCookieData CookieData { get; set; }

        public AppIdentity(IIdentity identity, AppCookieData data)
        {
            if (data != null)
            {
                Identity = identity;
                CookieData = data;
            }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
