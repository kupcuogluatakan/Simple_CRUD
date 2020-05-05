using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class StockRackListItem
    {
        /// <summary>
        /// Bundan raf ve part id ye ulaşabilirim
        /// </summary>
        public long StockRackDetail { get; set; }

        public string  RackCode { get; set; }
        /// <summary>
        /// Raftaki Mevcut Miktar
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Topladığım Miktar
        /// </summary>
        public decimal PickQuantity { get; set; }

        public int RackId { get; set; }
    }
}
