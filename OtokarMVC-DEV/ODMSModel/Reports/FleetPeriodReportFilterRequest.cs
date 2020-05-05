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
    public class FleetPeriodReportFilterRequest : ReportListModelBase
    {

        public FleetPeriodReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }

        public FleetPeriodReportFilterRequest()
        {

        }

        [Display(Name = "FiloPeriodReport_Display_Period", ResourceType = typeof(MessageResource))]
        public int Period { get; set; }

        [Display(Name = "FiloPeriodReport_Display_FleetCode", ResourceType = typeof(MessageResource))]
        public string FleetCode { get; set; }

        [Display(Name = "FiloPeriodReport_Display_FleetName", ResourceType = typeof(MessageResource))]
        public string FleetName { get; set; }

        [Display(Name = "FiloPeriodReport_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime StartDate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_FinishDate", ResourceType = typeof(MessageResource))]
        public DateTime FinishDate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderOpenDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderOpenDate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderVehicleLeaveDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderVehicleLeaveDate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderCloseDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderCloseDate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderNo", ResourceType = typeof(MessageResource))]
        public int WorkOrderNo { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderStatus", ResourceType = typeof(MessageResource))]
        public string WorkOrderStatus { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderDealerRegion", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealerRegion { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderDealerCode", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealerCode { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderDealer", ResourceType = typeof(MessageResource))]
        public string WorkOrderDealer { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerType", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerType { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerId", ResourceType = typeof(MessageResource))]
        public int WorkOrderCustomerId { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerName { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerIdentity", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerIdentity { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerTaxNo", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerTaxNo { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerAddress", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerAddress { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerMobile", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerMobile { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerTelephone", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerTelephone { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerWithHolding", ResourceType = typeof(MessageResource))]
        public bool WorkOrderCustomerWithHolding { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerVatExclude", ResourceType = typeof(MessageResource))]
        public bool WorkOrderCustomerVatExclude { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerVehiclePlate", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehiclePlate { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerVehicleModel", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleModel { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerVehicleVinNo", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleVinNo { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerVehicleKm", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerVehicleKm { get; set; }

        [Display(Name = "FiloPeriodReport_Display_CustomerRequest", ResourceType = typeof(MessageResource))]
        public string WorkOrderCustomerRequest { get; set; }

        [Display(Name = "FiloPeriodReport_Display_WorkOrderDetailId", ResourceType = typeof(MessageResource))]
        public int WorkOrderDetailId { get; set; }

        [Display(Name = "FiloPeriodReport_Display_IndicatorType", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorType { get; set; }

        [Display(Name = "FiloPeriodReport_Display_IndicatorCode", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorCode { get; set; }

        [Display(Name = "FiloPeriodReport_Display_IndicatorDescription", ResourceType = typeof(MessageResource))]
        public string WorkOrderIndicatorDescription { get; set; }

    }
}
