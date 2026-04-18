using EnergyUse.Models;
using SkiaSharp;
using System.Windows.Media;

namespace WpfUI.Extensions;

public static class ColorExtensions
{
    public static Color ToWpfColor(this CostCategory category)
    {
        return Color.FromArgb(category.ColorA, category.ColorR, category.ColorG, category.ColorB);
    }

    public static void FromWpfColor(this CostCategory category, Color color)
    {
        category.ColorA = color.A;
        category.ColorR = color.R;
        category.ColorG = color.G;
        category.ColorB = color.B;
    }

    public static Color ToWpfColor(this EnergyType type)
    {
        return Color.FromArgb(type.ColorA, type.ColorR, type.ColorG, type.ColorB);
    }

    public static void FromWpfColor(this EnergyType type, Color color)
    {
        type.ColorA = color.A;
        type.ColorR = color.R;
        type.ColorG = color.G;
        type.ColorB = color.B;
    }

    public static SKColor ToSKColor(this System.Drawing.Color c) => new SKColor(c.R, c.G, c.B, c.A);

    public static string ToHtmlColor(this EnergyType type)
    {
        // #RRGGBB (A wordt genegeerd)
        return $"#{type.ColorR:X2}{type.ColorG:X2}{type.ColorB:X2}";
    }

    public static void FromHtmlColor(this EnergyType type, string html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return;

        if (html.StartsWith("#"))
            html = html[1..];

        if (html.Length != 6)
            throw new FormatException("Invalid HTML color format");

        type.ColorR = Convert.ToByte(html.Substring(0, 2), 16);
        type.ColorG = Convert.ToByte(html.Substring(2, 2), 16);
        type.ColorB = Convert.ToByte(html.Substring(4, 2), 16);
        type.ColorA = 255;
    }
}