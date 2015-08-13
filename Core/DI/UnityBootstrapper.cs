using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.DI
{
    public partial class UnityBootstrapper
    {

        public static void Initialize()
        {
            var container = ConfigureContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static IUnityContainer ConfigureContainer()
        {

            var container = new UnityContainer();
            //container.RegisterType<IPopUpNotificationRepository, PopUpNotificationRepository>();
            
            return container;

        }

    }
}
