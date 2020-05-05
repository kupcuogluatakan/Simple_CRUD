using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.GeneralParameter
{
    public class GeneralParameterViewModelValidator : AbstractValidator<GeneralParameterViewModel>
    {
        public GeneralParameterViewModelValidator()
        {
            RuleFor(c => c.Type).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.ParameterId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Type).Must(InPreDefinedValues).WithMessage(MessageResource.Err_Generic_Unexpected);
        }

        private bool InPreDefinedValues(string type)
        {
            return type == "I" || type == "N" || type == "D" || type == "B" || type == "T";

        }
    }
}
