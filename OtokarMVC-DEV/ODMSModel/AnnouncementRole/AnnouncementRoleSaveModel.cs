using System;
using System.Collections.Generic;
using System.Text;

namespace ODMSModel.AnnouncementRole
{
    public class AnnouncementRoleSaveModel : ModelBase
    {
        public Int64 IdAnnouncement { get; set; }
        public List<int> RoleTypeIdList { get; set; }

        public string SerializedRoleTypeIds
        {
            get
            {
                if (RoleTypeIdList == null || RoleTypeIdList.Count == 0)
                    return string.Empty;

                var builder = new StringBuilder();
                RoleTypeIdList.ForEach(id =>
                {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
