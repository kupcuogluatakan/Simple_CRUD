using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.DomainModel
{
    [Serializable]
    public class PermissionModel
    {
        public PermissionModel()
        {
            
        }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
    }
}
