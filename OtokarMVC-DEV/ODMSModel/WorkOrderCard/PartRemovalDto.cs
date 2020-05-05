using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation;
using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(PartRemovalDtoValidator))]
    public class PartRemovalDto
    {
        public long WorkOrderDetailId{ get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPart", ResourceType = typeof(MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPart", ResourceType = typeof(MessageResource))]
        public long? DismentledPartId{ get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPartSerialNo", ResourceType = typeof(MessageResource))]
        public string DismentledPartSerialNo{ get; set; }
        [Display(Name = "WorkOrderCard_Display_DismentledPart", ResourceType = typeof(MessageResource))]
        public string DismentledPartName { get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPart", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "WorkOrderCard_Display_UsedPartSerialNo", ResourceType = typeof(MessageResource))]
        public string PartSerialNo { get; set; }

        public string PartCode { get; set; }
    }

    public class PartRemovalDtoValidator : AbstractValidator<PartRemovalDto>
    {
        public PartRemovalDtoValidator()
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
