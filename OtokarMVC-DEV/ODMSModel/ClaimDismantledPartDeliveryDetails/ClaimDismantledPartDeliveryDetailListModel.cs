using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimDismantledPartDeliveryDetails
{
    public class ClaimDismantledPartDeliveryDetailListModel : BaseListWithPagingModel
    {
        public ClaimDismantledPartDeliveryDetailListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartName", "PART_NAME"},
                    {"PartCode", "PART_CODE"},
                    {"DismantledDate", "CREATE_DATE"},
                    {"Barcode", "BARCODE"},
                    {"Qty", "QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public ClaimDismantledPartDeliveryDetailListModel()
        {

        }

        public int ClaimWayBillId { get; set; }

        public int Id { get; set; }//Dynamic Id for barcode !

        public int ClaimDismantledPartId { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_DismantledDate", ResourceType = typeof(MessageResource))]
        public DateTime DismantledDate { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_Barcode", ResourceType = typeof(MessageResource))]
        public string Barcode { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_Qty", ResourceType = typeof(MessageResource))]
        public decimal Qty { get; set; }

        public bool IsExists { get; set; }

        public bool IsSelected { get; set; }

    }
}
