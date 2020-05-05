using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SaleReportFilterRequest : ReportListModelBase
    {
        public SaleReportFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public SaleReportFilterRequest()
        {
            SaleType = "1";
            ReportType = 1;
        }
        /// <summary>
        /// /WO || SP
        /// </summary>
        /// 
        [Display(Name = "SaleReport_Display_SaleType", ResourceType = typeof(MessageResource))]
        public string SaleType { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceBeginDate { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceEndDate { get; set; }
        [Display(Name = "SaleReport_Display_RecordDate", ResourceType = typeof(MessageResource))]
        public DateTime? CreateBeginDate { get; set; }
        [Display(Name = "SaleReport_Display_RecordDate", ResourceType = typeof(MessageResource))]
        public DateTime? CreateEndDate { get; set; }
        [Display(Name = "SaleReport_Display_CustomerId", ResourceType = typeof(MessageResource))]
        public string CustomerId { get; set; }
        [Display(Name = "SaleReport_Display_DealerId", ResourceType = typeof(MessageResource))]
        public string DealerId { get; set; }
        [Display(Name = "SaleReport_Display_CutomerName", ResourceType = typeof(MessageResource))]
        public string CutomerName { get; set; }
        public int? PurchaseType { get; set; }
        [Display(Name = "SaleReport_Display_PurchaseStatus", ResourceType = typeof(MessageResource))]
        public int? PurchaseStatus { get; set; }
        public int? InvoiceType { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 0=Parça Bazlı || 1=Fatura bazında
        /// </summary>
        public int ReportType { get; set; }
        [Display(Name = "SaleReport_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SaleReport_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "SaleReport_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "SaleReport_Display_SaleTypeLookVal", ResourceType = typeof(MessageResource))]
        public string SaleTypeLookVal { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PartName",
            ResourceType = typeof(MessageResource))]
        public long? PartId { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCodeList { get; set; }
    }
}
