using EnergyUse.Core.Context;

namespace EnergyUse.Core.Manager;

public class LibMeterReading
{
    #region Properties

    private readonly EnergyUseContext _context;

    #endregion

    public LibMeterReading(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);
    }

    /// <summary>
    /// Calculate Delta based on value of previous day
    /// </summary>
    /// <param name="startRange">Start of calculation range</param>
    /// <param name="endRange">End of calculation range</param>
    /// <param name="energyTypeId">Energy type id</param>
    public async Task RecalculateReadingsDiffPreviousDay(DateTime startRange, DateTime endRange, long energyTypeId, long addressId)
    {
        List<Models.MeterReading> meterReadings;

        var repoMeterReading = new Repositories.RepoMeterReading(_context);
        var recalculateAll = startRange == DateTime.MinValue || endRange == DateTime.MinValue;
        if (recalculateAll)
            meterReadings = (await repoMeterReading.SelectByEnergyIdAndAddressId(energyTypeId, addressId)).ToList();
        else
            meterReadings = (await repoMeterReading.SelectByRange(startRange.AddDays(-1), endRange.AddDays(1), energyTypeId, addressId)).ToList();

        if (meterReadings.Count > 0)
        {
            if (recalculateAll)
            {
                var repoMeter = new Repositories.RepoMeter(_context);
                var meters = (await repoMeter.SelectByAddressAndEnergyType(addressId, energyTypeId)).ToList();
                correctMeterAssignments(meterReadings, meters);
            }

            calculateDiffPerMeter(meterReadings);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Models.MeterReading>> RecalculateReadingsDiffPreviousDay(List<Models.MeterReading> meterReadings)
    {
        if (meterReadings.Count > 0)
        {
            calculateDiffPerMeter(meterReadings);
            meterReadings = meterReadings.OrderBy(o => o.RegistrationDate).ToList();
        }

        return await Task.FromResult(meterReadings);
    }

    private static void correctMeterAssignments(
        List<Models.MeterReading> meterReadings,
        List<Models.Meter> meters)
    {
        var assignments = new List<(Models.MeterReading Reading, Models.Meter Meter)>();

        foreach (var meterReading in meterReadings)
        {
            var registrationDate = meterReading.RegistrationDate.Date;
            var matchingMeters = meters
                .Where(meter => meter.ActiveFrom.Date <= registrationDate
                             && (meter.ActiveTill == null || meter.ActiveTill.Value.Date >= registrationDate))
                .ToList();

            if (matchingMeters.Count == 0)
            {
                throw new InvalidOperationException(
                    $"No meter is available for reading {meterReading.Id} on {registrationDate:yyyy-MM-dd}. " +
                    "Correct the meter periods before recalculating all data.");
            }

            if (matchingMeters.Count > 1)
            {
                throw new InvalidOperationException(
                    $"Multiple meters are active for reading {meterReading.Id} on {registrationDate:yyyy-MM-dd}. " +
                    "Correct the overlapping meter periods before recalculating all data.");
            }

            assignments.Add((meterReading, matchingMeters[0]));
        }

        var duplicateAssignment = assignments
            .GroupBy(assignment => new
            {
                assignment.Meter.Id,
                Date = assignment.Reading.RegistrationDate.Date
            })
            .FirstOrDefault(group => group.Count() > 1);

        if (duplicateAssignment != null)
        {
            throw new InvalidOperationException(
                $"Multiple readings would be assigned to meter {duplicateAssignment.Key.Id} " +
                $"on {duplicateAssignment.Key.Date:yyyy-MM-dd}. Resolve the duplicate readings before recalculating all data.");
        }

        foreach (var assignment in assignments.Where(item => item.Reading.MeterId != item.Meter.Id))
        {
            assignment.Reading.MeterId = assignment.Meter.Id;
            assignment.Reading.Meter = assignment.Meter;
        }
    }

    private void calculateDiffPerMeter(IEnumerable<Models.MeterReading> meterReadings)
    {
        foreach (var readingsForMeter in meterReadings.GroupBy(reading => reading.MeterId))
        {
            Models.MeterReading? lastMeterReading = null;

            foreach (var meterReading in readingsForMeter.OrderBy(reading => reading.RegistrationDate))
            {
                var currentReading = meterReading;
                CalculateDiff(ref currentReading, lastMeterReading);
                lastMeterReading = meterReading;
            }
        }
    }

    public void CalculateDiff(ref Models.MeterReading meterReading, Models.MeterReading? lastMeterReading)
    {
        meterReading.DeltaLow = 0;
        meterReading.DeltaNormal = 0;
        meterReading.ReturnDeliveryDeltaLow = 0;
        meterReading.ReturnDeliveryDeltaNormal = 0;

        if (lastMeterReading != null)
        {
            if (meterReading.RateLow > lastMeterReading.RateLow)
                lastMeterReading.DeltaLow = meterReading.RateLow - lastMeterReading.RateLow;

            if (meterReading.RateNormal > lastMeterReading.RateNormal)
                lastMeterReading.DeltaNormal = meterReading.RateNormal - lastMeterReading.RateNormal;

            if (meterReading.ReturnDeliveryLow > lastMeterReading.ReturnDeliveryLow)
                lastMeterReading.ReturnDeliveryDeltaLow = meterReading.ReturnDeliveryLow - lastMeterReading.ReturnDeliveryLow;

            if (meterReading.ReturnDeliveryNormal > lastMeterReading.ReturnDeliveryNormal)
                lastMeterReading.ReturnDeliveryDeltaNormal = meterReading.ReturnDeliveryNormal - lastMeterReading.ReturnDeliveryNormal;
        }
    }
}
