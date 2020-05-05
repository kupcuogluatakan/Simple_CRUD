using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class ChargePerCarReport
    {
        [Display(Name = "ChargePerCarReport_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string NameSurname { get; set; }
        [Display(Name = "ChargePerCarReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "ChargePerCarReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TypeName { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelName { get; set; }
        [Display(Name = "ChargePerCarReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "ChargePerCarReport_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeName { get; set; }
        public string ProcessTypeCode { get; set; }
        [Display(Name = "ChargePerCarReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "ChargePerCarReport_InGuarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InGuarantee { get; set; }
        [Display(Name = "ChargePerCarReport_WorkOrderCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderCount { get; set; }
        [Display(Name = "ChargePerCarReport_WorkOrderDetailCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderDetailCount { get; set; }
        [Display(Name = "ChargePerCarReport_CarCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CarCount { get; set; }
        public decimal PartPrice { get; set; }
        public decimal LabourPrice { get; set; }

        [Display(Name = "ChargePerCarReport_CategoryLookval", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CategoryLookval { get; set; }
        [Display(Name = "ChargePerCarReport_ApproveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "ChargePerCarReport_TotalAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalAmount { get; set; }


        public int WorkOrderId { get; set; }
        public int WorkOrderDetId { get; set; }



        public int DealerId { get; set; }
        public int DealerRegionId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleModelCode { get; set; }
        public int IdVehicle { get; set; }
        public List<ChargePerCarProcessType> ProcessTypes { get; set; }
    }

    public class ChargePerCarProcessType
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal PartPrice { get; set; }
        public decimal LabourPrice { get; set; }
    }
}
