using FluentValidation;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalAddPdiPackageModelValidator))]
    public class ProposalAddPdiPackageModel : ModelBase
    {
        [Display(Name = "ProposalCard_Display_PdiCheckNote", ResourceType = typeof(MessageResource))]
        public string PdiCheckNote { get; set; }
        [Display(Name = "ProposalCard_Display_TransmissionSerialNo", ResourceType = typeof(MessageResource))]
        public string TransmissionSerialNo { get; set; }
        [Display(Name = "ProposalCard_Display_DifferencialSerialNo", ResourceType = typeof(MessageResource))]
        public string DifferencialSerialNo { get; set; }
        [Display(Name = "ProposalCard_Pdi_ApproverNote", ResourceType = typeof(MessageResource))]
        public string ApprovalNote { get; set; }
        public long ProposalId { get; set; }
    }

    public class ProposalAddPdiPackageModelValidator : AbstractValidator<ProposalAddPdiPackageModel>
    {
        public ProposalAddPdiPackageModelValidator()
        {
            RuleFor(c => c.DifferencialSerialNo)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DifferencialSerialNo)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 50);
            RuleFor(c => c.TransmissionSerialNo)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TransmissionSerialNo)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 50);
            RuleFor(c => c.PdiCheckNote)
                .Length(0, 1000)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 1000);
        }
    }
}
