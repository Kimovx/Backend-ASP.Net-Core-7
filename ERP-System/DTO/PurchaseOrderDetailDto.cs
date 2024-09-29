namespace ERP_System.DTO
{
    public class PurchaseOrderDetailDto
    {
        public int? PurOrderId { get; set; }
        public int? ItemCardId { get; set; }
        public int? StoreId { get; set; }
        public int? UnitId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ItemCardDesc { get; set; }
    }
}
