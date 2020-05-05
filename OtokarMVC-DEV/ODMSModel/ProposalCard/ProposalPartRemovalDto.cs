using FluentValidation;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalPartRemovalDtoValidator))]
    public class ProposalPartRemovalDto
    {
        public long ProposalDetailId { get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPart", ResourceType = typeof(MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPart", ResourceType = typeof(MessageResource))]
        public long? DismentledPartId { get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPartSerialNo", ResourceType = typeof(MessageResource))]
        public string DismentledPartSerialNo { get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPart", ResourceType = typeof(MessageResource))]
        public string DismentledPartName { get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPart", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPartSerialNo", ResourceType = typeof(MessageResource))]
        public string PartSerialNo { get; set; }

        public string PartCode { get; set; }
    }

    public class ProposalPartRemovalDtoValidator : AbstractValidator<ProposalPartRemovalDto>
    {
        public ProposalPartRemovalDtoValidator()
        {
            RuleFor(c => c.DismentledPartSerialNo)
                .Length(0, 15)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 15);
            RuleFor(c => c.DismentledPartSerialNo)
             .Matches(@"^[a-zA-Z0-9\s,]*$")
             .WithLocalizedMessage(() => MessageResource.Validation_AlphaNumericOnly);
            RuleFor(c => c.PartSerialNo)
                .Length(0, 15)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 15);
            RuleFor(c => c.PartSerialNo)
           .Matches(@"^[a-zA-Z0-9\s,]*$")
           .WithLocalizedMessage(() => MessageResource.Validation_AlphaNumericOnly);
        }
    }
}
