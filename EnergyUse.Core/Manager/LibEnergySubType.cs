namespace EnergyUse.Core.Manager
{
    public class LibEnergySubType
    {
        public static string GetCombinedType(long energySubTypeId)
        {
            string combinedType;

            combinedType = "Unknown";

            switch (energySubTypeId)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 6:
                case 7:
                    combinedType = "Energy";
                    break;
                case 5:
                    combinedType = "Other";
                    break;
            }

            return combinedType;
        }
    }
}