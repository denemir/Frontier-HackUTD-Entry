using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Add DbSet<T> properties for your tables
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
    .ToTable("current_customers")
    .Property(c => c.AcctId).HasColumnName("acct_id")
    .IsRequired();

        modelBuilder.Entity<Customer>()
            .Property(c => c.Extenders).HasColumnName("extenders");

        modelBuilder.Entity<Customer>()
            .Property(c => c.WirelessClientsCount).HasColumnName("wireless_clients_count");

        modelBuilder.Entity<Customer>()
            .Property(c => c.WiredClientsCount).HasColumnName("wired_clients_count");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RxAvgBps).HasColumnName("rx_avg_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.TxAvgBps).HasColumnName("tx_avg_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RxP95Bps).HasColumnName("rx_p95_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.TxP95Bps).HasColumnName("tx_p95_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RxMaxBps).HasColumnName("rx_max_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.TxMaxBps).HasColumnName("tx_max_bps");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RssiMean).HasColumnName("rssi_mean");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RssiMedian).HasColumnName("rssi_median");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RssiMax).HasColumnName("rssi_max");

        modelBuilder.Entity<Customer>()
            .Property(c => c.RssiMin).HasColumnName("rssi_min");

        modelBuilder.Entity<Customer>()
            .Property(c => c.NetworkSpeed).HasColumnName("network_speed");

        modelBuilder.Entity<Customer>()
            .Property(c => c.City).HasColumnName("city");

        modelBuilder.Entity<Customer>()
            .Property(c => c.State).HasColumnName("state");

        modelBuilder.Entity<Customer>()
            .Property(c => c.WholeHomeWifi).HasColumnName("whole_home_wifi");

        modelBuilder.Entity<Customer>()
            .Property(c => c.WifiSecurity).HasColumnName("wifi_security");

        modelBuilder.Entity<Customer>()
            .Property(c => c.WifiSecurityPlus).HasColumnName("wifi_security_plus");

        modelBuilder.Entity<Customer>()
            .Property(c => c.PremiumTechPro).HasColumnName("premium_tech_pro");

        modelBuilder.Entity<Customer>()
            .Property(c => c.IdentityProtection).HasColumnName("identity_protection");

        modelBuilder.Entity<Customer>()
            .Property(c => c.FamilyIdentityProtection).HasColumnName("family_identity_protection");

        modelBuilder.Entity<Customer>()
            .Property(c => c.TotalShield).HasColumnName("total_shield");

        modelBuilder.Entity<Customer>()
            .Property(c => c.YouTubeTv).HasColumnName("youtube_tv");

        base.OnModelCreating(modelBuilder);
    }
}
