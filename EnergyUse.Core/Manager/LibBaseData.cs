using EnergyUse.Models;

namespace EnergyUse.Core.Manager;

public class LibBaseData
{
    #region GetDefaultData

    public static Models.Address GetDemoAddress(int addressCounter = 0)
    {
        Address address = new()
        {
            City = "Demo address",
            Description = "Demo address",
            PostalCode = "1234 ZZ",
            Street = "Demo street",
            HouseNumber = addressCounter.ToString()
        };
        address.Description = "Demo address";
        if (addressCounter > 0)
            address.Description += $" {addressCounter}";

        return address;
    }

    public static List<Models.Unit> GetDefaultUnit()
    {
        var units = new List<Models.Unit>();

        Unit unit = new()
        {
            Id = "kWh",
            Description = "Kilowatt hour"
        };
        units.Add(unit);

        unit = new Models.Unit
        {
            Id = "m3",
            Description = "Cubic metre"
        };
        units.Add(unit);

        unit = new Models.Unit
        {
            Id = "Day",
            Description = "Per Day"
        };
        units.Add(unit);

        return units;
    }

    public static List<Models.CalculationType> GetDefaultCalculationTypes()
    {
        var calculationTypes = new List<Models.CalculationType>();

        CalculationType calculationType = new()
        {
            Id = 1,
            Description = "Per Unit"
        };
        calculationTypes.Add(calculationType);

        calculationType = new Models.CalculationType
        {
            Id = 2,
            Description = "Percentage"
        };
        calculationTypes.Add(calculationType);

        calculationType = new Models.CalculationType
        {
            Id = 3,
            Description = "Per Day"
        };
        calculationTypes.Add(calculationType);

        return calculationTypes;
    }

    public static List<Models.EnergySubType> GetDefaultEnergySubTypes()
    {
        var energySubTypes = new List<Models.EnergySubType>();

        EnergySubType energySubType = new()
        {
            Id = 1,
            Description = "Normal"
        };
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType
        {
            Id = 2,
            Description = "Low"
        };
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType
        {
            Id = 3,
            Description = "ReturnNormal"
        };
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType
        {
            Id = 4,
            Description = "ReturnLow"
        };
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType
        {
            Id = 5,
            Description = "Other"
        };
        energySubTypes.Add(energySubType);

        return energySubTypes;
    }

    public static List<Models.TariffGroup> GetDefaultTariffGroup()
    {
        var tariffGroups = new List<Models.TariffGroup>();

        var tariffGroup = new Models.TariffGroup
        {
            Id = 1,
            Description = "General Tax",
            TypeId = 1
        };
        tariffGroups.Add(tariffGroup);

        tariffGroup = new Models.TariffGroup
        {
            Id = 2,
            Description = "Default energy",
            TypeId = 2
        };
        tariffGroups.Add(tariffGroup);

        return tariffGroups;
    }

    public static List<Models.EnergyType> GetDefaultEnergyTypes(bool hasNormalAndLow = true, bool hasEnergyReturn = false)
    {
        var energyTypeList = new List<Models.EnergyType>();

        var energyType = new Models.EnergyType
        {
            Id = 1,
            Name = "Electricity",
            UnitId = "kWh",
            HasNormalAndLow = hasNormalAndLow,
            HasEnergyReturn = hasEnergyReturn,
            DefaultType = true
        };
        energyTypeList.Add(energyType);

        energyType = new Models.EnergyType
        {
            Id = 2,
            Name = "Water",
            UnitId = "m3"
        };
        energyTypeList.Add(energyType);

        energyType = new Models.EnergyType
        {
            Id = 3,
            Name = "Gas",
            UnitId = "m3"
        };
        energyTypeList.Add(energyType);

        return energyTypeList;
    }

    #endregion
}