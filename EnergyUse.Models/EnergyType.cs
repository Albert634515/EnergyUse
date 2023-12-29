namespace EnergyUse.Models
{
    public partial class EnergyType
    {
        public EnergyType()
        {
            AdditionalCategoryAndGroupInfos = new HashSet<AdditionalCategoryAndGroupInfo>();
            CorrectionFactors = new HashSet<CorrectionFactor>();
            CostCategories = new HashSet<CostCategory>();
            MeterReadings = new HashSet<MeterReading>();
            Meters = new HashSet<Meter>();
            Nettings = new HashSet<Netting>();
            PreDefinedPeriodDates = new HashSet<PreDefinedPeriodDate>();
            Rates = new HashSet<Rate>();
        }


        public long Id { get; set; }
        public string Name { get; set; } = "";
        public bool HasNormalAndLow { get; set; }
        public bool HasEnergyReturn { get; set; }
        public bool DefaultType { get; set; }

        public string? UnitId { get; set; }

        public virtual Unit Unit { get; set; }

        // Readonly props
        public string UnitName
        {
            get
            {
                if (Unit == null)
                    return string.Empty;
                else
                    return Unit.Description;
            }
        }

        public virtual ICollection<AdditionalCategoryAndGroupInfo> AdditionalCategoryAndGroupInfos { get; set; }
        public virtual ICollection<CorrectionFactor> CorrectionFactors { get; set; }
        public virtual ICollection<CostCategory> CostCategories { get; set; }
        public virtual ICollection<MeterReading> MeterReadings { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
        public virtual ICollection<Netting> Nettings { get; set; }
        public virtual ICollection<PreDefinedPeriodDate> PreDefinedPeriodDates { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<CalculatedUnitPrice> CalculatedUnitPrices { get; set; }
    }
}
