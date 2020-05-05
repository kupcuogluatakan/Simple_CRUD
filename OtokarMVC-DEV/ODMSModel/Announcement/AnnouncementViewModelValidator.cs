using System;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Announcement
{
    public class AnnouncementViewModelValidator : AbstractValidator<AnnouncementViewModel>
    {
        public AnnouncementViewModelValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Title)
                .Length(0, 200)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Body).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StartDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EndDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsUrgent).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.StartDate)
                .Must((m, o) => m.EndDate > o)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                MessageResource.Announcement_Display_StartDate, MessageResource.Announcement_Display_EndDate));

            RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithLocalizedMessage(() =>  MessageResource.Validation_GreaterThanToday);
            RuleFor(c => c.StartDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithLocalizedMessage(() => MessageResource.Validation_GreaterThanToday); 
        }
    }
}
