namespace EnergyUse.Core.Manager;

public class LibBaseData
{
    #region GetDefaultData

    public static Models.Address GetDemoAddress(int addressCounter = 0)
    {
        var address = new Models.Address();
        address.City = "Demo address";
        address.Description = "Demo address";
        address.PostalCode = "1234 ZZ";
        address.Street = "Demo street";
        address.HouseNumber = addressCounter.ToString();
        address.Description = "Demo address";
        if (addressCounter > 0)
            address.Description += $" {addressCounter}";

        return address;
    }

    public static List<Models.Unit> GetDefaultUnit()
    {
        var units = new List<Models.Unit>();

        var unit = new Models.Unit();
        unit.Id = "kWh";
        unit.Description = "Kilowatt hour";
        units.Add(unit);

        unit = new Models.Unit();
        unit.Id = "m3";
        unit.Description = "Cubic metre";
        units.Add(unit);

        unit = new Models.Unit();
        unit.Id = "Day";
        unit.Description = "Per Day";
        units.Add(unit);

        return units;
    }

    public static List<Models.CalculationType> GetDefaultCalculationTypes()
    {
        var calculationTypes = new List<Models.CalculationType>();

        var calculationType = new Models.CalculationType();
        calculationType.Id = 1;
        calculationType.Description = "Per Unit";
        calculationTypes.Add(calculationType);

        calculationType = new Models.CalculationType();
        calculationType.Id = 2;
        calculationType.Description = "Percentage";
        calculationTypes.Add(calculationType);

        calculationType = new Models.CalculationType();
        calculationType.Id = 3;
        calculationType.Description = "Per Day";
        calculationTypes.Add(calculationType);

        return calculationTypes;
    }

    public static List<Models.EnergySubType> GetDefaultEnergySubTypes()
    {
        var energySubTypes = new List<Models.EnergySubType>();

        var energySubType = new Models.EnergySubType();
        energySubType.Id = 1;
        energySubType.Description = "Normal";
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType();
        energySubType.Id = 2;
        energySubType.Description = "Low";
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType();
        energySubType.Id = 3;
        energySubType.Description = "ReturnNormal";
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType();
        energySubType.Id = 4;
        energySubType.Description = "ReturnLow";
        energySubTypes.Add(energySubType);

        energySubType = new Models.EnergySubType();
        energySubType.Id = 5;
        energySubType.Description = "Other";
        energySubTypes.Add(energySubType);

        return energySubTypes;
    }

    public static List<Models.TariffGroup> GetDefaultTariffGroup()
    {
        var tariffGroups = new List<Models.TariffGroup>();

        var tariffGroup = new Models.TariffGroup();
        tariffGroup.Id = 1;
        tariffGroup.Description = "Default";
        tariffGroups.Add(tariffGroup);

        tariffGroup = new Models.TariffGroup();
        tariffGroup.Id = 2;
        tariffGroup.Description = "General";
        tariffGroups.Add(tariffGroup);

        return tariffGroups;
    }

    public static List<Models.EnergyType> GetDefaultEnergyTypes(bool hasNormalAndLow = true, bool hasEnergyReturn = false)
    {
        var energyTypeList = new List<Models.EnergyType>();

        var energyType = new Models.EnergyType();
        energyType.Id = 1;
        energyType.Name = "Electricity";
        energyType.UnitId = "kWh";
        energyType.HasNormalAndLow = hasNormalAndLow;
        energyType.HasEnergyReturn = hasEnergyReturn;
        energyType.DefaultType = true;
        energyTypeList.Add(energyType);

        energyType = new Models.EnergyType();
        energyType.Id = 2;
        energyType.Name = "Water";
        energyType.UnitId = "m3";
        energyTypeList.Add(energyType);

        energyType = new Models.EnergyType();
        energyType.Id = 3;
        energyType.Name = "Gas";
        energyType.UnitId = "m3";
        energyTypeList.Add(energyType);

        return energyTypeList;
    }

    #endregion
}