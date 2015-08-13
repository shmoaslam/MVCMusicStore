using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Session
{
    public interface IAppSession
    {
        void Dispose();

        T GetSessionValue<T>(SessionKeys sessionKey);

        void SetSessionValue<T>(SessionKeys sessionKey, T theValue);

        void ClearSessionValue(SessionKeys sessionKey);

        bool SessionEntryExists(SessionKeys sessionKey);
    }
}
