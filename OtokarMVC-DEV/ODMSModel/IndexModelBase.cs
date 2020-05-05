using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel
{
    public class IndexModelBase
    {
        public IEnumerable<SelectListItem> IsActiveOptions { get; private set; }

        public IndexModelBase()
        {
            IsActiveOptions = new List<SelectListItem>
                {
                    new SelectListItem
                        {
                            Value = true.ToString(),
                            Text = MessageResource.Global_Display_Active
                        },
                    new SelectListItem
                        {
                            Value = false.ToString(),
                            Text = MessageResource.Global_Display_Passive
                        },
                };
        }
    }
}
