using FluentValidation;

namespace ODMSModel.Rack
{
    public class RackDetailModelValidator : AbstractValidator<RackDetailModel>
    {
        public RackDetailModelValidator()
        {
            RuleFor(f => f.WarehouseId)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(f => f.Code)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(f => f.Code)
                .Length(0,5)
                .WithMessage(string.Format(ODMSCommon.Resources.MessageResource.Validation_Length,5));

            //RuleFor(f => f.Name)
            //    .NotEmpty()
            //    .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
