using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class WorkOrderMaintReportListModel : BaseListWithPagingModel
    {
        public WorkOrderMaintReportListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DealerRegionName","DEALER_REGION_NAME"},
                    {"ModelName","MODEL_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"MaintName", "MAINT_NAME"},
                    {"TotalInvoiceAmount", "TOTAL_INVOICE_AMOUNT"},
                    {"TotalDiscountPrice", "TOTAL_DISCOUNT_PRICE"},
                    {"TotalLabourPrice", "TOTAL_LABOUR_PRICE"},
                    {"TotalPartPrice", "TOTAL_PART_PRICE"},
                    {"InvoiceDate", "INVOICE_DATE"},
                    {"VinNo", "VIN_NO"},
                    {"IdWorkOrder", "ID_WORK_ORDER"},
                    {"Customer", "CUSTOMER"}


                };
            SetMapper(dMapper);
        }

        public WorkOrderMaintReportListModel()
        {
        }

        #region Search Parameters
        [Display(Name = "WorkOrderMaintReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceStartDate { get; set; }

        [Display(Name = "PurchaseOrderReport_Display_InvoiceEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceEndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKodList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_PeriodicMaint",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PeriodicMaint { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_InGuarantee",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int InGuarantee { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_InvoiceNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNo { get; set; }

        #endregion

        #region List Display Columns
        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKod",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_MaintName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaintName { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_TotalInvoiceAmount",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalInvoiceAmount { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_TotalPartPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalPartPrice { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_TotalLabourPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalLabourPrice { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_TotalDiscountPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalDiscountPrice { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_VinNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_Customer",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Customer { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_IdWorkOrder",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IdWorkOrder { get; set; }

        [Display(Name = "WorkOrderMaintReport_Display_InvoiceDate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime InvoiceDate { get; set; }

        public long WorkOrderInvoiceId { get; set; }
        public int VehicleId { get; set; }
        #endregion
    }
}
