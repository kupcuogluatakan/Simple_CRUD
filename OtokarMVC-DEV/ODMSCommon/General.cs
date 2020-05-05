using System.Web;
using System.Configuration;

namespace ODMSCommon
{
    public class General
    {
        /// <summary>
        /// Aktif database server bilgisini verir.
        /// </summary>
        public static string Server
        {
            get
            {
                if (HttpContext.Current.Session["ActiveServer"] == null)
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["OtokarDMS_DB"].ConnectionString);
                    HttpContext.Current.Session["ActiveServer"] = builder.DataSource;
                }
                return HttpContext.Current.Session["ActiveServer"].ToString();
            }
        }

        /// <summary>
        /// Aktif database adını verir.
        /// </summary>
        public static string DatabaseName
        {
            get
            {
                if (HttpContext.Current.Session["ActiveDatabaseName"] == null)
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["OtokarDMS_DB"].ConnectionString);
                    HttpContext.Current.Session["ActiveDatabaseName"] = builder.InitialCatalog;
                }
                return HttpContext.Current.Session["ActiveDatabaseName"].ToString();
            }
        }

        /// <summary>
        /// Test ortamı mı?
        /// </summary>
        public static bool IsTest
        {
            get
            {
                return !(DatabaseName == "Otokar-DMS");
            }
        }

        /// <summary>
        /// Kaynak
        /// </summary>
        public static string Source
        {
            get
            {
                if (ConfigurationManager.AppSettings["Source"] != null)
                    return ConfigurationManager.AppSettings["Source"].ToString();

                return string.Empty;
            }
        }

    }
}