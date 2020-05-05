using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.FavoriteScreen
{
    public class SaveModel : ModelBase
    {
        public List<int> ScreenIdList { get; set; }
        public string SerializedScreenIds
        {
            get
            {
                if (ScreenIdList == null || ScreenIdList.Count == 0)
                    return string.Empty;
                var builder = new StringBuilder();
                ScreenIdList.ForEach(id => {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
