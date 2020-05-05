using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.ReportService;

namespace ReportsServiceTests
{
   public static class Helper
    {
       public static string GetReportsSavePath()
       {
           return @"C:\ODMSReports\Test";
       }
        public static  UserCredentials GetCredentials()
        {
            var credentials= new UserCredentials
            {
                password = ODMSCommon.CommonUtility.EncryptSymmetric("0osman42"),
                userId = 3112
            };
            return credentials;
        }
}
}
