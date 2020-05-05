using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.ProposalCard
{
    public class ProposalCardModel : ModelBase
    {
        public long ProposalId { get; set; }

        public int ProposalStatId { get; set; }
        public string ProposalStat { get; set; }
        public bool ProposalStatManualChangeAllow { get; set; }
        public DateTime ProposalDate { get; set; }
        public int VehicleNoteCount { get; set; }
        public bool HasCampaign { get; set; }
        public int DocumentCount { get; set; }
        public string ContactInfo { get; set; }
        public bool IsPdiOwner { get; set; }
        public bool IsPrint { get; set; }
        public ProposalCustomerModel Customer { get; set; }
        public ProposalVehicleModel Vehicle { get; set; }
        public List<ProposalDetailModel> Details { get; set; }
        public List<ProposalDetailList> DetailList { get; set; }
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
        public bool IsConvert { get; set; }
        public string CancelReason { get; set; }
        public ProposalCardModel()
        {
            Details = new List<ProposalDetailModel>();
            DetailList = new List<ProposalDetailList>();
            Matter1 = "7";
        }
        public string CustomerNote { get; set; }

        public int DealerId { get; set; }

        public long AppointmentId { get; set; }
        public string DealerName { get; set; }
        public int ProposalSeq { get; set; }


        public string Matter1 { get; set; }
        public string Matter2 { get; set; }
        public string Matter3 { get; set; }
        public string Matter4 { get; set; }
        public IEnumerable<SelectListItem> TypeList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "müşteri", Value = "müşteri tarafından"},
                    new SelectListItem { Text = "servis", Value = "servis tarafından"},
                    new SelectListItem { Text = "yarı yarıya", Value = "yarı yarıya taraflarca"}
                };
            }
        }
        public string TechnicalDesc { get; set; }
        public string[] TechnicialDescList { get; set; }
        public int ApprovedCount { get; set; }


        //WitholdingStatus
        public int? WitholdingStatus { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(MessageResource))]
        public string WitholdingStatusName { get; set; }

        //WitholdingId
        [Display(Name = "Customer_Display_WitholdingRate", ResourceType = typeof(MessageResource))]
        public string WitholdingId { get; set; }
        public string WitholdingName { get; set; }

    }
}
