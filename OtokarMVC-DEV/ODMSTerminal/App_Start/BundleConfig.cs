namespace ODMSTerminal
{

    using System.Web;
    using System.Web.Optimization;
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/Json2.js",
                       "~/Scripts/jquery-1.7.0.min.js",
                       "~/Scripts/custom/ks.*"
                       ));
#if (DEBUG)
{
            BundleTable.EnableOptimizations = false;
}
#else
{
            BundleTable.EnableOptimizations = true;
}
#endif

        }
    }
}