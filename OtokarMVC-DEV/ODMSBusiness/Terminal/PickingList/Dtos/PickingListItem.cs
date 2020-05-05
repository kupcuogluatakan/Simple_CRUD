using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingListItem
    {
        public long PickingId { get; set; }

        public string PlateAndCustomer{ get; set; }

        public DateTime  Date { get; set; }

        public string Source { get; set; }

        public long OrderId { get; set; }

    }
}
