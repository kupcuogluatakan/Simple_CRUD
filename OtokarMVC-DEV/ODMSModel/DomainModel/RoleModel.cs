using System.Collections.Generic;
using ODMSModel.ViewModel;

namespace ODMSModel.DomainModel
{
    public class RoleModel : ModelBase
    {
        public int RoleTypeId { get; set; }
        public string AdminDesc { get; set; }
        public bool DealerRole { get; set; }
        public bool AllowWorkshop { get; set; }
        public List<MultiLanguageContentViewModel> RoleTypeName { get; set; }
    }
}
