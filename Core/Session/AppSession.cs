using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Session
{
    public partial class AppSession : IAppSession
    {
        private static Dictionary<string, object> _session = new Dictionary<string, object>();

        public void Dispose()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Abandon();
            }
            else
            {
                _session = null;
            }
        }

        public T GetSessionValue<T>(SessionKeys sessionKey)
        {
            string keyString = sessionKey.ToString();
            HttpContext theContext = HttpContext.Current;

            if (theContext != null && theContext.Session != null)
            {
                if (theContext.Session[keyString] != null)
                    return (T)theContext.Session[keyString];
                else
                    return default(T);
            }
            else
                throw new ApplicationException("HttpContext.Current or HTTPContext.Current.Session is NULL.");
        }

        public void SetSessionValue<T>(SessionKeys sessionKey, T theValue)
        {
            string keyString = sessionKey.ToString();
            HttpContext theContext = HttpContext.Current;

            if (theContext != null && theContext.Session != null)
            {
                theContext.Session[keyString] = theValue;
            }
            else
                throw new ApplicationException("HttpContext.Current or HTTPContext.Current.Session is NULL.");
        }

        public void ClearSessionValue(SessionKeys sessionKey)
        {
            string keyString = sessionKey.ToString();
            HttpContext theContext = HttpContext.Current;

            if (theContext != null && theContext.Session != null)
            {
                theContext.Session[keyString] = null;
            }
            else
                throw new ApplicationException("HttpContext.Current or HTTPContext.Current.Session is NULL.");
        }

        public bool SessionEntryExists(SessionKeys sessionKey)
        {
            HttpContext theContext = HttpContext.Current;

            if (theContext != null && theContext.Session != null)
            {
                if (theContext.Session[sessionKey.ToString()] == null)
                    return false;
                else
                    return true;
            }
            else
                throw new ApplicationException("HttpContext.Current or HTTPContext.Current.Session is NULL.");
        }
    }
}
