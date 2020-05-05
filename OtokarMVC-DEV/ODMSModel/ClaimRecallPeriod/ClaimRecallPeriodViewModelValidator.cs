using System;
using System.Web.UI.WebControls;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimRecallPeriod
{
    public class ClaimRecallPeriodViewModelValidator : AbstractValidator<ClaimRecallPeriodViewModel>
    {
        public ClaimRecallPeriodViewModelValidator()
        {
            RuleFor(c => c.ValidLastDay).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShipFirstDay).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShipLastDay).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShipFirstDay).GreaterThanOrEqualTo(c => c.ValidLastDay).WithLocalizedMessage(() => MessageResource.ClaimRecallPeriod_Warning_ShipFirstDayGreaterThanValidLastDay);
            RuleFor(c => c.ShipLastDay).GreaterThanOrEqualTo(c => c.ShipFirstDay).WithLocalizedMessage(() => MessageResource.ClaimRecallPeriod_Warning_ShipLastDayGreaterThanShipFirstDay);
            RuleFor(x => x.ValidLastDay)
                .Must((m, o) => m.ValidLastDay >= DateTime.Now)
                .WithMessage(string.Format(
                    MessageResource.Validation_EndDateGreaterThanBeginDate, 
                    DateTime.Now,
                    MessageResource.ClaimRecallPeriod_Display_ValidLastDay));
            RuleFor(x => x.ShipFirstDay)
                .Must((m, o) => m.ShipFirstDay > m.ValidLastDay)
                .WithMessage(string.Format(
                    MessageResource.Validation_EndDateGreaterThanBeginDate,
                    MessageResource.ClaimRecallPeriod_Display_ValidLastDay,
                    MessageResource.ClaimRecallPeriod_Display_ShipFirstDay));
            RuleFor(x => x.ShipLastDay)
                .Must((m, o) => m.ShipLastDay >= m.ShipFirstDay)
                .WithMessage(string.Format(
                    MessageResource.Validation_EndDateGreaterThanBeginDate,
                    MessageResource.ClaimRecallPeriod_Display_ShipFirstDay,
                    MessageResource.ClaimRecallPeriod_Display_ShipLastDay));
        }
    }
}
