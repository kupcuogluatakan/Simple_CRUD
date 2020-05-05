using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class LaborCostPerVehicleReport
    {
        //[Display(Name = "WorkOrderDetailReport_Display_DealerIdList",ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string DealerIdList { get; set; }
        //[Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string RegionIdList { get; set; }

        //public string CustomerIdList { get; set; }
        //public string VehicleTypeList { get; set; }
        //public string VehicleModelList { get; set; }
        //public string ProcessTypeCodeList { get; set; }
        //public string VinNo { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public int GuaranteeStat { get; set; }
        //public string Servis { get; set; }
        //public string Bolge { get; set; }
        //public string Musteri { get; set; }
        //public string AracTipi { get; set; }
        //public string AracModeli { get; set; }
        //public string ParaBirimi { get; set; }
        //public int ToplamIsEmriAdedi { get; set; }
        //public int ToplamBelirtiAdedi { get; set; }
        //public int ToplamAracAdedi { get; set; }
        public decimal PT_A_ToplamIscilikSuresi { get; set; }
        public decimal PT_A_ToplamIscilikTutari { get; set; }
        public decimal PT_C_ToplamIscilikSuresi { get; set; }
        public decimal PT_C_ToplamIscilikTutari { get; set; }
        public decimal PT_M_ToplamIscilikSuresi { get; set; }
        public decimal PT_M_ToplamIscilikTutari { get; set; }
        public decimal PT_P_ToplamIscilikSuresi { get; set; }
        public decimal PT_P_ToplamIscilikTutari { get; set; }
        public decimal PT_S_ToplamIscilikSuresi { get; set; }
        public decimal PT_S_ToplamIscilikTutari { get; set; }
        public decimal PT_T_ToplamIscilikSuresi { get; set; }
        public decimal PT_T_ToplamIscilikTutari { get; set; }
        public decimal PT_Y_ToplamIscilikSuresi { get; set; }
        public decimal PT_Y_ToplamIscilikTutari { get; set; }
        //public int X { get; set; }
        //public int DealerId { get; set; }
        //public int DealerRegionId { get; set; }
        //public int CustomerId { get; set; }
        //public int VehicleTypeId { get; set; }
        //public string VehicleModelCode { get; set; }

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






        public int DealerId { get; set; }
        public int DealerRegionId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleModelCode { get; set; }
        public int IdVehicle { get; set; }
        public List<ChargePerCarProcessType> ProcessTypes { get; set; }

    }
}
