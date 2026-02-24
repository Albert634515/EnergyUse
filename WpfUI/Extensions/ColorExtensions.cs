using EnergyUse.Models;
using SkiaSharp;
using System.Windows.Media;

namespace WpfUI.Extensions;

public static class ColorExtensions
{
    // -----------------------------
    // CostCategory
    // -----------------------------
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

    // -----------------------------
    // EnergyType  ← DIT ontbrak!
    // -----------------------------
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

    public static SKColor ToSKColor(this System.Drawing.Color c)
        => new SKColor(c.R, c.G, c.B, c.A);

}