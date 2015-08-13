using Core.Application;
using Core.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Core
{
    public class BaseController : Controller
    {
        #region Field

        public IApplicaton Application
        {
            get
            {
                return base.ControllerContext.HttpContext.ApplicationInstance as IApplicaton;
            }
        }

        public IAppSession WebSession
        {
            get
            {
                return Application.AppSession;
            }
        }
        #endregion

        #region Method

        public void LogException(Exception ex)
        {
        }

        [NonAction]
        protected void LogOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
        }

        [NonAction]
        protected void Login()
        {
        }

        protected T DeserializeData<T>(string data) where T : class
        {
            T obj = default(T);
            if (!string.IsNullOrEmpty(data))
            {
                obj = JsonConvert.DeserializeObject<T>(data);
            }
            return obj;
        }

        protected string Serialize<T>(T obj) where T : class
        {
            string serializeResult = null;
            if (obj != null)
            {
                serializeResult = JsonConvert.SerializeObject(obj);
            }
            return serializeResult;
        }

        #endregion
    }
}
