using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentDetailsParts
{
    public class AppointmentDetailsPartsListModel : BaseListWithPagingModel
    {
        public AppointmentDetailsPartsListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "APPOINTMENT_INDICATOR_CONTENT_ID"},
                     {"AppointIndicId", "APPOINTMENT_INDICATOR_ID"},
                     {"PartName", "PART_NAME"},
                     {"Quantity", "QUANTITY"},
                     {"ListPrice", "LIST_PRICE"}
                 };
            SetMapper(dMapper);

        }

        public AppointmentDetailsPartsListModel()
        {
        }

        public int Id { get; set; }
        public int AppointIndicId { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "AppointDetailsParts_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Quantity { get; set; }

        [Display(Name = "Global_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ListPrice { get; set; }

        [Display(Name = "Dealer_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

    }
}
