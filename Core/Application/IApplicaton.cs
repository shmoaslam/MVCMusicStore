using Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application
{
    public interface IApplicaton
    {
        IAppSession AppSession { get; }
    }
}
