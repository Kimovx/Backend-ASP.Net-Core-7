namespace ERP_System.Models
{    public class AnalyticalCode
    {
        public int AId { get; set; }
        public int? ParentAId { get; set; } // Nullable if it can be null
        public int CodeLevel { get; set; }
        public byte CodeLevelType { get; set; }
        public string Code { get; set; }
        public string DescA { get; set; }
        public string DescE { get; set; }
        public bool Stopped { get; set; }
        public int AccountId { get; set; }
        public int CostCenterId { get; set; }
        public int AssetId { get; set; }
        public int EmpId { get; set; }
        public int BSPartnerId { get; set; }
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public int ItemCardId { get; set; }
        public int AccomulatAId { get; set; }
    
        // Additional fields
        public string AddField1 { get; set; }
        public string AddField2 { get; set; }
        public string AddField3 { get; set; }
        public string AddField4 { get; set; }
        public string AddField5 { get; set; }
        public string AddField6 { get; set; }
        public string AddField7 { get; set; }
        public string AddField8 { get; set; }
        public string AddField9 { get; set; }
        public string AddField10 { get; set; }
        public string AddField11 { get; set; }
        public string AddField12 { get; set; }
        public string AddField13 { get; set; }
        public string AddField14 { get; set; }
        public string AddField15 { get; set; }
        public string AddField16 { get; set; }
        public string AddField17 { get; set; }
        public string AddField18 { get; set; }
        public string AddField19 { get; set; }
        public string AddField20 { get; set; }
        public string AddField21 { get; set; }
        public string AddField22 { get; set; }
        public string AddField23 { get; set; }
        public string AddField24 { get; set; }
        public string AddField25 { get; set; }
        public string AddField26 { get; set; }
        public string AddField27 { get; set; }
        public string AddField28 { get; set; }
        public string AddField29 { get; set; }
        public string AddField30 { get; set; }

        // Date fields
        public DateTime? AnDate1 { get; set; } // Nullable
        public DateTime? AnDate2 { get; set; } // Nullable
        public DateTime? AnDate3 { get; set; } // Nullable
        public bool IsNotify1 { get; set; }
        public DateTime? NotifyDate1 { get; set; } // Nullable
        public bool IsNotify2 { get; set; }
        public DateTime? NotifyDate2 { get; set; } // Nullable
        public bool IsNotify3 { get; set; }
        public DateTime? NotifyDate3 { get; set; } // Nullable

        public string RemarksA { get; set; }
        public string RemarksE { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; } // Nullable
    }
}
