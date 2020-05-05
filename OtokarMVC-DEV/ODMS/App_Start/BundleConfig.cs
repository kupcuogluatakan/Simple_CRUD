using System.Web.Optimization;

namespace ODMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // The jQuery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-1.9*")
                            .Include("~/Scripts/jquery.cookie.js")
                            .Include("~/Scripts/jquery.validate.js")
                            .Include("~/Scripts/jquery.validate.unobtrusive.min.js")
                            //.Include("~/Scripts/jquery.validate.unobtrusive.validationgroups.js")
                            .Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // The Kendo JavaScript bundle
            bundles.Add(new ScriptBundle("~/bundles/kendo")
                 .Include("~/Scripts/kendo/kendo.all.min.js")
                 .Include("~/Scripts/kendo/kendo.aspnetmvc.min.js")
                    .Include("~/Scripts/Common/odms.*")
            );

            // The Kendo CSS bundle
            bundles.Add(new StyleBundle("~/Content/kendo/css")
                  .Include("~/Content/kendo/kendo.common.*")
                  .Include("~/Content/kendo/kendo.bootstrap.*")
                  .Include("~/Content/fontawesome/css/font-awesome.min.css")
            );

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/odms/css/commons")
                .Include("~/Style/Content.css"));

            // Clear all items from the ignore list to allow minified CSS and JavaScript files in debug mode
            bundles.IgnoreList.Clear();

            // Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            BundleTable.EnableOptimizations = true;
        }
    }
}