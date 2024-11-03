namespace EnergyUse.Models;

public partial class Setting
{
    public int Id { get; set; } = 0;
    public string Key { get; set; } = string.Empty;
    public string KeyValue { get; set; } = string.Empty;
}