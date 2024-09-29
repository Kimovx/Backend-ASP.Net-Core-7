namespace ERP_System.DTO
{
    public class PurchaseOrderDto
    {
        public int? PurOrderId {  get; set; }
        public int? BookId { get; set; }
        public int? AId{ get; set; }
        public int? TermType { get; set; }
        public DateTime? TrDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? DeliveryPeriodDays { get; set; }
        public int? PayPeriodDays { get; set; }
        public string? ManualTrNo { get; set; }
        public int? InvoiceType { get; set; }
        public decimal? Rate { get; set; }
        public int? VendorId { get; set; }
        public int? CurrencyId { get; set; }
    }
}
