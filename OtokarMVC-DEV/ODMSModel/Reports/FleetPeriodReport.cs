using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class FleetPeriodReport
    {

        [Display(Name = "FleetPeriodReport_Display_Period", ResourceType = typeof(MessageResource))]
        public int? Period { get; set; }

        [Display(Name = "FleetPeriodReport_Display_FleetCode", ResourceType = typeof(MessageResource))]
        public string FleetCode { get; set; }

        [Display(Name = "FleetPeriodReport_Display_FleetName", ResourceType = typeof(MessageResource))]
        public string FleetName { get; set; }

        [Display(Name = "FleetPeriodReport_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime StartDate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_FinishDate", ResourceType = typeof(MessageResource))]
        public DateTime FinishDate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderOpenDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderOpenDate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderVehicleLeaveDate", ResourceType = typeof(MessageResource))]
        public DateTime? WorkOrderVehicleLeaveDate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderCloseDate", ResourceType = typeof(MessageResource))]
        public DateTime? WorkOrderCloseDate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderNo", ResourceType = typeof(MessageResource))]
        public int? WorkOrderNo { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderStatus", ResourceType = typeof(MessageResource))]
        public string WorkOrderStatus { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderDealerRegion", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealerRegion { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderDealerCode", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealerCode { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderDealer", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealer { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerType", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerType { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerId", ResourceType = typeof(MessageResource))]
        public int WorkOrderCustomerId { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerName { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerIdentity", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerIdentity { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerTaxNo", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerTaxNo { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerAddress", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerAddress { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerMobile", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerMobile { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerTelephone", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerTelephone { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerWithHolding", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerWithHolding { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerVatExclude", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVatExclude { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerVehiclePlate", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehiclePlate { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerVehicleModel", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleModel { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerVehicleVinNo", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleVinNo { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerVehicleKm", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleKm { get; set; }

        [Display(Name = "FleetPeriodReport_Display_CustomerRequest", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerRequest { get; set; }

        [Display(Name = "FleetPeriodReport_Display_WorkOrderDetailId", ResourceType = typeof(MessageResource))]
        public int WorkOrderDetailId { get; set; }

        [Display(Name = "FleetPeriodReport_Display_IndicatorType", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorType { get; set; }

        [Display(Name = "FleetPeriodReport_Display_IndicatorCode", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorCode { get; set; }

        [Display(Name = "FleetPeriodReport_Display_IndicatorDescription", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorDescription { get; set; }


        [Display(Name = "FleetPeriodReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PartQuantity { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal? PartUnitPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_OtokarPartDiscountRate", ResourceType = typeof(MessageResource))]
        public decimal? OtokarPartDiscountRate { get; set; }
        [Display(Name = "FleetPeriodReport_Display_OtokarPartDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? OtokarPartDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_DealerPartDiscountRate", ResourceType = typeof(MessageResource))]
        public decimal? DealerPartDiscountRate { get; set; }
        [Display(Name = "FleetPeriodReport_Display_DealerPartDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? DealerPartDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartDiscountedPrice", ResourceType = typeof(MessageResource))]
        public decimal? PartDiscountedPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? PartDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_PartPrice", ResourceType = typeof(MessageResource))]
        public decimal? PartPrice { get; set; }


        [Display(Name = "FleetPeriodReport_Display_LabourCode", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourName", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourQuantity", ResourceType = typeof(MessageResource))]
        public decimal? LabourQuantity { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourDuration", ResourceType = typeof(MessageResource))]
        public decimal? LabourDuration { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourUnitPrice", ResourceType = typeof(MessageResource))]
        public decimal? LabourUnitPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_OtokarLabourDiscountRate", ResourceType = typeof(MessageResource))]
        public decimal? OtokarLabourDiscountRate { get; set; }
        [Display(Name = "FleetPeriodReport_Display_OtokarLabourDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? OtokarLabourDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_DealerLabourDiscountRate", ResourceType = typeof(MessageResource))]
        public decimal? DealerLabourDiscountRate { get; set; }
        [Display(Name = "FleetPeriodReport_Display_DealerLabourDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? DealerLabourDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourDiscountedPrice", ResourceType = typeof(MessageResource))]
        public decimal? LabourDiscountedPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? LabourDiscountPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_LabourPrice", ResourceType = typeof(MessageResource))]
        public decimal? LabourPrice { get; set; }
        [Display(Name = "FleetPeriodReport_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
    }
}
