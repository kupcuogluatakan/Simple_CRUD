using System;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSModel.User
{
    public class UserIndexViewModelValidator : AbstractValidator<UserIndexViewModel>
    {
        public UserIndexViewModelValidator()
        {
            RuleFor(c => c.UserCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.UserCode)
                .Length(0, 10)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.LanguageCode).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.UserFirstName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.UserFirstName)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.UserMidName)
                 .Length(0, 30)
                 .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.UserLastName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.UserLastName)
                 .Length(0, 30)
                 .WithLocalizedMessage(() => MessageResource.Validation_Length);

            RuleFor(c => c.RoleTypeId).NotNull().When(c => UserManager.UserInfo.GetUserDealerId() != 0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SexId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MaritalStatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EmploymentDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.UnemploymentDate)
                .GreaterThanOrEqualTo(c => c.EmploymentDate)
                .When(c => c.IsActive == false && c.UnemploymentDate.GetValueOrDefault() != default(DateTime))
                .WithLocalizedMessage(() => MessageResource.Validation_UnemploymentDate);
            RuleFor(c => c.BirthDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BirthDate)
                .LessThan(DateTime.Now)
                .WithLocalizedMessage(() => MessageResource.Validation_BirthDate);
            RuleFor(c => c.EMail).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EMail)
                .EmailAddress()
                .WithLocalizedMessage(() => MessageResource.Validation_EMail_Format);
            RuleFor(c => c.EMail)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Address)
                .Length(0, 500)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Extension).Matches(@"^[-,0-9]+$").WithLocalizedMessage(() => MessageResource.Validation_Number);
            RuleFor(c => c.Extension)
                .Length(0, 10)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.EmploymentDate)
                .GreaterThanOrEqualTo(c => c.BirthDate.GetValue<DateTime>().AddDays(1))
                .WithLocalizedMessage(() => MessageResource.Validation_UserEmploymentDateGreaterThanBirthDay);

            RuleFor(c => c.Phone).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Address).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.PhotoDocId).NotEmpty().When(c => c.PhotoDocId == 0).WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.TCIdentityNo).NotEmpty().When(c => string.IsNullOrWhiteSpace(c.PassportNo)).WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.TCIdentityNo).InclusiveBetween(10000000000, 99999999999).WithLocalizedMessage(() => MessageResource.ValidationCode_Length);
            RuleFor(c => c.PassportNo).NotEmpty().When(c => !c.TCIdentityNo.HasValue || c.TCIdentityNo <= 0).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
