using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Reports
{
    public class LaborCostPerVehicleReportFilterRequest: ReportListModelBase
    {
        public LaborCostPerVehicleReportFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public LaborCostPerVehicleReportFilterRequest() { }

        //[Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string DealerIdList { get; set; }

        //[Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string RegionIdList { get; set; }

        //[Display(Name = "WorkOrderDetailReport_Display_CustomerList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string CustomerIdList { get; set; }

        //[Display(Name = "WorkOrderDetailReport_Display_ModelKodList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string VehicleModelList { get; set; }

        //[Display(Name = "ChargePerCarReport_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string VehicleTypeIdList { get; set; }

        //[Display(Name = "ChargePerCarReport_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string ProcessTypeList { get; set; }

        //[Display(Name = "ChargePerCarReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string VinNo { get; set; }

        //[Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public DateTime? StartDate { get; set; }

        //[Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public DateTime? EndDate { get; set; }

        //[Display(Name = "PartStockReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string Currency { get; set; }

        //[Display(Name = "FleetRequestVehicle_Display_IsWarranty", ResourceType = typeof(MessageResource))]
        //public int IsWarranty { get; set; }

        [Display(Name = "ChargePerCarReport_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIdList { get; set; }
        [Display(Name = "ChargePerCarReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "ChargePerCarReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleTypeIdList { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelIdList { get; set; }
        [Display(Name = "ChargePerCarReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "ChargePerCarReport_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeIdList { get; set; }
        [Display(Name = "ChargePerCarReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "ChargePerCarReport_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "ChargePerCarReport_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "ChargePerCarReport_InGuarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? InGuarantee { get; set; }
        [Display(Name = "ChargePerCarReport_GuaranteeConfirmDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? GuaranteeConfirmDate { get; set; }
        [Display(Name = "GuaranteeConfirmDate_GuaranteeCategory", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeCategories { get; set; }

        [Display(Name = "GuaranteeConfirmDate_MaxKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public float? MaxKM { get; set; }
        [Display(Name = "GuaranteeConfirmDate_MinKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public float? MinKM { get; set; }


    }
}
