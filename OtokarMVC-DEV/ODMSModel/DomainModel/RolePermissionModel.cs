using System;

namespace ODMSModel.DomainModel
{
    [Serializable]
    public class RolePermissionModel : ModelBase
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
