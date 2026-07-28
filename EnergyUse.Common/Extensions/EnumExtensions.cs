using System.ComponentModel;
using System.Reflection;

namespace EnergyUse.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum e)
    {
        var member =
            e.GetType()
                .GetTypeInfo()
                .GetMember(e.ToString())
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field);

        var attribute = member?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? e.ToString();
    }
}
