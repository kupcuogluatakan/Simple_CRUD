using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GRADMstViewModel : ModelBase
    {
        public Int64 GuaranteeId { get; set; }

        public int GuaranteeSeq { get; set; }

        public Int64 WorkOrderId { get; set; }

        public int WarrantyStatus { get; set; }

        public bool IsEditable { get; set; }

        public int HasPdiVehicle { get; set; }

        public bool IsInvoiced { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_Desc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ConfirmDesc { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Category? CategoryId { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CategoryName { get; set; }

        [Display(Name = "WorkOrderCard_Display_FailureCodeAndDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FailureCodeAndDescription { get; set; }

        public bool IsShowCategory { get; set; }

        public string CurrencySymbol { get; set; }

        public string GuaranteeDealer { get; set; }
        public int DealerId { get; set; }
        public string GuaranteeCustomer { get; set; }
        [Display(Name = "FleetRequestApprove_Display_RequestDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TechnicalDesc { get; set; }
        [Display(Name = "WorkOrderCard_Display_TechnicalDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerDesc { get; set; }
        public string VehicleId { get; set; }
        public int VehicleNoteCount { get; set; }
        public enum Category
        {
            [StringValue("GuaranteeRequestApprove_Category_Other")]
            Other,
            [StringValue("GuaranteeRequestApprove_Category_Campaign")]
            Campaign,
            [StringValue("GuaranteeRequestApprove_Category_Corrosion")]
            Corrosion,
            [StringValue("GuaranteeRequestApprove_Category_Service")]
            Service,
            [StringValue("GuaranteeRequestApprove_Category_Contract")]
            Contract,
            [StringValue("GuaranteeRequestApprove_Category_Design")]
            Design,
            [StringValue("GuaranteeRequestApprove_Category_Supplier")]
            Supplier,
            [StringValue("GuaranteeRequestApprove_Category_Commercial")]
            Commercial,
            [StringValue("GuaranteeRequestApprove_Category_Production")]
            Production,
            [StringValue("GuaranteeRequestApprove_Category_PDI")]
            PDI,
            [StringValue("GuaranteeRequestApprove_Category_SPGuarantee")]
            SPGuarantee,
            [StringValue("GuaranteeRequestApprove_Category_Coupon")]
            Coupon,
            [StringValue("GuaranteeRequestApprove_Category_Water")]
            Water
        }

        public enum WarrantyStatusType
        {
            NewRecord = 0,
            WaitingAuthorityApproval = 1,
            AuthorityApproved = 2,
            AuthorityRejected = 3,
            WaitingApproval = 4,
            Approved = 5,
            Rejected = 6
        }

        public const string GIFCategory_Other = "diger";
        public const string GIFCategory_Campaign = "kampanya";
        public const string GIFCategory_Corrosion = "korozyon";
        public const string GIFCategory_Service = "servis";
        public const string GIFCategory_Contract = "sozlesme";
        public const string GIFCategory_Design = "tasarim";
        public const string GIFCategory_Supplier = "tedarikci";
        public const string GIFCategory_Commercial = "ticari";
        public const string GIFCategory_Production = "uretim";
        public const string GIFCategory_PDI = "tob";
        public const string GIFCategory_SPGuarantee = "YP_G";
        public const string GIFCategory_Coupon = "kupon";
        public const string GIFCategory_Water = "sualma";

        public VehicleInfo VehicleInfo { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public FailureInfo FailureInfo { get; set; }
        public GifModel GifInfo { get; set; }
        public FailureDescInfo FailureDescInfo { get; set; }
        public PeriodicMaintInfo PeriodicMaintInfo { get; set; }
    }
}
