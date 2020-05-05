using System.Collections.Generic;

namespace ODMSModel.GuaranteeAuthorityGroupMembers
{
    public class GuaranteeAuthorityGroupMembersModel : ModelBase
    {
        public int GroupId { get; set; }
        public List<int> UserList { get; set; }
    }
}
