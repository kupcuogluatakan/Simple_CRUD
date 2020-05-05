using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorFailureCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class AppointmentIndicatorFailureCodeReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Belirti Sebep Kodu Tanımlama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new AppointmentIndicatorFailureCodeListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var appointmentIndicatorFailureCodeBo = new AppointmentIndicatorFailureCodeBL();
            int totalCnt = 0;
            return appointmentIndicatorFailureCodeBo.ListAppointmentIndicatorFailureCode(UserInfo,(Filter as AppointmentIndicatorFailureCodeListModel), out totalCnt).Data;
        }
    }
}
