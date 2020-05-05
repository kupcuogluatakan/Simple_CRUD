using System;
using ODMSModel.Validation;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.VehicleNoteProposalApprove
{

    [Validator(typeof(VehicleNoteProposalApproveModelValidator))]
    public class VehicleNoteProposalApproveModel : ModelBase
    {
        public VehicleNoteProposalApproveModel()
        {
        }


        public int VehicleNotesId { get; set; }


        public int VehicleId { get; set; }

        public int? DealerId { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinId { get; set; }

        [Display(Name = "VehicleNote_Display_KM", ResourceType = typeof(MessageResource))]
        public decimal VehicleKm { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleNote_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "VehicleNote_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }

        public DateTime? ApproveDate { get; set; }

        [Display(Name = "VehicleNote_Display_Date", ResourceType = typeof(MessageResource))]
        public DateTime? CreateDate { get; set; }

        public int? ApproveUser { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString
        {
            set { }
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }
        [Display(Name = "VehicleNote_Display_Date", ResourceType = typeof(MessageResource))]
        public string CreateDateString { get { return CreateDate == null ? string.Empty : CreateDate.Value.ToString("dd/MM/yyyy"); } }

        private class VehicleNoteProposalApproveModelValidator
        {
        }
    }
}






    

