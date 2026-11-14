using System.ComponentModel;
using EnergyUse.Models;

namespace EnergyUse.Core.Import;

public enum ImportStatus { New, Changed, Unchanged, Conflict }

public sealed class CsvMeterReading : MeterReading
{
    public int SourceRow { get; init; }
}

public sealed record ImportChoice(string Description, MeterReading? Reading)
{
    public override string ToString() => Description;
}

public sealed class ImportPreviewRow : INotifyPropertyChanged
{
    public DateTime Date { get; init; }
    public long? MeterId { get; init; }
    public string MeterName { get; init; } = "";
    public ImportStatus Status { get; init; }
    public bool IsImported { get; init; } = true;
    public bool CanChoose => Choices.Count > 0;
    internal MeterReading? StoredReading { get; init; }
    private MeterReading? displayedReading => SelectedChoice?.Reading ?? StoredReading;
    public DateTime RegistrationDate => Date;
    public long? Id => StoredReading?.Id;
    public decimal? RateNormal => displayedReading?.RateNormal;
    public decimal? RateLow => displayedReading?.RateLow;
    public decimal? ReturnDeliveryNormal => displayedReading?.ReturnDeliveryNormal;
    public decimal? ReturnDeliveryLow => displayedReading?.ReturnDeliveryLow;
    public decimal? DeltaNormal => StoredReading?.DeltaNormal;
    public decimal? DeltaLow => StoredReading?.DeltaLow;
    public decimal? ReturnDeliveryDeltaNormal => StoredReading?.ReturnDeliveryDeltaNormal;
    public decimal? ReturnDeliveryDeltaLow => StoredReading?.ReturnDeliveryDeltaLow;
    public string StatusText => !IsImported && Status == ImportStatus.Unchanged ? "Stored" : Status switch
    {
        ImportStatus.New => "New", ImportStatus.Changed => "Changed",
        ImportStatus.Unchanged => "Unchanged", _ => "Conflict"
    };
    public string SourceRows { get; init; } = "";
    public string ExistingValues { get; init; } = "";
    public string ExistingConsumption { get; init; } = "";
    public string IncomingValues { get; init; } = "";
    public string Reason { get; init; } = "";
    public IReadOnlyList<ImportChoice> Choices { get; init; } = [];
    internal long? ExistingId { get; init; }
    private ImportChoice? _selectedChoice;
    public ImportChoice? SelectedChoice
    {
        get => _selectedChoice;
        set
        {
            if (value != null && !Choices.Contains(value))
                throw new ArgumentException("Select a choice from this row.");
            _selectedChoice = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}

public sealed class ImportPreview
{
    public IReadOnlyList<ImportPreviewRow> Rows { get; internal init; } = [];
    public int SourceCount { get; internal init; }
    public int ExtraSourceRows => SourceCount - Rows.Count;
    public int NewCount => Rows.Count(row => row.Status == ImportStatus.New);
    public int ChangedCount => Rows.Count(row => row.Status == ImportStatus.Changed);
    public int ConflictCount => Rows.Count(row => row.Status == ImportStatus.Conflict);
    public int UnchangedCount => Rows.Count(row => row.Status == ImportStatus.Unchanged);
    public int UnresolvedCount => Rows.Count(row => row.SelectedChoice == null);
    public int SelectedCount => Rows.Count(row => row.SelectedChoice?.Reading != null && row.Status != ImportStatus.Unchanged);
    public bool IsSaved { get; internal set; }
    public string Summary => $"Per address + energy type + day: {SourceCount} source rows · {NewCount} new · {ChangedCount} changed · " +
        $"{UnchangedCount} unchanged · {ConflictCount} conflicts · {UnresolvedCount} unresolved · {SelectedCount} to apply · {ExtraSourceRows} extra source rows on the same day";
    internal long AddressId { get; init; }
    internal long EnergyTypeId { get; init; }
    internal string DatabasePath { get; init; } = "";
    internal IReadOnlyList<ReadingSnapshot> OriginalReadings { get; init; } = [];
    internal IReadOnlyList<MeterSnapshot> OriginalMeters { get; init; } = [];
}

internal sealed record ReadingSnapshot(long? Id, long? MeterId, long? EnergyTypeId, DateTime Date,
    int Week, decimal Normal, decimal Low, decimal ReturnNormal, decimal ReturnLow,
    decimal DeltaNormal, decimal DeltaLow, decimal ReturnDeltaNormal, decimal ReturnDeltaLow)
{
    internal static ReadingSnapshot From(MeterReading row) => new(row.Id, row.MeterId, row.EnergyTypeId,
        row.RegistrationDate, row.WeekNo, row.RateNormal, row.RateLow, row.ReturnDeliveryNormal, row.ReturnDeliveryLow,
        row.DeltaNormal, row.DeltaLow, row.ReturnDeliveryDeltaNormal, row.ReturnDeliveryDeltaLow);
}

internal sealed record MeterSnapshot(long Id, DateTime ActiveFrom, DateTime? Till, long? AddressId, long? EnergyTypeId)
{
    internal static MeterSnapshot From(Meter row) => new(row.Id, row.ActiveFrom, row.ActiveTill, row.AddressId, row.EnergyTypeId);
}
