using ODMSCommon.Security;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace ODMS.Core
{
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var date = value.ConvertTo(typeof(DateTime), new CultureInfo("tr"));

            return date;
        }
    }

    public class NullableDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null && value.AttemptedValue != string.Empty && value.AttemptedValue != "undefined")
            {
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "tr")
                    return value.ConvertTo(typeof(DateTime), new CultureInfo(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName));
                else
                {
                    DateTime date;
                    if (DateTime.TryParseExact(value.AttemptedValue, "dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo(UserManager.LanguageCode), DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        return DateTime.ParseExact(value.AttemptedValue, "dd/MM/yyyy", CultureInfo.GetCultureInfo(UserManager.LanguageCode));
                    }
                }
            }
            return null;
        }
    }
}