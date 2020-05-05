using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PurchaseOrder
{
    public class PurchaseOrderSearchListModel : BaseListWithPagingModel
    {
        public PurchaseOrderSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
        }

        public PurchaseOrderSearchListModel()
        {
        }

        [Display(Name = "PurchaseOrder_Display_StartDate", ResourceType = typeof (ODMSCommon.Resources.MessageResource))
        ]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PurchaseOrder_Display_EndDate", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PurchaseOrder_Display_PartId", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public int? PartId { get; set; }

        [Display(Name = "PurchaseOrder_Display_OtokarProposeNo",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string OtokarProposeNo { get; set; }

        [Display(Name = "PurchaseOrder_Display_DMSOrderNo", ResourceType = typeof (ODMSCommon.Resources.MessageResource)
            )]
        public string DMSOrderNo { get; set; }

        [Display(Name = "PurchaseOrder_Display_PurchaseType",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public int? PurchaseType { get; set; }

        [Display(Name = "PurchaseOrder_Display_IsDealerOrder",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public bool IsDealerOrder { get; set; }

        [Display(Name = "PurchaseOrder_Display_PartCodeList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeList { get; set; }

        [Display(Name = "PurchaseOrder_Display_IsOpenOrder",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsOpenOrder { get; set; }

        [Display(Name = "PurchaseOrderSearch_Display_DURUM",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StatusId { get; set; }

        [Display(Name = "PurchaseOrder_Display_StatusName",
    ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PoStatusId { get; set; }

        public string I_VKORG { get; set; }
        public string I_VTWEG { get; set; }
        public string I_SPART { get; set; }
        public string I_AUART { get; set; }
        public string I_LPRIO { get; set; }
        public string I_AUGRU { get; set; }
        public string I_SASBEG { get; set; }
        public string I_SASEND { get; set; }
        public string I_RFSTK { get; set; }
        public string I_KUNNR { get; set; }
        public string S_VBELN { get; set; }
        public string S_MATNR { get; set; }
        public string I_DURUM { get; set; }


        [Display(Name = "PurchaseOrderSearch_Display_VBELN",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VBELN { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_POSNR",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string POSNR { get; set; }
        public string ZZCHASSIS_NUM { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_ANGDT",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ANGDT { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_MATNR",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MATNR { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_BSTKD",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BSTKD { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_KWMENG",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal KWMENG { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_SASI_TEXT",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SASI_TEXT { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_DPARCA",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DPARCA { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_BAKIYE",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BAKIYE { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_TEYIT",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TEYIT { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_PAKETLEME",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PAKETLEME { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_MCIKISI",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal MCIKISI { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_IRSNO",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IRSNO { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_BLDAT",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BLDAT { get; set; }
        public string TAHTES { get; set; }
        public string PSTYV { get; set; }
        [Display(Name = "PurchaseOrderSearch_Display_DURUM",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DURUM { get; set; }
        [Display(Name = "PurchaseOrder_Display_StatusName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ParentStatus { get; set; }
    }
}
