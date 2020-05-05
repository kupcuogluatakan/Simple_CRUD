using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Defect
{
    public class DefectViewModelValidator : AbstractValidator<DefectViewModel>
    {
        public DefectViewModelValidator()
        {
            RuleFor(c => c.DefectNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DeclarationDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerDeclarationDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdContract).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DocId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DefectNo)
                .Length(0, 10)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
