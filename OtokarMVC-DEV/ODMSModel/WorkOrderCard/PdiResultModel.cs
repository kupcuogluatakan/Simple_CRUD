using FluentValidation;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(PdiResultModelValidator))]
    public class PdiResultModel
    {
        public long WorkOrderId { get; set; }
        public string ControlCode { get; set; }
        public string PartCode { get; set; }
        public string BreakDownCode { get; set; }
        public string ResultCode { get; set; }
    }

    public class PdiResultModelValidator : AbstractValidator<PdiResultModel>
    {
        public PdiResultModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ControlCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BreakDownCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ResultCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }

}
