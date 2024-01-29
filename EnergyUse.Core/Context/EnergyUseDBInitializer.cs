using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Context
{
    public static class EnergyUseDBInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var units = Manager.LibBaseData.GetDefaultUnit();
            modelBuilder.Entity<Models.Unit>().HasData(units);

            var calculationTypes = Manager.LibBaseData.GetDefaultCalculationTypes();
            modelBuilder.Entity<Models.CalculationType>().HasData(calculationTypes);

            var energySubTypes = Manager.LibBaseData.GetDefaultEnergySubTypes();
            modelBuilder.Entity<Models.EnergySubType>().HasData(energySubTypes);

            var tariffGroups = Manager.LibBaseData.GetDefaultTariffGroup();
            modelBuilder.Entity<Models.TariffGroup>().HasData(tariffGroups);

            var energyTypes = Manager.LibBaseData.GetDefaultEnergyTypes(); 
            modelBuilder.Entity<Models.EnergyType>().HasData(energyTypes);
        }
    }
}