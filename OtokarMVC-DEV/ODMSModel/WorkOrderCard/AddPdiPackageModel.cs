using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(AddPdiPackageModelValidator))]
    public class AddPdiPackageModel:ModelBase
    {
        [Display(Name = "WorkOrderCard_Display_PdiCheckNote", ResourceType = typeof(MessageResource))]
        public string PdiCheckNote{ get; set; }
        [Display(Name = "WorkOrderCard_Display_TransmissionSerialNo", ResourceType = typeof(MessageResource))]
        public string TransmissionSerialNo { get; set; }
        [Display(Name = "WorkOrderCard_Display_DifferencialSerialNo", ResourceType = typeof(MessageResource))]
        public string DifferencialSerialNo { get; set; }
        [Display(Name = "WorkOrderCard_Pdi_ApproverNote", ResourceType = typeof(MessageResource))]
        public string ApprovalNote { get; set; }
        public long WorkOrderId { get; set; }
    }

    public class AddPdiPackageModelValidator : AbstractValidator<AddPdiPackageModel>
    {
        public AddPdiPackageModelValidator()
        {
            RuleFor(c => c.DifferencialSerialNo)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DifferencialSerialNo)
                .Length(0,50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,50);
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
