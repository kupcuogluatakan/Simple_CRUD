using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using ODMSCommon;

namespace ODMSModel.LabourPrice
{
    public class LabourPriceListModel : BaseListWithPagingModel
    {
        [Display(Name = "VehicleModel_Display_LabourPriceId", ResourceType = typeof(MessageResource))]
        public string LabourPriceId { get; set; }
        public int HasTSLabourPriceId { get; set; }
        public int HasNoTSLabourPriceId { get; set; }
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }
        [Display(Name = "VehicleGroup_Display_ModelKod", ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupName { get; set; }
        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public int DealerRegionId { get; set; }
        [Display(Name = "LabourPrice_Display_ValidFromDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidFromDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidEndDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidEndDate { get; set; }
        [Display(Name = "LabourPrice_Display_ValidDate", ResourceType = typeof(MessageResource))]
        public DateTime ValidDate { get; set; }
        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }


        [Display(Name = "Dealer_Display_DealerClass", ResourceType = typeof(MessageResource))]
        public string DealerClass { get; set; }
        [Display(Name = "Dealer_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "LabourPrice_Display_LabourPriceType", ResourceType = typeof(MessageResource))]
        public string LabourPriceType { get; set; }
        [Display(Name = "LabourPrice_Display_UnitPrice", ResourceType = typeof(MessageResource))]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int _IsActive { get { return IsActive.GetValue<int>(); } }
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public int VehicleGroupId { get; set; }
        [Display(Name = "LabourPrice_Display_LabourPriceType", ResourceType = typeof(MessageResource))]
        public int LabourPriceTypeId { get; set; }

        public LabourPriceListModel()
        { }
        public LabourPriceListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"ModelName", "VM.MODEL_NAME"},
                     {"DealerClass", "DEALER_CLASS_NAME"},//"LB.DEALER_CLASS_LOOKVAL"
                     {"ValidFromDate", "LB.VALID_START_DATE"},
                     {"ValidEndDate", "LB.VALID_END_DATE"},
                     {"LabourPriceType", "LABOUR_TYPE_DESC"},//"LB.LABOUR_PRICE_TYPE"},
                     {"VehicleGroupName", "VGL.VHCL_GRP_NAME"},
                     {"DealerRegionName", "DR.DEALER_REGION_NAME"},
                     {"HasTsPaper", "LB.TS_CERT_CHCK"},
                     {"CurrencyCode", "LB.CURRENCY_CODE"},
                     {"UnitPrice", "LB.UNIT_PRICE"},
                     {"IsActiveString","LB.IS_ACTIVE"},
                     {"HasTSUnitPrice","TS_UNIT_PRICE"},
                     {"HasNoTSUnitPrice","NOTS_UNIT_PRICE"},
                     {"LabourPriceId","LB.ID_LABOUR_PRICE" }
                 };
            SetMapper(dMapper);

        }
        [Display(Name = "LabourPrice_Display_HasTsPage", ResourceType = typeof(MessageResource))]
        public bool HasTsPaper { get; set; }
        public bool? SearchHasTsPaper { get; set; }
        //public string DealerClassLookKeyVal { get; set; }

        [Display(Name = "LabourPrice_Display_HasTSUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal HasTSUnitPrice { get; set; }

        [Display(Name = "LabourPrice_Display_HasNoTSUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal HasNoTSUnitPrice { get; set; }
    }
}
