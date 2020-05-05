using System.Web;
using System.Web.Mvc;

namespace ODMS.Filters
{
    public class CacheControlFilter : ActionFilterAttribute
    {
        public CacheControlFilter(HttpCacheability cacheability)
        {
            _cacheability = cacheability;
        }

        private readonly HttpCacheability _cacheability;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //TODO:(Caching)
            /*
             * Sayfa bazlı cache yapılmak istendiğinde buradan engellendiği için cache yapamıyoruz.Bu yuzden kapattım .eger cache kullanılmaması gerekiyorsa zaten 
             * browser cache tutmayacak,kullanılması gerekiyorsa sayfa bazlı output cache kullanılması uygun olabilir.
             
             */

            //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            //cache.SetCacheability(_cacheability);
            //cache.SetExpires(DateTime.Now);
            //cache.SetAllowResponseInBrowserHistory(true);
            //cache.SetNoServerCaching();
            //cache.SetNoStore();

        }
    }
}