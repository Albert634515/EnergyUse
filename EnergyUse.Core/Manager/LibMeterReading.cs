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
        Models.MeterReading? lastMeterReading;

        var repoMeterReading = new Repositories.RepoMeterReading(_context);
        if (startRange == DateTime.MinValue || endRange == DateTime.MinValue)
            meterReadings = (await repoMeterReading.SelectByEnergyIdAndAddressId(energyTypeId, addressId)).ToList();
        else
            meterReadings = (await repoMeterReading.SelectByRange(startRange.AddDays(-1), endRange.AddDays(1), energyTypeId, addressId)).ToList();

        if (meterReadings.Count > 0)
        {
            lastMeterReading = null;
            meterReadings = meterReadings.OrderBy(o => o.RegistrationDate).ToList();
            foreach (var meterReading in meterReadings)
            {
                meterReading.DeltaLow = 0;
                meterReading.DeltaNormal = 0;
                meterReading.ReturnDeliveryDeltaLow = 0;
                meterReading.ReturnDeliveryDeltaNormal = 0;

                if (lastMeterReading != null)
                {
                    //if (meterReading.RateLow > lastMeterReading.RateLow)
                    lastMeterReading.DeltaLow = meterReading.RateLow - lastMeterReading.RateLow;

                    //if (meterReading.RateNormal > lastMeterReading.RateNormal)
                    lastMeterReading.DeltaNormal = meterReading.RateNormal - lastMeterReading.RateNormal;

                    //if (meterReading.ReturnDeliveryLow > lastMeterReading.ReturnDeliveryLow)
                    lastMeterReading.ReturnDeliveryDeltaLow = meterReading.ReturnDeliveryLow - lastMeterReading.ReturnDeliveryLow;

                    //if (meterReading.ReturnDeliveryNormal > lastMeterReading.ReturnDeliveryNormal)
                    lastMeterReading.ReturnDeliveryDeltaNormal = meterReading.ReturnDeliveryNormal - lastMeterReading.ReturnDeliveryNormal;
                }

                _context.SaveChanges();

                lastMeterReading = meterReading;
            }
        }
    }

    public async Task<List<Models.MeterReading>> RecalculateReadingsDiffPreviousDay(List<Models.MeterReading> meterReadings)
    {
        if (meterReadings.Count > 0)
        {
            Models.MeterReading? lastMeterReading = null;
            meterReadings = meterReadings.OrderBy(o => o.RegistrationDate).ToList();

            foreach (var meterReading in meterReadings)
            {
                var currentReading = meterReading;

                if (lastMeterReading?.MeterId != currentReading.MeterId)
                    lastMeterReading = null;

                CalculateDiff(ref currentReading, lastMeterReading);

                lastMeterReading = meterReading;
            }
        }

        return await Task.FromResult(meterReadings);
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
