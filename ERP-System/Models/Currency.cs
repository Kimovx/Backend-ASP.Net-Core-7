namespace ERP_System.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyDescA { get; set; }
        public string CurrencyDescE { get; set; }
        public decimal Rate { get; set; }
        public bool DefaultCurrency { get; set; }
        public string CurrencySymbol { get; set; }
        public string FractionalUnit { get; set; }
        public byte DecimalPlaces { get; set; }
        public int CurrencyCategoryId { get; set; }
        public string CurrencyType { get; set; }
        public string SingleCurrencyName { get; set; }
        public string SingleCurrencyName2 { get; set; }
        public string DoubleCurrencyName { get; set; }
        public string DoubleCurrencyName2 { get; set; }
        public string CurrencyNameOverThree { get; set; }
        public string CurrencyNameOverThree2 { get; set; }
        public string CollectionCurrencyName { get; set; }
        public string CollectionCurrencyName2 { get; set; }
        public string SingleCurrencyFractionName { get; set; }
        public string SingleCurrencyFractionName2 { get; set; }
        public string DoubleCurrencyFractionName { get; set; }
        public string DoubleCurrencyFractionName2 { get; set; }
        public string CurrencyNameFractionOverThree { get; set; }
        public string CurrencyNameFractionOverThree2 { get; set; }
        public string CollectionCurrencyFractionName { get; set; }
        public string CollectionCurrencyFractionName2 { get; set; }
        public string SingleCurrencyTempName { get; set; }
        public string DoubleCurrencyTempName { get; set; }
        public string CurrencyNameTempOver3 { get; set; }
        public string CollectionCurrencyTempName { get; set; }
        public int Aid { get; set; }
        public decimal EquivalentConversionPrice { get; set; }
        public DateTime LastModify { get; set; }
        public string EtaxUnitCode { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; } // Nullable if not always set
    }
}
