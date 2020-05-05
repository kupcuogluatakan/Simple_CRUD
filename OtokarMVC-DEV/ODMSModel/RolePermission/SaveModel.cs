using System.Collections.Generic;
using System.Text;

namespace ODMSModel.RolePermission
{
    public class SaveModel : ModelBase
    {
        public int RoleId { get; set; }
        public List<int> PermissionIdList { get; set; }

        public string SerializedPermissionIds
        {
            get
            {
                if (PermissionIdList == null || PermissionIdList.Count == 0)
                    return string.Empty;

                var builder = new StringBuilder();
                PermissionIdList.ForEach(id =>
                {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
