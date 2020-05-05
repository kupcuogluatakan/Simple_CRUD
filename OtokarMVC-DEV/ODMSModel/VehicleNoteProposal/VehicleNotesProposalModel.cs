using System;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.VehicleNoteProposal
{
    [Validator(typeof(VehicleNotesProposalModelValidator))]
    public class VehicleNotesProposalModel : ModelBase
    {


        //VehicleNoteID
        public int VehicleNotesId { get; set; }
        //VehicleID
        public int VehicleId { get; set; }
        //DealerID
        public int? DealerId { get; set; }
        //DealerName
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleNote_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        //Note
        [Display(Name = "VehicleNote_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }
        //Approve Date
        public DateTime? ApproveDate { get; set; }
        //Approve User
        public int? ApproveUser { get; set; }
        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private class VehicleNotesProposalModelValidator
        {
        }
    }

    
}
