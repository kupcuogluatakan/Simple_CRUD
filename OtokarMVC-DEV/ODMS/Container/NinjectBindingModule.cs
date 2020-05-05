using Ninject.Modules;
using ODMS.Controllers;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSData;

namespace ODMS.Container
{
    public class NinjectBindingModule : NinjectModule
    {
        public override void Load()
        {
            
            ///Business Layer
            Kernel.Bind<IIntegrationBL>().To<IntegrationBL>();
            Kernel.Bind<ReportCreateBL>().To<ReportCreateBL>();
            Kernel.Bind<ReportsBL>().To<ReportsBL>();
            Kernel.Bind<SystemAdministrationBL>().To<SystemAdministrationBL>();
            

            //Data Layer
            Kernel.Bind<SystemAdministrationData>().To<SystemAdministrationData>();
            Kernel.Bind<ReportsData>().To<ReportsData>();
            

            //Controller
            Kernel.Bind<IntegrationController>().To<IntegrationController>();


            //Filter
            //Kernel.Bind<SessionExpireFilter>().To<SessionExpireFilter>();
            //Kernel.Bind<ErrorHandlingFilter>().To<ErrorHandlingFilter>();
            //Kernel.Bind<UserInfoFilter>().To<UserInfoFilter>();
            //Kernel.Bind<CacheControlFilter>().To<CacheControlFilter>();
            //Kernel.Bind<PreventDirectFilter>().To<PreventDirectFilter>();
            //Kernel.Bind<AuthorizationFilter>().To<AuthorizationFilter>();



        }
    }
}