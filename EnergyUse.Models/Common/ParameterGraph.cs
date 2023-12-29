using EnergyUse.Common.Enums;

namespace EnergyUse.Models.Common
{
    public class ParameterGraph
    {
        public Address Address { get; set; } = new();
        public List<EnergyType> EnergyTypeList { get; set; } = new();
        public string DbName { get; set; } = "";
        public bool ShowStacked { get; set; }
        public bool ShowAvg { get; set; }
        public bool PredictMissingData { get; set; }
        public DateTime From { get; set; }
        public DateTime Till { get; set; }
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public long TarifGroupId { get; set; }
        public Period PeriodType { get; set; }
        public ShowBy ShowBy { get; set; }
        public ShowType ShowType { get; set; }
        public List<CostCategory> CostCategoryList { get; set; } = new List<CostCategory>();
    }
}