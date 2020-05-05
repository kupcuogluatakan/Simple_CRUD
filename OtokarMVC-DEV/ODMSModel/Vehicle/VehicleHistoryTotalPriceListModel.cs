using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Vehicle
{
    public class VehicleHistoryTotalPriceListModel : BaseListWithPagingModel
    {
        public VehicleHistoryTotalPriceListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ProcessType", "PROCESS_TYPE_NAME"},
                    {"IndicatorType", "INDICATOR_TYPE_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"HistoryDate","HISTORY_DATE"},
                    {"VehicleKM", "VEHICLE_KM"},
                    {"HistoryName","HISTORY_NAME"}
                };
            SetMapper(dMapper);

        }

        public VehicleHistoryTotalPriceListModel()
        {
        }

        public int VehicleId { get; set; }

        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeCode { get; set; }

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeName { get; set; }
        
        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorTypeCode { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorTypeName { get; set; }

        [Display(Name = "VehicleHistoryTotalPrice_Display_CustomerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerPrice { get; set; }

        [Display(Name = "VehicleHistoryTotalPrice_Display_OtokarPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OtokarPrice { get; set; }

        [Display(Name = "VehicleHistoryTotalPrice_Display_CustomerTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerTotalPrice { get; set; }

        [Display(Name = "VehicleHistoryTotalPrice_Display_OtokarTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OtokarTotalPrice { get; set; }

        [Display(Name = "VehicleHistoryTotalPrice_Display_TotalCustomerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalCustomerPrice { get; set; }
        [Display(Name = "VehicleHistoryTotalPrice_Display_TotalOtokarPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalOtokarPrice { get; set; }
    }
}
