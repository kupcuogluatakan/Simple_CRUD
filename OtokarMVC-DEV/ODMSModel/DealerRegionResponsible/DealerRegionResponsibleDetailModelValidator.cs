using FluentValidation;

namespace ODMSModel.DealerRegionResponsible
{
    public class DealerRegionResponsibleDetailModelValidator : AbstractValidator<DealerRegionResponsibleDetailModel>
    {
        public DealerRegionResponsibleDetailModelValidator()
        {
            RuleFor(x => x.DealerRegionIdString)
               .NotEmpty()
               .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(x => x.UserIdString)
               .NotEmpty()
               .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
