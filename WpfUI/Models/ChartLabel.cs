using System.Windows.Media;
using WpfUI.ViewModels;

namespace WpfUI.Models;

public class ChartLabel : ViewModelBase
{
    private bool _visible;
    public bool Visible
    {
        get => _visible;
        set => SetProperty(ref _visible, value);
    }

    private string _text;
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    private Brush _foreColor;
    public Brush ForeColor
    {
        get => _foreColor;
        set => SetProperty(ref _foreColor, value);
    }

    private Brush _backColor;
    public Brush BackColor
    {
        get => _backColor;
        set => SetProperty(ref _backColor, value);
    }
}