using System;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSplitting
{
    public class SparePartSplittingListModel:BaseListWithPagingModel
    {
        [Display(Name = "SPSplitting_Display_Group", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupId { get; set; }

        [Display(Name = "SPSplitting_Display_RankNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RankNo { get; set; }

        public string OldPartId { get; set; }
        [Display(Name = "SPSplitting_Display_OldPartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OldPartCode { get; set; }
        [Display(Name = "SPSplitting_Display_OldPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OldPartName { get; set; }

        public string NewPartId { get; set; }
        [Display(Name = "SPSplitting_Display_NewPartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string NewPartCode { get; set; }
        [Display(Name = "SPSplitting_Display_NewPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string NewPartName { get; set; }

        [Display(Name = "SPSplitting_Display_CounterNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CounterNo { get; set; }
        [Display(Name = "SPSplitting_Display_Qty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Quantity { get; set; }
        [Display(Name = "SPSplitting_Display_Usable", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Usable { get; set; }

        [Display(Name = "SPSplitting_Display_User", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateUser { get; set; }
        public string CreateDate { get; set; }

        [Display(Name = "SPSplitting_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SparePart_Display_SplitPartUsable", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool WarrantyUsable { get; set; }
        [Display(Name = "BreakdownDefinition_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyUsableChange { get; set; }

        public SparePartSplittingListModel()
        { 
        }
        public SparePartSplittingListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            
        }
    }
}
