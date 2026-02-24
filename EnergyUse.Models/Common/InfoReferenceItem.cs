namespace EnergyUse.Models.Common
{
    public class InfoReferenceItem
    {
        public string Name { get; }
        public string Url { get; }

        public InfoReferenceItem(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
