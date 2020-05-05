using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;

namespace ODMSService.Helpers
{
    internal class GosErrorEmailSender
    {

        public void Send(IEnumerable<ServiceCallScheduleErrorListModel> errors, string title = "SERVİS ÇALIŞMA HATALARI")
        {
            var emails = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.GosFailEmailList).Model;
            CommonBL.SendDbMail(emails, title, GenerateErrorMailBody(errors, title));
        }
        private string GenerateErrorMailBody(IEnumerable<ServiceCallScheduleErrorListModel> errors, string title)
        {
            var sb = new StringBuilder();
            sb.Append(@"<html>
<head>
<style type='text/css'>
body{
	font:12px Arial;
}

</style>
<title>
").Append(title).Append(@"
</title>
</head>
<body>
<h3>").Append(title).Append(@"</h3>
<div>
<p> Servis çalışması sonuçu aşağıdaki kayıtlarda hata bulunmuştur. Bilginize. <em>SYS Mail Sistemi</em></p>
<table>");
            foreach (var item in errors)
            {
                sb.Append($"<tr><td>{item.Action}-{item.Error}</td></tr>");
            }


            sb.Append(@"
</table>
</div>
</body>
</html>");
            return sb.ToString();
        }
    }
}
