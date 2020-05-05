using System.Web.Mvc;
using System.Web.Routing;

namespace ODMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProposalIndex",
                url: "Proposal/ProposalIndex",
                defaults: new { controller = "Proposal_", action = "ProposalIndex" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}