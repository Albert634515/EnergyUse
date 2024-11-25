using EnergyUse.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Context;

public partial class EnergyUseContext : DbContext
{
    private readonly string _dbFileName;

    public EnergyUseContext(string dbFileName) : base()
    {
        //_dbFileName = "c:\\Temp\\EnergyUse_test.sqlite";
        //_dbFileName = "c:\\Temp\\EnergyUse.db";
        _dbFileName = dbFileName;

        // Start with a clean database
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={_dbFileName}");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalCategoryAndGroupInfo>(entity =>
        {
            entity.Property(r => r.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (255)");

            entity.HasOne(d => d.CostCategory).WithMany(p => p.AdditionalCategoryAndGroupInfos).HasForeignKey(d => d.CostCategoryId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.EnergyType).WithMany(p => p.AdditionalCategoryAndGroupInfos).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.AdditionalCategoryAndGroupInfos).HasForeignKey(d => d.TariffGroupId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "ACG_EnergyTypeId");
        });


        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.City).HasColumnType("VARCHAR (100)");
            entity.Property(e => e.DefaultAddress).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(e => e.Description).IsRequired().HasColumnType("VARCHAR (100)");                
            entity.Property(e => e.HouseNumber).HasColumnType("VARCHAR (15)");
            entity.Property(e => e.PostalCode).HasColumnType("VARCHAR (15)");
            entity.Property(e => e.Street).HasColumnType("VARCHAR (150)");

            entity.Property(e => e.SolarPanelsAvailable).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(e => e.TotalCapacity).HasColumnType("INTEGER (5)").HasDefaultValueSql("0");

            entity.HasOne(d => d.TariffGroup).WithMany(p => p.Addresses).HasForeignKey(d => d.DefaultTariffGroupId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.Addresses).HasForeignKey(d => d.GeneralTariffGroupId).OnDelete(DeleteBehavior.NoAction);
        });


        modelBuilder.Entity<CalculationType>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (100)");
        });

        modelBuilder.Entity<CorrectionFactor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();                
            entity.Property(e => e.Factor).HasColumnType("DECIMAL (10, 5)");
            entity.Property(e => e.Factor).HasPrecision(10, 5); 
            entity.Property(e => e.StartFactor).HasColumnType("DATE");
            entity.Property(e => e.EndFactor).HasColumnType("DATE");
            entity.Property(e => e.EnergyTypeId).IsRequired();
            entity.HasOne(d => d.EnergyType).WithMany(p => p.CorrectionFactors).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "CF_EnergyTypeId");
        });

        modelBuilder.Entity<CostCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CalculateVat).HasColumnType("BOOLEAN").HasDefaultValueSql("True");
            entity.Property(e => e.CanNotBeNegative).HasColumnType("BOOLEAN").HasDefaultValueSql("False");
            entity.Property(e => e.Name).HasColumnType("VARCHAR (50)");
            entity.Property(e => e.NotCalculateOverReturn).HasColumnType("BOOLEAN").HasDefaultValueSql("False");
            entity.Property(e => e.SortOrder).HasColumnType("INTEGER (3)");
            entity.Property(e => e.UnitId).HasColumnType("VARCHAR (15)");
            entity.Property(e => e.Start).HasColumnType("DATE");
            entity.Property(e => e.End).HasColumnType("DATE");

            entity.HasOne(d => d.CalculationType).WithMany(p => p.CostCategories).HasForeignKey(d => d.CalculationTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.EnergySubType).WithMany(p => p.CostCategories).HasForeignKey(d => d.EnergySubTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.EnergyType).WithMany(p => p.CostCategories).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.CostCategories).HasForeignKey(d => d.TariffGroupId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Unit).WithMany(p => p.CostCategories).HasForeignKey(d => d.UnitId).OnDelete(DeleteBehavior.NoAction);
          
            entity.HasIndex(i => i.EnergyTypeId, "CC_EnergyTypeId");
        });

        modelBuilder.Entity<CostType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (100)");
            entity.Property(e => e.Name).HasColumnType("VARCHAR (25)").IsRequired();

            entity.HasOne(d => d.CostCategory).WithMany(p => p.CostTypes).HasForeignKey(d => d.CostCategoryId);
        });

        modelBuilder.Entity<CalculatedUnitPrice>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Price).HasColumnType("DECIMAL (10, 5)");
            entity.Property(e => e.Year).HasColumnType("NUMERIC (4)").IsRequired();

            entity.HasOne(d => d.EnergyType).WithMany(p => p.CalculatedUnitPrices).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.CalculatedUnitPrices).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<EnergySubType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (25)").IsRequired();
        });

        modelBuilder.Entity<EnergyType>().Property(r => r.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<EnergyType>().Property(r => r.Name).HasColumnType("VARCHAR (25)");

        modelBuilder.Entity<EnergyType>(entity =>
        {
            entity.Property(r => r.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.DefaultType).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(e => e.HasEnergyReturn).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(e => e.HasNormalAndLow).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(e => e.Name).HasColumnType("VARCHAR (25)");
            entity.Property(e => e.UnitId).HasColumnType("VARCHAR (15)");

            entity.HasOne(d => d.Unit).WithMany(p => p.EnergyTypes).HasForeignKey(d => d.UnitId);
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Active).HasColumnType("BOOLEAN").HasDefaultValueSql("false");
            entity.Property(p => p.AddressId).HasDefaultValueSql("0");
            entity.Property(p => p.Description).HasColumnType("VARCHAR (50)");
            entity.Property(p => p.Number).HasColumnType("VARCHAR (50)");
            entity.Property(p => p.ActiveFrom).HasColumnType("DATE");

            entity.HasOne(h => h.Address).WithMany(p => p.Meters).HasForeignKey(d => d.AddressId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(h => h.EnergyType).WithMany(p => p.Meters).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "MT_EnergyTypeId");
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasIndex(r => r.Id, "MR_MeterReadings_Id").IsUnique();
            entity.Property(r => r.Id).ValueGeneratedOnAdd();
            entity.Property(r => r.RegistrationDate).HasColumnType("DATETIME");
            entity.Property(r => r.WeekNo).HasColumnType("NUMERIC (2)");

            entity.Property(r => r.RateLow).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.RateNormal).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.ReturnDeliveryLow).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.ReturnDeliveryNormal).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.DeltaLow).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.DeltaNormal).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.ReturnDeliveryDeltaLow).HasColumnType("DECIMAL (10, 2)");
            entity.Property(r => r.ReturnDeliveryDeltaNormal).HasColumnType("DECIMAL (10, 2)");

            entity.HasOne(d => d.EnergyType).WithMany(p => p.MeterReadings).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Meter).WithMany(p => p.MeterReadings).HasForeignKey(d => d.MeterId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "MR_EnergyTypeId");
            entity.HasIndex(i => i.MeterId, "MR_MeterId");
        });

        modelBuilder.Entity<Netting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EndDate).HasColumnType("DATE");
            entity.Property(e => e.Rate).HasColumnType("DECIMAL (20, 2)");
            entity.Property(e => e.StartDate).HasColumnType("DATE");
            entity.HasOne(d => d.EnergyType).WithMany(p => p.Nettings).HasForeignKey(d => d.EnergyTypeId);

            entity.HasIndex(i => i.EnergyTypeId, "NT_EnergyTypeId");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (50)");
            entity.Property(e => e.PayDate).IsRequired().HasColumnType("DATE");
            entity.Property(e => e.Amount).HasColumnType("DECIMAL (20, 2)");

            entity.HasOne(d => d.Address).WithMany(p => p.Payments).HasForeignKey(d => d.AddressId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.PreDefinedPeriod).WithMany(p => p.Payments).HasForeignKey(d => d.PreDefinedPeriodId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.AddressId, "PM_AddressId");
        });

        modelBuilder.Entity<PreDefinedPeriod>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (50)").IsRequired();
        });

        modelBuilder.Entity<PreDefinedPeriodDate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EndDate).IsRequired().HasColumnType("DATE");
            entity.Property(e => e.StartDate).IsRequired().HasColumnType("DATE");

            entity.HasOne(d => d.EnergyType).WithMany(p => p.PreDefinedPeriodDates).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.PreDefinedPeriod).WithMany(p => p.PreDefinedPeriodDates).HasForeignKey(d => d.PreDefinedPeriodId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.PreDefinedPeriodDates).HasForeignKey(d => d.TariffGroupId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "PPD_EnergyTypeId");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (50)");
            entity.Property(e => e.StartRate).HasColumnType("DATE");
            entity.Property(e => e.EndRate).HasColumnType("DATE");
            entity.Property(e => e.ExpectedPriceChange).HasColumnType("DECIMAL (3, 2)").HasDefaultValueSql("0");
            entity.Property(e => e.PriceChange).HasColumnType("DECIMAL (3, 2)").HasDefaultValueSql("0");
            entity.Property(e => e.RateValue).HasColumnType("DECIMAL (10, 5)").HasDefaultValueSql("0");
            entity.Property(e => e.RateTypeId).HasColumnType("NUMERIC (2)");

            entity.HasOne(d => d.CostCategory).WithMany(p => p.Rates).HasForeignKey(d => d.CostCategoryId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.EnergyType).WithMany(p => p.Rates).HasForeignKey(d => d.EnergyTypeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.TariffGroup).WithMany(p => p.Rates).HasForeignKey(d => d.TariffGroupId).OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(i => i.EnergyTypeId, "RT_EnergyTypeId");
            entity.HasIndex(i => i.RateTypeId, "RT_RateTypeId");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Id, "ST_Settings_Id").IsUnique();
            entity.HasIndex(e => e.Key, "ST_Settings_Key").IsUnique();

            entity.Property(e => e.Key).IsRequired().HasColumnType("VARCHAR (20)");
            entity.Property(e => e.KeyValue).HasColumnType("VARCHAR (254)");
        });

        modelBuilder.Entity<Staffel>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Id, "SF_Id").IsUnique();
            entity.HasIndex(e => e.RateId, "SF_RateId");

            entity.Property(e => e.ValueFrom).IsRequired().HasColumnType("DECIMAL (10, 5)");
            entity.Property(e => e.ValueTill).IsRequired().HasColumnType("DECIMAL (10, 5)");
            entity.Property(e => e.StaffelValue).HasColumnType("DECIMAL (10, 5)").HasDefaultValueSql("0");

            entity.HasOne(d => d.Rate).WithMany(p => p.Staffels).HasForeignKey(d => d.RateId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<TariffGroup>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasColumnType("VARCHAR (100)").IsRequired();
            entity.Property(e => e.TypeId).HasColumnType("NUMERIC (2)").IsRequired();
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(e => e.Id).HasColumnType("VARCHAR (15)");
            entity.Property(e => e.Description).HasColumnType("VARCHAR (30)").IsRequired();
        });

        modelBuilder.Entity<VatTarif>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.StartDate).HasColumnType("DATE");
            entity.Property(e => e.EndDate).HasColumnType("DATE");
            entity.Property(e => e.Tarif).HasColumnType("DECIMAL (10, 2)");                
            entity.HasOne(d => d.CostCategory).WithMany(p => p.VatTarifs).HasForeignKey(d => d.CostCategoryId);
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Seed();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    // Entities
    public DbSet<AdditionalCategoryAndGroupInfo> AdditionalCategoryAndGroupInfos { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public DbSet<CalculationType> CalculationTypes { get; set; }
    public DbSet<CorrectionFactor> CorrectionFactors { get; set; }
    public DbSet<CostCategory> CostCategories { get; set; }
    public DbSet<EnergySubType> EnergySubTypes { get; set; }
    public DbSet<EnergyType> EnergyTypes { get; set; }
    public DbSet<Meter> Meters { get; set; }
    public DbSet<MeterReading> MeterReadings { get; set; }
    public DbSet<Netting> Nettings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PreDefinedPeriod> PreDefinedPeriods { get; set; }
    public DbSet<PreDefinedPeriodDate> PreDefinedPeriodDates { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<TariffGroup> TariffGroups { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<VatTarif> VatTarifs { get; set; }
    public DbSet<Staffel> Staffels { get; set; }
}