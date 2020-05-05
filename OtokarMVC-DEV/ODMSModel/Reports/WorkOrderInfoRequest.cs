﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class WorkOrderInfoRequest:ReportListModelBase
    {

        public WorkOrderInfoRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public WorkOrderInfoRequest() { }

        [Display(Name = "Sorgulama Kriteri")]
        public int GroupType { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustTypeList { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? CustomerId { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StatusIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusIdList { get; set; }

        public int group_type { get; set; }

        public int IsDetail { get; set; }

        public string GroupTypeVal { get; set; }

        public int VehicleId { get; set; }

        public string GroupName { get; set; }
        public string GroupCode { get; set; }



    }
}
