using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public string AcctId { get; set; }

    public int Extenders { get; set; }
    public int WirelessClientsCount { get; set; }
    public int WiredClientsCount { get; set; }
    public double RxAvgBps { get; set; }
    public double TxAvgBps { get; set; }
    public double RxP95Bps { get; set; }
    public double TxP95Bps { get; set; }
    public double RxMaxBps { get; set; }
    public double TxMaxBps { get; set; }
    public double RssiMean { get; set; }
    public double RssiMedian { get; set; }
    public double RssiMax { get; set; }
    public double RssiMin { get; set; }
    public string NetworkSpeed { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public bool WholeHomeWifi { get; set; }
    public bool WifiSecurity { get; set; }
    public bool WifiSecurityPlus { get; set; }
    public bool PremiumTechPro { get; set; }
    public bool IdentityProtection { get; set; }
    public bool FamilyIdentityProtection { get; set; }
    public bool TotalShield { get; set; }
    public bool YouTubeTv { get; set; }
}