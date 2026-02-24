using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
            IncreaseCommand = new RelayCommand(_ => Value++);
            DecreaseCommand = new RelayCommand(_ => Value--);
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
