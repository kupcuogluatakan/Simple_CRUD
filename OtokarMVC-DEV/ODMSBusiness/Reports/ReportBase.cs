using System.Configuration;
using ODMSBusiness.ReportService;

namespace ODMSBusiness.Reports
{
    public class ReportBase
    {
        protected ReportServiceSoapClient Client;

        protected UserCredentials Credentials;

        protected string Language;

        public ReportBase()
        {
            Client = new ReportServiceSoapClient("ReportServiceSoap");
            var user = ODMSCommon.Security.UserManager.UserInfo;
            Language = ODMSCommon.Security.UserManager.LanguageCode;
            Credentials = new UserCredentials
            {
                password = ODMSCommon.CommonUtility.EncryptSymmetric(user.Password),
                userId = user.UserId
            };
        }


    }
}
