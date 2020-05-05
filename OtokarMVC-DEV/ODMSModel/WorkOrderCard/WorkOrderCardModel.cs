using System;
using System.Collections.Generic;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCardModel : ModelBase
    {
        public long WorkOderId { get; set; }

        public int WorkOrderStatId { get; set; }
        public string WorkOrderStat { get; set; }
        public bool WorkOrderStatManualChangeAllow { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public int VehicleNoteCount { get; set; }
        public bool HasCampaign { get; set; }
        public int DocumentCount { get; set; }
        public string ContactInfo { get; set; }
        public bool IsPdiOwner { get; set; }
        public bool IsFromProposal { get; set; }
        public WorkOrderCustomerModel Customer { get; set; }
        public WorkOrderVehicleModel Vehicle { get; set; }
        public List<WorkOrderDetailModel> Details { get; set; }
        public List<WorkOrderDetailList> DetailList { get; set; }
        public int? FleetId { get; set; }
        public string FleetName { get; set; }
        public bool? IsPartConstricted { get; set; }
        public decimal OtokarPartDiscountRate { get; set; }
        public decimal OtokarLabourDiscountRate { get; set; }
        public decimal DealerPartDiscountRate { get; set; }
        public decimal DealerLabourDiscountRate { get; set; }
        public DateTime? VehicleLeaveDate { get; set; }
        public int? ApplicableFleetId { get; set; }
        public string Staff { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public bool IsCentralDealer { get; set; }
        public string CancelReason { get; set; }
        public WorkOrderCardModel()
        {
            Details = new List<WorkOrderDetailModel>();
            DetailList = new List<WorkOrderDetailList>();
        }
        public string CustomerNote { get; set; }

        public int DealerId { get; set; }

        public long AppointmentId { get; set; }
        public string DealerName { get; set; }
        public long? ProposalId { get; set; }
        public int ProposalSeq { get; set; }

        public string DeniedCampaignCodes { get; set; }
        public string DeniedCampaignServiceCodes { get; set; }
    }
}
