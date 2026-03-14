using System.Windows;
using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class DatePredefinedControl : UserControl
    {
        public DatePredefinedControl()
        {
            InitializeComponent();
        }

        public long SelectedPeriodId
        {
            get => (long)GetValue(SelectedPeriodIdProperty);
            set => SetValue(SelectedPeriodIdProperty, value);
        }

        public static readonly DependencyProperty SelectedPeriodIdProperty =
            DependencyProperty.Register(
                nameof(SelectedPeriodId),
                typeof(long),
                typeof(DatePredefinedControl),
                new PropertyMetadata(0L, OnSelectedPeriodIdChanged));

        private static void OnSelectedPeriodIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DatePredefinedControl)d;

            if (control.DataContext is DatePredefinedViewModel vm)
            {
                vm.SetDates((long)e.NewValue);
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Optioneel: markeer wijzigingen
        }
    }
}