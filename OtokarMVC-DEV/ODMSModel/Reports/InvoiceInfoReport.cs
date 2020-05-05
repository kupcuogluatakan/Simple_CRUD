using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class InvoiceInfoReport
    {

        public long WorkOrderInvoiceId { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerSAP", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SAPCode { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CompanyType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CompanyType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_GovermentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GovermentType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TCIdentityNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_PassportNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PassportNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_TaxOffice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxOffice { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_TaxNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CustomerSurname", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerSurname { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_CustomerType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_HasWithold", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string HasWithold { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_AddressType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AddressType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_City", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string City { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Town", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Town { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Country { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_ZipCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ZipCode { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VehicleGroup", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleGroup { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VehicleCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_EngineType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EngineType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_EngineNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EngineNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_ModelYear", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelYear { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VehicleCustomer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleCustomer { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? WorkOrderId { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_WorkOrderDetailId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? WorkOrderDetailId { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceSerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceType { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_DiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiscountRatio { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_UnitPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal UnitPrice { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_TotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceLabel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceLabel { get; set; }


        [Display(Name = "InvoiceInfoReport_Display_LabourName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourCode { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_LabourPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal LabourPrice { get; set; }
        public int Type { get; set; }
        public int DealerId { get; set; }


    }
}
