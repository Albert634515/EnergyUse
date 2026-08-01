namespace EnergyUse.Models;

using System.ComponentModel.DataAnnotations.Schema;

public partial class PreDefinedPeriod
{
    public PreDefinedPeriod()
    {
        PreDefinedPeriodDates = new HashSet<PreDefinedPeriodDate>();
        Payments = new HashSet<Payment>();
    }

    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;

    [NotMapped]
    public DateTime? StartDate => PreDefinedPeriodDates.Count == 0
        ? null
        : PreDefinedPeriodDates.Min(date => date.StartDate);

    [NotMapped]
    public DateTime? EndDate => PreDefinedPeriodDates.Count == 0
        ? null
        : PreDefinedPeriodDates.Max(date => date.EndDate);

    public virtual ICollection<PreDefinedPeriodDate> PreDefinedPeriodDates { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
}
