using System;
using System.Globalization;
using System.Web.Mvc;
namespace ODMSTerminal.Infrastructure
{
    public class DecimalModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {

                if (CultureInfo.CurrentUICulture.Name == "tr-TR")
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue.Replace(".", ","), CultureInfo.CurrentUICulture);
                else
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentUICulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}