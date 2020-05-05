using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Handlers;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingInfo:IResponse
    {

        public long PickingId { get; set; }

        public string PlateAndCustomer { get; set; }

        public DateTime Date { get; set; }

        public string Source { get; set; }

        public long OrderId { get; set; }

        public string PartCode { get; set; }

        public EnumerableResponse<PickingPartsListItem> Parts { get; set; }

        public string UpdateUser { get; set; }

        public int StatusId { get; set; }

        //public long PickingId { get; set; }
        //public string WaybillNo { get; set; }
        //public string SapNo { get; set; }
        //public DateTime WaybillDate { get; set; }
        //public string AcceptUser { get; set; }
        //public string Supplier { get; set; }

    }
}
