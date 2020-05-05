using System.Collections.Generic;
using ODMSModel;

namespace ODMSModel.AnnouncementDealer
{
    
    public class AnnouncementDealerModel:ModelBase
    {
        public int AnnouncementId { get; set; }
        public List<int> DealerList { get; set; }
    }
}
