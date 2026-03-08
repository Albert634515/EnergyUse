using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Services;
using EnergyUse.Core.Interfaces;
using System.Windows.Forms;

namespace WpfUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settings;
        private readonly IDialogService _dialogs;
        private readonly ILanguageService _lang;

        public SettingsViewModel(
            ISettingsService settings,
            IDialogService dialogs,
            ILanguageService lang)
        {
            _settings = settings;
            _dialogs = dialogs;
            _lang = lang;

            InitializeCommands();
            LoadSettings();
        }

        #region Properties

        public ObservableCollection<string> Languages { get; } =
            new ObservableCollection<string> { "English", "Dutch", "German" };

        private string _selectedLanguage;
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

        private string _currency;
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

        private string _exportDirectory;
        public string ExportDirectory
        {
            get => _exportDirectory;
            set
            {
                _exportDirectory = value;
                OnPropertyChanged(nameof(ExportDirectory));
            }
        }

        private string _importDirectory;
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

        private Brush _formBackgroundColor;
        public Brush FormBackgroundColor
        {
            get => _formBackgroundColor;
            set
            {
                _formBackgroundColor = value;
                OnPropertyChanged(nameof(FormBackgroundColor));
                SaveColor("BackgroundColorForms", value);
            }
        }

        private Brush _sliderColor;
        public Brush SliderColor
        {
            get => _sliderColor;
            set
            {
                _sliderColor = value;
                OnPropertyChanged(nameof(SliderColor));
                SaveColor("SliderColor", value);
            }
        }

        private Brush _chartBackgroundColor;
        public Brush ChartBackgroundColor
        {
            get => _chartBackgroundColor;
            set
            {
                _chartBackgroundColor = value;
                OnPropertyChanged(nameof(ChartBackgroundColor));
                SaveColor("BackgroundColorChart", value);
            }
        }

        private Brush _chartLineColor;
        public Brush ChartLineColor
        {
            get => _chartLineColor;
            set
            {
                _chartLineColor = value;
                OnPropertyChanged(nameof(ChartLineColor));
                SaveColor("LineColorChart", value);
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

        private DateTime _avgDateFrom;
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

        private string _avgCorrectionPercentage;
        public string AvgCorrectionPercentage
        {
            get => _avgCorrectionPercentage;
            set
            {
                _avgCorrectionPercentage = value;
                OnPropertyChanged(nameof(AvgCorrectionPercentage));
                _settings.Save("AvgCorrectionPercentage", value);
            }
        }

        private string _avgCorrectionPercentageReturn;
        public string AvgCorrectionPercentageReturn
        {
            get => _avgCorrectionPercentageReturn;
            set
            {
                _avgCorrectionPercentageReturn = value;
                OnPropertyChanged(nameof(AvgCorrectionPercentageReturn));
                _settings.Save("AvgCorrectionPercentageReturn", value);
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

        private void InitializeCommands()
        {
            CloseCommand = new RelayCommand(_ => CloseWindow());
            ResetAllCommand = new RelayCommand(_ => ResetAll());
            SelectExportDirCommand = new RelayCommand(_ => SelectExportDirectory());
            SelectImportDirCommand = new RelayCommand(_ => SelectImportDirectory());

            PickFormBackgroundColorCommand = new RelayCommand(_ => PickColor(c => FormBackgroundColor = c));
            PickSliderColorCommand = new RelayCommand(_ => PickColor(c => SliderColor = c));
            PickChartBackgroundColorCommand = new RelayCommand(_ => PickColor(c => ChartBackgroundColor = c));
            PickChartLineColorCommand = new RelayCommand(_ => PickColor(c => ChartLineColor = c));

            ResetColorsCommand = new RelayCommand(_ => ResetColors());
            ResetChartCommand = new RelayCommand(_ => ResetChart());
            ResetPredictionCommand = new RelayCommand(_ => ResetPrediction());
        }

        #endregion

        #region Methods

        private void LoadSettings()
        {
            Currency = _settings.Get("Currency");
            ExportDirectory = _settings.Get("ExportDirectory");
            ImportDirectory = _settings.Get("ImportDirectory");

            SelectedLanguage = _settings.Get("Language") ?? "English";

            FormBackgroundColor = LoadColor("BackgroundColorForms");
            SliderColor = LoadColor("SliderColor");
            ChartBackgroundColor = LoadColor("BackgroundColorChart");
            ChartLineColor = LoadColor("LineColorChart");

            AvgCorrectionPercentage = _settings.Get("AvgCorrectionPercentage");
            AvgCorrectionPercentageReturn = _settings.Get("AvgCorrectionPercentageReturn");

            UseAllDataForAvg = GetBool("UseAllDataForAvg", true);
            UseDataFromDate = GetBool("CalculateAvgDateFrom", false);

            AvgDateFrom = _settings.GetDate("AvgDateFromDate", DateTime.Now.AddYears(-2));
        }

        private bool GetBool(string key, bool defaultValue)
        {
            var val = _settings.Get(key);
            return bool.TryParse(val, out var result) ? result : defaultValue;
        }

        private Brush LoadColor(string key)
        {
            var html = _settings.Get(key);
            if (string.IsNullOrWhiteSpace(html))
                return Brushes.Transparent;

            var c = System.Drawing.ColorTranslator.FromHtml(html);
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

        private void SaveColor(string key, Brush brush)
        {
            if (brush is SolidColorBrush scb)
            {
                var c = scb.Color;
                var html = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
                _settings.Save(key, html);
            }
        }

        private void PickColor(Action<Brush> setter)
        {
            var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var c = dlg.Color;
                setter(new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B)));
            }
        }

        private void SelectExportDirectory()
        {
            var folder = _dialogs.OpenFolder();
            if (folder != null)
            {
                ExportDirectory = folder;
                _settings.Save("ExportDirectory", folder);
            }
        }

        private void SelectImportDirectory()
        {
            var folder = _dialogs.OpenFolder();
            if (folder != null)
            {
                ImportDirectory = folder;
                _settings.Save("ImportDirectory", folder);
            }
        }

        private void ResetColors()
        {
            _settings.Save("BackgroundColorForms", "");
            _settings.Save("SliderColor", "");
            LoadSettings();
        }

        private void ResetChart()
        {
            _settings.Save("BackgroundColorChart", "");
            _settings.Save("LineColorChart", "");
            LoadSettings();
        }

        private void ResetPrediction()
        {
            _settings.Save("AvgCorrectionPercentage", "");
            _settings.Save("AvgCorrectionPercentageReturn", "");
            LoadSettings();
        }

        private void ResetAll()
        {
            _settings.Save("ExportDirectory", "");
            _settings.Save("ImportDirectory", "");
            _settings.Save("Currency", "");

            ResetColors();
            ResetChart();
            ResetPrediction();

            LoadSettings();
        }

        private void CloseWindow()
        {
            System.Windows.Application.Current.Windows
                .OfType<System.Windows.Window>()
                .FirstOrDefault(w => w is WpfUI.Views.Windows.SettingsWindow)
                ?.Close();
        }

        #endregion
    }
}