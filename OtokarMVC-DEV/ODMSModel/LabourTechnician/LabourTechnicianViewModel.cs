using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System.Collections.Generic;

namespace ODMSModel.LabourTechnician
{
    [Validator(typeof(LabourTechnicianViewModelValidator))]
    public class LabourTechnicianViewModel : ModelBase
    {
        public int LabourTechnicianId { get; set; }

        public int WorkOrderDetailId { get; set; }

        [Display(Name = "LabourTechnician_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public int? WorkOrderId { get; set; }

        public int? LabourId { get; set; }

        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "LabourTechnician_Display_LabourName", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }

        public int? UserID { get; set; }
        public string _UserID { get; set; }

        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        public string UserNameSurname { get; set; }

        public new int? StatusId { get; set; }

        [Display(Name = "LabourTechnician_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "LabourTechnician_Display_WorkTimeEstimate", ResourceType = typeof(MessageResource))]
        public decimal? WorkTimeEstimate { get; set; }

        [Display(Name = "LabourTechnician_Display_LabourTimeEstimate", ResourceType = typeof(MessageResource))]
        public decimal? LabourTimeEstimate { get; set; }

        public string TotalLabourTimeEstimate {
            get
            {
                decimal totalWorkTime = 0;
                if (TecnicianUsers != null)
                {
                    foreach (var tUser in TecnicianUsers)
                    {
                        totalWorkTime += tUser.WorkTime;
                    }
                    return totalWorkTime.ToString();
                }
                else
                {
                    return 0.ToString();
                }
            }
        }

        //public string LabourTimeEstimateTotalDisplay {
        //    set {
        //        LabourTimeEstimate.ToString()
        //    }
        //}

        public string WorkTimeEstimateDisplay { get { return WorkTimeEstimate.HasValue ? WorkTimeEstimate.Value.ToString() : ""; } }

        [Display(Name = "LabourTechnician_Display_WorkTimeReal", ResourceType = typeof(MessageResource))]
        public decimal? WorkTimeReal { get; set; }

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

        public int WorkshopPlanTypeId { get; set; }
        public bool IsDealerDuration { get; set; }

        public List<int> Users { get; set; }

        public List<LabourTecnicianUserModel> TecnicianUsers { get; set; }
        public List<NewLabourTecnicianUserModel> NewTecnicianUsers { get; set; }
    }
}
