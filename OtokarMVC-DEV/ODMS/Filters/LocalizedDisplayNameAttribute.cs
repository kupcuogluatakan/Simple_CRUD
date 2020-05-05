using System.ComponentModel;

namespace ODMS.Filters
{

    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId)
            : base(GetMessageFromResource(resourceId))
        {
        }

        private static string GetMessageFromResource(string resourceId)
        {
            return ODMSCommon.CommonUtility.GetResourceValue(resourceId);
        }
    }

}