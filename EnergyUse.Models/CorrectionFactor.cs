namespace EnergyUse.Models
{
    public partial class CorrectionFactor
    {
        public long Id { get; set; }
        public long? EnergyTypeId { get; set; }
        public decimal Factor { get; set; }
        public DateTime StartFactor { get; set; }
        public DateTime EndFactor { get; set; }


        public virtual EnergyType? EnergyType { get; set; }
    }
}