using System.Windows;
using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls;

public partial class DatePredefinedControl : UserControl
{
    public DatePredefinedControl()
    {
        InitializeComponent(); // DataContext NIET zetten!
    }

    public long SelectedPeriodId
    {
        get => (long)GetValue(SelectedPeriodIdProperty);
        set => SetValue(SelectedPeriodIdProperty, value);
    }

    public static readonly DependencyProperty SelectedPeriodIdProperty =
        DependencyProperty.Register(
            "SelectedPeriodId",
            typeof(long),
            typeof(DatePredefinedControl),
            new PropertyMetadata(0L, OnSelectedPeriodChanged));

    private static void OnSelectedPeriodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (DatePredefinedControl)d;
        var vm = control.DataContext as DatePredefinedViewModel;

        if (vm == null)
            return;

        vm.LoadDates((long)e.NewValue);
    }

}