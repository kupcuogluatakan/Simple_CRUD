using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.WorkHour
{
    public class WorkHourViewModelValidator:AbstractValidator<WorkHourViewModel>
    {
        public WorkHourViewModelValidator()
        {
            RuleFor(c => c.StartDate).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.EndDate).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(c => c.StartDate)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                    MessageResource.WorkHour_Display_StartDate, MessageResource.WorkHour_Display_EndDate));
            RuleFor(c => c.WorkStartHour).NotNull().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.WorkEndHour).NotNull().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.MeetingInterval).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Priority).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Priority).Must(c =>
            {
                if (c.HasValue)
                    return c.Value.ToString().Length < 3;
                return false;
            }).WithMessage(string.Format(MessageResource.Validation_IntegerLength,2));
            RuleFor(c => c.MeetingInterval).Must(c => c.ToString().Length < 3).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 2));
            RuleFor(c => c.WeekDays)
                .Must((m, p) => p.Exists(c => c == true))
                .WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Description)
                .Length(0, 200)
                .WithMessage(string.Format(MessageResource.Validation_Length, 200));
            RuleFor(c => c.MeetingInterval).ExclusiveBetween(1,100).WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.LaunchBreakEndHour)
                .Must(IsInWorkHours)
                .WithMessage(string.Format(MessageResource.Validation_MustBeWithinWorkHours,
                    MessageResource.WorkHour_Display_LauchBreakEndHour));
            RuleFor(c => c.LaunchBreakStartHour)
                .Must(IsInWorkHours)
                .WithMessage(string.Format(MessageResource.Validation_MustBeWithinWorkHours,
                    MessageResource.WorkHour_Display_LauchBreakStartHour));
            RuleFor(c => c.TeaBreaks)
                .Must(IsTeaBreaksInWorkHours)
                .WithMessage(string.Format(MessageResource.Validation_MustBeWithinWorkHours,
                    MessageResource.WorkHour_Display_TeaBreak));
        }
        private bool IsInWorkHours(WorkHourViewModel model, TimeSpan hour)
        {
            var cmp1 = TimeSpan.Compare(hour, model.WorkStartHour);
            var cmp2 = TimeSpan.Compare(hour, model.WorkEndHour);

            return (hour!=model.WorkStartHour && hour!=model.WorkEndHour) ? cmp1 + cmp2 <= 0 : true;
        }
        private bool IsTeaBreaksInWorkHours(WorkHourViewModel model, IEnumerable<TeaBreakModel> list)
        {
            bool result = !list.Any();
            foreach (var teaBreakModel in list)
            {
                if (teaBreakModel.StartTime != "undefined")
                {
                    result = IsInWorkHours(model, teaBreakModel.StartTime.GetValue<DateTime>().ToTimeSpan());
                    if (!result)
                        return false;
                }
            }
            return result;
        }
    }
}
