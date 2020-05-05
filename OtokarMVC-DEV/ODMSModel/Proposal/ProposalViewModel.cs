using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Attributes;
using ODMSModel.WorkOrder;

namespace ODMSModel.Proposal
{
    [Validator(typeof(ProposalViewModelValidator))]
    public  class ProposalViewModel:ModelBase
    {
        public int ProposalId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "Customer_Display_LastName", ResourceType = typeof(MessageResource))]
        public string CustomerSurName { get; set; }
        [Display(Name = "User_Display_Phone", ResourceType = typeof(MessageResource))]
        public string CustomerPhone { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }
        public int VehicleTypeId { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }
        [Display(Name = "Appointment_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public int VehicleId { get; set; }
        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(MessageResource))]
        public string AppointmentTypeId { get; set; }
        public int DealerId { get; set; }
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "Proposal_Display_ResponsibleConsultant", ResourceType = typeof(MessageResource))]
        public string Stuff { get; set; }
        [Display(Name = "Appointment_Display_ComplaintDescription", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(MessageResource))]
        public int? AppointmentId { get; set; }
        [Display(Name = "Proposal_Display_ProposalDate", ResourceType = typeof(MessageResource))]
        public DateTime ProposalDate { get; set; }
        [Display(Name = "Proposal_Display_ProposalStatus", ResourceType = typeof(MessageResource))]
        public int ProposalStatusId { get; set; }
        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(MessageResource))]
        public int VehicleKM { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "WorkOrder_Display_DeliveryTime", ResourceType = typeof(MessageResource))]
        public DateTime DeliveryTime { get; set; }
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }

        public string CustomerFullName
        {
            get
            {
                return string.Format("{0} {1}", CustomerName, CustomerSurName);
            }
        }

        public string AppointmentType { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public int FleetId { get; set; }

        public long WorkOrderId { get; set; }
        public int ProposalSeq { get; set; }
    }
}
