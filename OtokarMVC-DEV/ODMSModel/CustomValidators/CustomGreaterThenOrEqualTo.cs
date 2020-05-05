using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace ODMSModel.CustomValidators
{
    public class CustomGreaterThenOrEqualTo : FluentValidationPropertyValidator
    {
        public CustomGreaterThenOrEqualTo(ModelMetadata metadata,
                                    ControllerContext controllerContext,
                                    PropertyRule rule,
                                    IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
        }

        public override IEnumerable<ModelClientValidationRule>
                                                        GetClientValidationRules()
        {
            if (!ShouldGenerateClientSideRules())
            {
                yield break;
            }

            var validator = Validator as GreaterThanOrEqualValidator;

            var errorMessage = new MessageFormatter()
               .AppendPropertyName(Rule.GetDisplayName())
               .BuildMessage(validator.ErrorMessageSource.GetString());


            if (validator.MemberToCompare == null)
            {
                var rule2 = new ModelClientValidationRule
                {
                    ErrorMessage = errorMessage,
                    ValidationType = "greaterthanorequal"
                };

                
                yield return rule2;
            }

            else
            {

                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = errorMessage,
                    ValidationType = "greaterthanorequaldate"
                };

                //TYPE CONTROL

                rule.ValidationParameters["other"] =
                    CompareAttribute.FormatPropertyForClientValidation(
                        validator.MemberToCompare.Name);
                yield return rule;
            }
        }
    }
}
