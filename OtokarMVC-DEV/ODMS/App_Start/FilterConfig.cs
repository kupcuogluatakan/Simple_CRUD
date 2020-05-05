using Ninject;
using System.Web;
using System.Web.Mvc;

namespace ODMS
{
    public class FilterConfig
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Filters.UserInfoFilter());
            filters.Add(new Filters.CacheControlFilter(HttpCacheability.NoCache));
            //filters.Add(new Filters.ErrorHandlingFilter
            //{
            //    ExceptionType = typeof(ODMSAuthenticationException),
            //    View = "../SystemAdministration/Login"
            //}, 2);

            filters.Add(new Filters.ErrorHandlingFilter());
            filters.Add(new Filters.SessionExpireFilter());
        }
    }
}