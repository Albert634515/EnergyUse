namespace EnergyUse.Models.Common;

public class SelectionItem
{
    public SelectionItem()
    {
    }

    public SelectionItem(int id, string key, string description)
    {
        Id = id;
        Key = key;
        Description = description;
    }

    public int Id { get; set; } = 0;
    public string Key { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}