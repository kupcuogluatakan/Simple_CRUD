using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class InvoiceInfoFilterRequest: ReportListModelBase
    {
        public InvoiceInfoFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }
        public InvoiceInfoFilterRequest() { }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_LabourIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourIdList { get; set; }


        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceCreateBeginDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceCreateBeginDate { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceCreateEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceCreateEndDate { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_InvoiceType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceType { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_InvoiceDocumentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceDocumentType { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_CustomerType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerType { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_GovermentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GovermentType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CompanyType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CompanyType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CustomerNameList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerNameList { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CustomerSurNameList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerSurNameList { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_HasWithold", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool HasWithold { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_VatType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VatType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_ShowInvoiceDetails", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool ShowInvoiceDetails { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_InvoiceNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNo { get; set; }

    }
}
