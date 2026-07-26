using EnergyUse.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settings;
        private readonly IDialogService _dialogs;
        private readonly ILanguageService _lang;

        public SettingsViewModel(ISettingsService settings, IDialogService dialogs, ILanguageService lang)
        {
            _settings = settings;
            _dialogs = dialogs;
            _lang = lang;

            CloseCommand = new RelayCommand(_ => closeWindow());
            ResetAllCommand = new RelayCommand(_ => resetAll());
            SelectExportDirCommand = new RelayCommand(_ => selectExportDirectory());
            SelectImportDirCommand = new RelayCommand(_ => selectImportDirectory());

            PickFormBackgroundColorCommand = new RelayCommand(_ => pickColor(c => FormBackgroundColor = c));
            PickSliderColorCommand = new RelayCommand(_ => pickColor(c => SliderColor = c));
            PickChartBackgroundColorCommand = new RelayCommand(_ => pickColor(c => ChartBackgroundColor = c));
            PickChartLineColorCommand = new RelayCommand(_ => pickColor(c => ChartLineColor = c));

            ResetColorsCommand = new RelayCommand(_ => resetColors());
            ResetChartCommand = new RelayCommand(_ => resetChart());
            ResetPredictionCommand = new RelayCommand(_ => resetPrediction());

            setSettings();
        }

        #region Properties

        public ObservableCollection<string> Languages { get; } =
            new ObservableCollection<string> { "English", "Dutch", "German" };

        private string _selectedLanguage = string.Empty;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
                _settings.Save("Language", value);
            }
        }

        private string _currency = string.Empty;
        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
                _settings.Save("Currency", value);
            }
        }

        private string _exportDirectory = string.Empty;
        public string ExportDirectory
        {
            get => _exportDirectory;
            set
            {
                _exportDirectory = value;
                OnPropertyChanged(nameof(ExportDirectory));
            }
        }

        private string _importDirectory = string.Empty;
        public string ImportDirectory
        {
            get => _importDirectory;
            set
            {
                _importDirectory = value;
                OnPropertyChanged(nameof(ImportDirectory));
            }
        }

        #region Colors

        private Brush? _formBackgroundColor;
        public Brush? FormBackgroundColor
        {
            get => _formBackgroundColor;
            set
            {
                _formBackgroundColor = value;
                OnPropertyChanged(nameof(FormBackgroundColor));
                setColor("BackgroundColorForms", value);
            }
        }

        private Brush? _sliderColor;
        public Brush? SliderColor
        {
            get => _sliderColor;
            set
            {
                _sliderColor = value;
                OnPropertyChanged(nameof(SliderColor));
                setColor("SliderColor", value);
            }
        }

        private Brush? _chartBackgroundColor;
        public Brush? ChartBackgroundColor
        {
            get => _chartBackgroundColor;
            set
            {
                _chartBackgroundColor = value;
                OnPropertyChanged(nameof(ChartBackgroundColor));
                setColor("BackgroundColorChart", value);
            }
        }

        private Brush? _chartLineColor;
        public Brush? ChartLineColor
        {
            get => _chartLineColor;
            set
            {
                _chartLineColor = value;
                OnPropertyChanged(nameof(ChartLineColor));
                setColor("LineColorChart", value);
            }
        }

        #endregion

        #region Prediction Settings

        private bool _useAllDataForAvg;
        public bool UseAllDataForAvg
        {
            get => _useAllDataForAvg;
            set
            {
                _useAllDataForAvg = value;
                OnPropertyChanged(nameof(UseAllDataForAvg));
                _settings.Save("UseAllDataForAvg", value.ToString());
            }
        }

        private bool _useDataFromDate;
        public bool UseDataFromDate
        {
            get => _useDataFromDate;
            set
            {
                _useDataFromDate = value;
                OnPropertyChanged(nameof(UseDataFromDate));
                _settings.Save("CalculateAvgDateFrom", value.ToString());
            }
        }

        private DateTime _avgDateFrom = DateTime.MinValue;
        public DateTime AvgDateFrom
        {
            get => _avgDateFrom;
            set
            {
                _avgDateFrom = value;
                OnPropertyChanged(nameof(AvgDateFrom));
                _settings.SaveDate("AvgDateFromDate", value);
            }
        }

        private decimal _avgCorrectionPercentage = 0;
        public decimal AvgCorrectionPercentage
        {
            get => _avgCorrectionPercentage;
            set
            {
                _avgCorrectionPercentage = value;
                OnPropertyChanged(nameof(AvgCorrectionPercentage));
                _settings.SaveDecimal("AvgCorrectionPercentage", value);
            }
        }

        private decimal _avgCorrectionPercentageReturn = 0;
        public decimal AvgCorrectionPercentageReturn
        {
            get => _avgCorrectionPercentageReturn;
            set
            {
                _avgCorrectionPercentageReturn = value;
                OnPropertyChanged(nameof(AvgCorrectionPercentageReturn));
                _settings.SaveDecimal("AvgCorrectionPercentageReturn", value);
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand CloseCommand { get; private set; }
        public ICommand ResetAllCommand { get; private set; }
        public ICommand SelectExportDirCommand { get; private set; }
        public ICommand SelectImportDirCommand { get; private set; }

        public ICommand PickFormBackgroundColorCommand { get; private set; }
        public ICommand PickSliderColorCommand { get; private set; }
        public ICommand PickChartBackgroundColorCommand { get; private set; }
        public ICommand PickChartLineColorCommand { get; private set; }

        public ICommand ResetColorsCommand { get; private set; }
        public ICommand ResetChartCommand { get; private set; }
        public ICommand ResetPredictionCommand { get; private set; }

        #endregion

        #region Methods

        private void setSettings()
        { 
            Currency = _settings.Get("Currency") ?? "";
            ExportDirectory = _settings.Get("ExportDirectory") ?? "";
            ImportDirectory = _settings.Get("ImportDirectory") ?? "";

            SelectedLanguage = _settings.Get("Language") ?? "English";

            FormBackgroundColor = getColor("BackgroundColorForms");
            SliderColor = getColor("SliderColor");
            ChartBackgroundColor = getColor("BackgroundColorChart");
            ChartLineColor = getColor("LineColorChart");

            AvgCorrectionPercentage = _settings.GetDecimal("AvgCorrectionPercentage");
            AvgCorrectionPercentageReturn = _settings.GetDecimal("AvgCorrectionPercentageReturn");

            UseAllDataForAvg = getBool("UseAllDataForAvg", true);
            UseDataFromDate = getBool("CalculateAvgDateFrom", false);

            AvgDateFrom = _settings.GetDate("AvgDateFromDate", DateTime.Now.AddYears(-2));
        }

        private bool getBool(string key, bool defaultValue)
        {
            var val = _settings.Get(key);
            return bool.TryParse(val, out var result) ? result : defaultValue;
        }

        private Brush getColor(string key)
        {
            var html = _settings.Get(key);
            if (string.IsNullOrWhiteSpace(html))
                return Brushes.Transparent;

            var c = System.Drawing.ColorTranslator.FromHtml(html);
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        private void setColor(string key, Brush? brush)
        {
            if (brush is SolidColorBrush scb)
            {
                var c = scb.Color;
                var html = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                _settings.Save(key, html);
            }
        }

        private void pickColor(Action<Brush> setter)
        {
            var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var c = dlg.Color;
                setter(new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B)));
            }
        }

        private void selectExportDirectory()
        {
            var folder = _dialogs.OpenFolder();
            if (folder != null)
            {
                ExportDirectory = folder;
                _settings.Save("ExportDirectory", folder);
            }
        }

        private void selectImportDirectory()
        {
            var folder = _dialogs.OpenFolder();
            if (folder != null)
            {
                ImportDirectory = folder;
                _settings.Save("ImportDirectory", folder);
            }
        }

        private void resetColors()
        {
            _settings.Save("BackgroundColorForms", "");
            _settings.Save("SliderColor", "");
            setSettings();
        }

        private void resetChart()
        {
            _settings.Save("BackgroundColorChart", "");
            _settings.Save("LineColorChart", "");
            setSettings();
        }

        private void resetPrediction()
        {
            _settings.Save("AvgCorrectionPercentage", "");
            _settings.Save("AvgCorrectionPercentageReturn", "");
            setSettings();
        }

        private void resetAll()
        {
            _settings.Save("ExportDirectory", "");
            _settings.Save("ImportDirectory", "");
            _settings.Save("Currency", "");

            resetColors();
            resetChart();
            resetPrediction();

            setSettings();
        }

        private void closeWindow()
        {
            System.Windows.Application.Current.Windows
                .OfType<System.Windows.Window>()
                .FirstOrDefault(w => w is WpfUI.Views.Windows.SettingsWindow)
                ?.Close();
        }

        #endregion
    }
}