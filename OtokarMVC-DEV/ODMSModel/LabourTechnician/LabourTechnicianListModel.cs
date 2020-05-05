using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.LabourTechnician
{
    public class LabourTechnicianListModel : BaseListWithPagingModel
    {
        public int LabourTechnicianId { get; set; }

        public int WorkOrderDetailId { get; set; }
        [Display(Name = "LabourTechnician_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public int? WorkOrderId { get; set; }

      
        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        public int? LabourId { get; set; }
        [Display(Name = "LabourTechnician_Display_LabourName", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }

        public int? UserID { get; set; }
        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        public string UserNameSurname { get; set; }

        public int? StatusId { get; set; }
        [Display(Name = "LabourTechnician_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "LabourTechnician_Display_WorkTimeEstimate", ResourceType = typeof(MessageResource))]
        public string WorkTimeEstimate { get; set; }

        [Display(Name = "LabourTechnician_Display_LabourTimeEstimate", ResourceType = typeof(MessageResource))]
        public string LabourTimeEstimate { get; set; }

        [Display(Name = "LabourTechnician_Display_WorkTimeReal", ResourceType = typeof(MessageResource))]
        public string WorkTimeReal { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "LabourTechnician_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }

        [Display(Name = "LabourTechnician_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime? CreateDate { get; set; }
        
        [Display(Name = "LabourTechnician_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "LabourTechnician_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        public bool IsSelected { get; set; }

        public int DealerId { get; set; }
        
        public LabourTechnicianListModel()
        {
        }

        public LabourTechnicianListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderId", "WOD.ID_WORK_ORDER"},
                     {"LabourName", "LABOUR_TYPE_DESC"},
                     {"LabourCode", "LABOUR_CODE"},
                     {"UserNameSurname", "(U.CUSTOMER_NAME +' '+ U.USER_LAST_NAME)"},
                     {"StatusName", "LABOUR_WORK_STATUS_LOOKVAL"},
                     {"WorkTimeEstimate", "LABOUR_WORK_TIME_ESTIMATE"},
                     {"WorkTimeReal", "LABOUR_WORK_TIME_REAL"},
                     {"WorkTimeEstimateFormatted", "LABOUR_WORK_TIME_ESTIMATE"},
                     {"WorkTimeRealFormatted", "LABOUR_WORK_TIME_REAL"},
                     {"Plate","PLATE"},
                     {"CreateDate","LT.CREATE_DATE"},
                     {"StartDate","START_DATE"},
                     {"EndDate","END_DATE"}
                 };
            SetMapper(dMapper);

        }
    }
}
