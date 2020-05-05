using System;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.EducationDates
{
    public class EducationDatesViewModelValidator : AbstractValidator<EducationDatesViewModel>
    {
        public EducationDatesViewModelValidator()
        {

            //EducationPlace
            RuleFor(q => q.EducationPlace)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.EducationPlace)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            //Instructor
            RuleFor(q => q.Instructor)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.Instructor)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            //Notes
            RuleFor(q => q.Notes)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            //Max&Min
            RuleFor(x => x.MinimumAtt)
                .Must((m, o) => m.MaximumAtt >= o)
                .WithMessage(string.Format(ODMSCommon.Resources.MessageResource.Validation_XGreaterThanY, MessageResource.EducationDates_Display_Max, MessageResource.EducationDates_Display_Min));
            //EducationTimeDT
            RuleFor(x => x.EducationTimeDT)
                .Must((m, o) => m.EducationTimeDT >= DateTime.Now)
                .WithMessage(string.Format(ODMSCommon.Resources.MessageResource.Validation_EndDateGreaterThanBeginDate, MessageResource.WorkHour_Display_StartDate, DateTime.Now));

        }
    }
}
