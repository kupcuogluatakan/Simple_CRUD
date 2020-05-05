using System;

namespace ODMSCommon.Security
{
    [Serializable]
    public class PermissionInfo
    {
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public int IsOtokarScreen { get; set; }
    }
}
