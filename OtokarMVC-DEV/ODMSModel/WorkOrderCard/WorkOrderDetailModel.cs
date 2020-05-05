
using System;
namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderDetailModel
    {
        public long Id { get; set; }
        public string MaintenanceName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int MaintenanceId { get; set; }
        public int WorkOrderDetailId { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public double DiscountRatio { get; set; }
        public decimal WarrantyPrice { get; set; }
        public decimal VatRatio { get; set; }
        public decimal? WarrantyRatio { get; set; }
        public string CurrencyCode { get; set; }
        public string Code { get; set; }
        public bool IsCampaign{ get; set; }
        public bool IsBillable { get; set; }
        public bool InvoiceCancel { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal RequiredQuantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public decimal PickedQuantity { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public string FailureCode { get; set; }
        public decimal Duration { get; set; }
        public bool HasOtokarCampaignStock { get; set; }
        public long? CampaignRequestId{ get; set; }
        public string ProcessType { get; set; }
        public string ProcessTypeCode { get; set; }
        public bool GuarantyAuthorizationNeed { get; set; }
        public int WarrantyStatus { get; set; }
        public string WarrantyStatusDesc { get; set; }
        public long? InvoiceId { get; set; }
        public bool IsFleetDiscountApplied { get; set; }
        public bool IsExternalLabour { get; set; }
        public decimal? ProfitMarginRatio { get; set; }
        public decimal? DealerPrice { get; set; }
        public string FailureCodeDescription { get; set; }
        public bool IsMust { get; set; }
        public bool GosApprovalNeed { get; set; }
        public bool IsFromProposal { get; set; }
        public string GuaranteeConfirmDesc { get; set; }
        public bool GosSendCheck { get; set; }
        public string IndicatorTypeCode { get; set; }
        public sbyte LabourWorkStatusId{ get; set; }
        public string LabourWorkStatus { get; set; }
        public string StockType { get; set; }
        public string LabourTechnician { get; set; }
        public DateTime LabourStartDate { get; set; }
        public DateTime LabourFinishDate { get; set; }
        public bool AllowFailureCodeChange { get; set; }
        public long? GifNo { get; set; }
        public decimal? WarrantyTotal { get; set; }
        public int DismantledPartCount { get; set; }
        public string DismantledPart { get; set; }
        public bool WarrantyWillBeApproved { get; set; }
        public bool IsCampaignCritical { get; set; }

        /// <summary>
        /// İskontolu müşteri toplam Fiyat
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// İndirimsiz Toplam Fiyat
        /// </summary>
        public decimal TotalPriceWithoutDiscount { get; set; }
   
        /// <summary>
        /// Toplam İndirim Tutarı
        /// </summary>
        public decimal TotalDiscount { get; set; }
        /// <summary>
        /// Toplam Garanti Fiyatı
        /// </summary>
        public decimal TotalWarrantyPrice { get; set; }
        /// <summary>
        /// Kesintisiz Toplam Garanti Fiyatı
        /// </summary>
        public decimal TotalWarrantyPriceWithoutDeduction { get; set; }
        /// <summary>
        /// Toplam Garanti Kesinti Tutarı
        /// </summary>
        public decimal TotalWarrantyDeductionPrice { get; set; }
        /// <summary>
        /// Toplam Müşteri Fiyatı
        /// </summary>
        public decimal TotalCustomerPrice { get; set; }

    }
}
