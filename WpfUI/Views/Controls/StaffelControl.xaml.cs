using System.Windows;
using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class StaffelControl : UserControl
    {
        public static readonly DependencyProperty RateIdProperty =
            DependencyProperty.Register(nameof(RateId), typeof(long), typeof(StaffelControl),
                new PropertyMetadata(0L, OnRateIdChanged));

        public long RateId
        {
            get => (long)GetValue(RateIdProperty);
            set => SetValue(RateIdProperty, value);
        }

        private static void OnRateIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (StaffelControl)d;
            if (control.DataContext is StaffelViewModel vm)
                vm.LoadStaffels((long)e.NewValue);
        }

        public StaffelControl()
        {
            InitializeComponent();
            DataContext = new StaffelViewModel();
        }
    }
}