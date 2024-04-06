using EnergyUse.Core.Manager;
using System.Data;
using System.Globalization;

namespace WinFormsEF.ucControls
{
    public partial class ucImport : UserControl
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Import _unitOfWork;
        private EnergyUse.Models.Address CurrentAddress { get; set; }
        private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }

        #endregion

        #region InitControl

        public ucImport(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            InitializeComponent();
            setBaseControlSettings();
            ResetSelection(address, energyType);
        }

        private void ucImport_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtImportFile.Text))
                LoadData();
        }

        #endregion

        #region Toolbar

        private void tsbImport_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tsbRecalculate_Click(object sender, EventArgs e)
        {
            recalculate();
        }

        private void tbsSave_Click(object sender, EventArgs e)
        {
            saveImport();
        }

        #endregion

        #region ButtonEvents

        private void cmdSelectImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                var libSettings = new LibSettings(Managers.Config.GetDbFileName());

                OpenFileDialog openFileDialog1 = new();
                openFileDialog1.InitialDirectory = libSettings.GetLastImportDirectory();

                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Title = Managers.Languages.GetResourceString("ImportBrowseFiles", "Browse import files");
                openFileDialog1.Filter = "csv files (*.csv)|*.csv|xlsx files (*.xslx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog1.CheckFileExists = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    TxtImportFile.Text = openFileDialog1.FileName;                    
                    libSettings.SetLastUsedImportFile(TxtImportFile.Text, getKeyForLastImportFile());
                    Cursor = Cursors.WaitCursor;
                    importCurrentFile();
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Events

        private void CboMeters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboMeters.SelectedIndex > -1)
            {
                var libSettings = new LibSettings(Managers.Config.GetDbFileName());
                string lastUsedImportFile = libSettings.GetLastUsedImportFile(getKeyForLastImportFile());
                if (!string.IsNullOrWhiteSpace(lastUsedImportFile))
                {
                    TxtImportFile.Text = lastUsedImportFile;

                    if (validateImport() == false)
                        return;

                    Cursor = Cursors.WaitCursor;
                    importCurrentFile();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void DgvImportRows_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DgImportRows.Rows[e.RowIndex];
            long id = 0;

            if (row.Cells["RecordId"].Value != null)
                id = (long)row.Cells["RecordId"].Value;

            if (id <= 0)
            {
                e.CellStyle.BackColor = Color.LightGreen;
                e.CellStyle.ForeColor = Color.Black;
            }
            else
            {
                e.CellStyle.BackColor = Color.Azure;
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        #endregion

        #region Methods

        public void LoadData(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
        {
            ResetSelection(selectedAddress, selectedEnergyType);

            if (selectedAddress != null && selectedEnergyType != null)
                LoadData();
        }

        public void LoadData()
        {
            if (validateImport() == false)
                return;

            Cursor = Cursors.WaitCursor;
            importCurrentFile();
            Cursor = Cursors.Default;
        }

        private void saveImport()
        {
            foreach (var meterReading in _unitOfWork.meterReadings.Where(meterReading => meterReading.Id == null))
            {
                _unitOfWork.MeterReadingRepo.Add(meterReading);
                _ = _unitOfWork.Complete();
            }

            var message = Managers.Languages.GetResourceString("DataSaved", "Data has been saved");
            MessageBox.Show(this, message);
        }

        private void recalculate()
        {
            Cursor = Cursors.WaitCursor;

            if (_unitOfWork.meterReadings.Count > 0)
            {
                var libMeterReading = new LibMeterReading(Managers.Config.GetDbFileName());
                libMeterReading.RecalculateReadingsDiffPreviousDay(_unitOfWork.meterReadings.Min(m => m.RegistrationDate), _unitOfWork.meterReadings.Max(m => m.RegistrationDate), CurrentEnergyType.Id, CurrentAddress.Id);
            }

            bsMeterReading.DataSource = _unitOfWork.meterReadings;
            bsMeterReading.ResetBindings(false);

            Cursor = Cursors.Default;
        }
        private string getKeyForLastImportFile()
        {
            var currentMeter = (EnergyUse.Models.Meter)CboMeters.SelectedItem;
            var fileKey = string.Empty;

            if (currentMeter != null && currentMeter.Id > 0)
                fileKey = $"{currentMeter.Id}{TxtImportFile.Tag}";

            return fileKey;
        }

        private void importCurrentFile()
        {
            if (_unitOfWork == null || CurrentEnergyType == null)
                return;

            var newId = 0;
            var currentMeter = (EnergyUse.Models.Meter)CboMeters.SelectedItem;
            var meterlist = _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id).OrderByDescending(o => o.ActiveFrom.Date).ToList();
            EnergyUse.Models.MeterReading lastMeterReading = null;
            
            var libMeterReading = new LibMeterReading(Managers.Config.GetDbFileName());

            var importedMeterReadings = getImportedData(TxtImportFile.Text, CurrentEnergyType, (EnergyUse.Models.Meter)CboMeters.SelectedItem);
            if (importedMeterReadings.Count == 0)
                return;

            var firstMeterReading = importedMeterReadings.OrderBy(o => o.RegistrationDate).FirstOrDefault();
            if (firstMeterReading != null)
                lastMeterReading = _unitOfWork.MeterReadingRepo.SelectLastRowFromDate(firstMeterReading.RegistrationDate, firstMeterReading.EnergyType.Id, CurrentAddress.Id);

            lastMeterReading ??= firstMeterReading;

            // Load existing data
            var minNewRange = importedMeterReadings.Min(m => m.RegistrationDate);
            var maxNewRange = importedMeterReadings.Max(m => m.RegistrationDate);

            _unitOfWork.meterReadings = _unitOfWork.MeterReadingRepo.SelectByRange(minNewRange.AddDays(-7), maxNewRange, CurrentEnergyType.Id, CurrentAddress.Id).ToList();

            foreach (EnergyUse.Models.MeterReading importedReading in importedMeterReadings.OrderBy(o => o.RegistrationDate))
            {
                EnergyUse.Models.Meter meter = meterlist.Where(w => w.ActiveFrom.Date <= importedReading.RegistrationDate.Date).OrderBy(o => o.ActiveFrom.Date).LastOrDefault();
                EnergyUse.Models.MeterReading existingMeterReading = _unitOfWork.MeterReadingRepo.SelectByExists(importedReading.RegistrationDate.Date, importedReading.EnergyType.Id, meter.Id).FirstOrDefault();
                                
                // Als datum gelijk aan start meter datum dan is er geen vorige reading, dus een reset
                if (importedReading.RegistrationDate == meter.ActiveFrom.Date)
                {
                    //New meter
                    if (existingMeterReading.Id == 0)
                        existingMeterReading = null;
                    else
                    {
                        //Bestaand record alleen vorige record resetten
                        lastMeterReading = new EnergyUse.Models.MeterReading();
                    }
                }

                if (existingMeterReading == null)
                {
                    var newReading = new EnergyUse.Models.MeterReading();
                    newId--;
                    newReading.Id = null;
                    newReading.EnergyTypeId = CurrentEnergyType.Id;
                    newReading.MeterId = meter.Id;
                    newReading.RegistrationDate = importedReading.RegistrationDate.Date;
                    newReading.WeekNo = ISOWeek.GetWeekOfYear(newReading.RegistrationDate);
                    newReading.RateNormal = importedReading.RateNormal;
                    newReading.RateLow = importedReading.RateLow;
                    newReading.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
                    newReading.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;

                    libMeterReading.CalculateDiff(ref newReading, lastMeterReading);

                    lastMeterReading = newReading;

                    _unitOfWork.meterReadings.Add(newReading);
                }
                else
                {
                    existingMeterReading = _unitOfWork.meterReadings.Where(x=> x.Id == existingMeterReading.Id).FirstOrDefault();
                    existingMeterReading.WeekNo = ISOWeek.GetWeekOfYear(existingMeterReading.RegistrationDate);
                    existingMeterReading.RateNormal = importedReading.RateNormal;
                    existingMeterReading.RateLow = importedReading.RateLow;
                    existingMeterReading.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
                    existingMeterReading.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;

                    libMeterReading.CalculateDiff(ref existingMeterReading, lastMeterReading);

                    lastMeterReading = existingMeterReading;
                }
            }

            bsMeterReading.DataSource = _unitOfWork.meterReadings.OrderByDescending(o => o.RegistrationDate);
            bsMeterReading.ResetBindings(false);
        }

        private List<EnergyUse.Models.MeterReading> getImportedData(string fileName, EnergyUse.Models.EnergyType energyType, EnergyUse.Models.Meter meter)
        {
            var libEpplus = new LibEpplus(Managers.Config.GetDbFileName());
            var importedMeterReadings = libEpplus.ImportFromCsvFile(fileName, energyType, meter);

            var meterReadings = new List<EnergyUse.Models.MeterReading>();

            //Remove duplicates from data
            importedMeterReadings = importedMeterReadings.OrderByDescending(o=> o.RegistrationDate.Date).ToList();
            foreach (var importedReading in importedMeterReadings)
            {
                if (meterReadings.Where(w => w.RegistrationDate.Date == importedReading.RegistrationDate.Date).Count() == 0)
                    meterReadings.Add(importedReading);
            }

            return meterReadings;
        }

        public void ResetSelection(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
        {
            CurrentAddress = selectedAddress;
            CurrentEnergyType = selectedEnergyType;

            _unitOfWork.meterReadings = new List<EnergyUse.Models.MeterReading>();
            bsMeterReading.DataSource = _unitOfWork.meterReadings;
            bsMeterReading.ResetBindings(false);

            showHideColumns();
            fillMeterCombo();

            var libSettings = new LibSettings(Managers.Config.GetDbFileName());
            TxtImportFile.Text = libSettings.GetLastUsedImportFile(getKeyForLastImportFile());
            importCurrentFile();
        }

        private void fillMeterCombo()
        {
            List<EnergyUse.Models.Meter> meters = new();
            EnergyUse.Models.Meter defaultMeter = null;

            if (CurrentEnergyType != null && CurrentAddress != null)
            {
                meters = _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id).ToList();
                bsMeter.DataSource = meters;
                defaultMeter = meters.Where(x => x.Active == true).FirstOrDefault();
                defaultMeter ??= meters.FirstOrDefault();
            }

            CboMeters.SelectedIndex = -1;

            if (defaultMeter != null)
                CboMeters.SelectedItem = defaultMeter;                
        }

        private void showHideColumns()
        {
            if (CurrentEnergyType != null)
            {
                if (CurrentEnergyType.HasNormalAndLow == true)
                {
                    DgImportRows.Columns["RateNormal"].HeaderText = "Rate Normal";
                    DgImportRows.Columns["DeltaNormal"].HeaderText = "Delta Normal";

                    DgImportRows.Columns["RateLow"].Visible = true;
                    DgImportRows.Columns["DeltaLow"].Visible = true;
                    DgImportRows.Columns["ReturnDeliveryNormal"].Visible = true;
                    DgImportRows.Columns["ReturnDeliveryDeltaNormal"].Visible = true;
                    DgImportRows.Columns["ReturnDeliveryLow"].Visible = true;
                    DgImportRows.Columns["ReturnDeliveryDeltaLow"].Visible = true;
                }
                else
                {
                    DgImportRows.Columns["RateNormal"].HeaderText = "Rate";
                    DgImportRows.Columns["DeltaNormal"].HeaderText = "Delta";

                    DgImportRows.Columns["RateLow"].Visible = false;
                    DgImportRows.Columns["DeltaLow"].Visible = false;
                    DgImportRows.Columns["ReturnDeliveryNormal"].Visible = false;
                    DgImportRows.Columns["ReturnDeliveryDeltaNormal"].Visible = false;
                    DgImportRows.Columns["ReturnDeliveryLow"].Visible = false;
                    DgImportRows.Columns["ReturnDeliveryDeltaLow"].Visible = false;
                }
            }
        }

        private bool validateImport()
        {
            if (string.IsNullOrWhiteSpace(TxtImportFile.Text))
            {
                var message = Managers.Languages.GetResourceString("ImportSelectFile", "Please enter a file to import");
                MessageBox.Show(this, message);
                TxtImportFile.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(TxtImportFile.Text) && !File.Exists(TxtImportFile.Text))
            {
                var message = Managers.Languages.GetResourceString("ImportSelectedFileNotExist", "Selected file does not exist or is not accessible");
                MessageBox.Show(this, message);
                TxtImportFile.Focus();
                return false;
            }

            if (CurrentEnergyType == null)
            {
                var message = Managers.Languages.GetResourceString("SelectEnergyType", "Please select an energy type");
                MessageBox.Show(this, message);
                return false;
            }

            if (CboMeters.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectMeter", "Please select a meter");
                MessageBox.Show(this, message);
                CboMeters.Focus();
                return false;
            }

            return true;
        }

        private void setBaseControlSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Import(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseUserControlSettings(this);
            if (BackColor != Color.Empty)
                DgImportRows.BackgroundColor = BackColor;
        }

        #endregion
    }
}