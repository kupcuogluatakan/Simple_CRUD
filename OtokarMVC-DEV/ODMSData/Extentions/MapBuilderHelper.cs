using System;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ODMSData.Extentions
{
    public static class MapBuilderHelper
    {
        public static IMapBuilderContext<T> ExcludeBaseModelProperties<T>(this IMapBuilderContext<T> src, Type type, string exceptions = null)
        {
            var properties = type.GetProperties();

            var exceptionsArr = !string.IsNullOrEmpty(exceptions) ? exceptions.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) : null;

            if (properties != null)
            {
                foreach (var propertyInfo in properties.Where(propertyInfo => exceptionsArr == null || !exceptionsArr.Contains(propertyInfo.Name)))
                {
                    src.DoNotMap(propertyInfo);
                }
            }

            return src;
        }
    }
}
