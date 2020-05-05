using System;

namespace ODMSModel.WorkOrderCard
{
    public class FleetInfoModel
    {
        public int FleetId { get; set; }
        public string FleetName { get; set; }
        public string FleetCode { get; set; }
        public decimal OtokarPartDiscountRate { get; set; }
        public decimal OtokarLabourDiscountRate { get; set; }
        public decimal DealerPartDiscountRate { get; set; }
        public decimal DealerLabourDiscountRate { get; set; }
        public bool IsPartRestricted { get; set; }/* Db de Constricted olarak geçio. Constricted kıt, az var anlamında. Restricted burda daha uygun*/
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
