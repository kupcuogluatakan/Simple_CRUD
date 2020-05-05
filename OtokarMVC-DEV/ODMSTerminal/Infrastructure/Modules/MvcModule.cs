using System.Linq;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using ODMSCommon.Logging;
using ODMSTerminal.Controllers;
using ODMSTerminal.Infrastructure.MvcActionFilters;

namespace ODMSTerminal.Infrastructure.Modules
{
    using Autofac;
    public class MvcModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(ThisAssembly);
            builder.RegisterFilterProvider();
            builder.RegisterType<TransactionalAttribute>()
                .As<TransactionalAttribute>()
                .PropertiesAutowired()
                .InstancePerRequest();
        }
    }
}