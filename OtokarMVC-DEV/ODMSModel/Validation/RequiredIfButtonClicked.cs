using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ODMSModel.Validation
{
    public class RequiredIfButtonClicked : ValidationAttribute, IClientValidatable
    {
        private RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string ButtonName { get; set; }

        public RequiredIfButtonClicked(string buttonName)
        {
            ButtonName = buttonName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value == null && (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[ButtonName]) ||(!string.IsNullOrEmpty(HttpContext.Current.Request.Form["action:"+ButtonName])))))
            {
                if (!_innerAttribute.IsValid(value))
                {
                    ErrorMessage = "alican";
                    return new ValidationResult(ErrorMessage, new[] { validationContext.DisplayName });
                    //return new ValidationResult("a", new[] { "AdminDesc" });
                    //return new ValidationResult("a", new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }

        #region IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                {
                    ErrorMessage = FormatErrorMessage("a"),
                    //ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                    ValidationType = "requiredifbuttonclicked"
                };
            rule.ValidationParameters.Add("buttonname", ButtonName);
            yield return rule;
        }

        #endregion
    }
}