using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class KilometerReportInfoModel
    {
        
        
        public string DEALER_NAME { get; set; }
        public string DEALER_REGION_NAME { get; set; }
        public string MODEL_KOD { get; set; }
        public string ID_WORK_ORDER { get; set; }
        public string ID_WORK_ORDER_DETAIL { get; set; }
        public decimal SSID_GUARANTEE { get; set; }
        public string VIN_NO { get; set; }
        public string ID_VEHICLE { get; set; }
        public string ENGINE_NO { get; set; }
        public string VEHICLE_WARRANTY_START_DATE { get; set; }
        public string CREATE_DATE { get; set; }
        public string VEHICLE_KM { get; set; }
        public string ID_DEALER { get; set; }
        public string ID_DEALER_REGION { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string ID_TYPE { get; set; }
        public string PartPrice { get; set; }
        public decimal LaborPrice { get; set; }
        public string CurrencyCode { get; set; }

    }
}
