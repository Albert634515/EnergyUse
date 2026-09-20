using EnergyUse.Core.Manager;
using System.Data;
using System.Globalization;

namespace WinFormsEF.ucControls;

public partial class ucImport : UserControl
{
    #region FormProperties

    private EnergyUse.Core.UnitOfWork.Import _unitOfWork;
    private EnergyUse.Models.Address CurrentAddress { get; set; }
    private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }
    private bool _isFillingMeters;

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

    private async void cmdSelectImportFile_Click(object sender, EventArgs e)
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
                Cursor.Current = Cursors.WaitCursor;
                await importCurrentFile();
                Cursor.Current = Cursors.Default;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    #endregion

    #region Events

    private async void CboMeters_SelectionChangeCommitted(object sender, EventArgs e)
    {
        if (_isFillingMeters)
            return;

        await loadSelectedMeterData();
    }

    private async Task loadSelectedMeterData()
    {
        clearImportPreview();

        if (CboMeters.SelectedItem is not EnergyUse.Models.Meter selectedMeter)
        {
            TxtImportFile.Text = string.Empty;
            return;
        }

        var libSettings = new LibSettings(Managers.Config.GetDbFileName());
        string lastUsedImportFile = libSettings.GetLastUsedImportFile(getKeyForLastImportFile());
        TxtImportFile.Text = lastUsedImportFile ?? string.Empty;

        try
        {
            Cursor.Current = Cursors.WaitCursor;
            var readings = (await _unitOfWork.MeterReadingRepo.SelectByMeterId(selectedMeter.Id)).ToList();

            if (CboMeters.SelectedItem is not EnergyUse.Models.Meter currentMeter ||
                currentMeter.Id != selectedMeter.Id)
            {
                return;
            }

            _unitOfWork.meterReadings = readings;
            await mergeCsvDataForSelectedMeter(selectedMeter);
            showReadingsForSelectedMeter();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally
        {
            Cursor.Current = Cursors.Default;
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
    }

    public async void LoadData()
    {
        if (validateImport() == false)
            return;

        Cursor.Current = Cursors.WaitCursor;
        await importCurrentFile();
        Cursor.Current = Cursors.Default;
    }

    private void saveImport()
    {
        foreach (var meterReading in _unitOfWork.meterReadings.Where(meterReading => meterReading.Id == null))
        {
            meterReading.EnergyType = null;
            meterReading.Meter = null;
            _unitOfWork.MeterReadingRepo.Add(meterReading);
        }

        if (_unitOfWork.HasChanges())
            _ = _unitOfWork.Complete();

        var message = Managers.Languages.GetResourceString("DataSaved", "Data has been saved");
        MessageBox.Show(this, message);
    }

    private async void recalculate()
    {
        Cursor.Current = Cursors.WaitCursor;

        if (_unitOfWork.meterReadings.Count > 0)
        {
            var libMeterReading = new LibMeterReading(Managers.Config.GetDbFileName());
            await libMeterReading.RecalculateReadingsDiffPreviousDay(_unitOfWork.meterReadings.Min(m => m.RegistrationDate), _unitOfWork.meterReadings.Max(m => m.RegistrationDate), CurrentEnergyType.Id, CurrentAddress.Id);
        }

        showReadingsForSelectedMeter();

        Cursor.Current = Cursors.Default;
    }
    private string getKeyForLastImportFile()
    {
        var currentMeter = (EnergyUse.Models.Meter)CboMeters.SelectedItem;
        var fileKey = string.Empty;

        if (currentMeter != null && currentMeter.Id > 0)
            fileKey = $"{currentMeter.Id}{TxtImportFile.Tag}";

        return fileKey;
    }

    private async Task importCurrentFile()
    {
        if (_unitOfWork == null || CurrentEnergyType == null)
            return;

        var newId = 0;
        var currentMeter = (EnergyUse.Models.Meter)CboMeters.SelectedItem;
        var meterlist = (await _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id))
                          .OrderByDescending(o => o.ActiveFrom.Date)
                          .ToList();
        EnergyUse.Models.MeterReading lastMeterReading = null;
        
        var libMeterReading = new LibMeterReading(Managers.Config.GetDbFileName());

        var importedMeterReadings = getImportedData(TxtImportFile.Text, CurrentEnergyType, (EnergyUse.Models.Meter)CboMeters.SelectedItem);
        if (importedMeterReadings.Count == 0)
            return;

        var meterByDate = new Dictionary<DateTime, EnergyUse.Models.Meter>();
        foreach (var importedReading in importedMeterReadings)
        {
            var registrationDate = importedReading.RegistrationDate.Date;
            var matchingMeters = meterlist
                .Where(meter => meter.ActiveFrom.Date <= registrationDate
                             && (meter.ActiveTill == null || meter.ActiveTill.Value.Date >= registrationDate))
                .ToList();

            if (matchingMeters.Count == 0)
            {
                MessageBox.Show(this, getMissingMeterMessage(meterlist, CurrentEnergyType, registrationDate));
                return;
            }

            if (matchingMeters.Count > 1)
            {
                MessageBox.Show(
                    this,
                    $"Multiple meters are active for {CurrentEnergyType.Name} on {registrationDate:yyyy-MM-dd}. " +
                    "Correct the meter periods before importing data.");
                return;
            }

            meterByDate[registrationDate] = matchingMeters[0];
        }

        var firstMeterReading = importedMeterReadings.OrderBy(o => o.RegistrationDate).FirstOrDefault();
        if (firstMeterReading != null)
            lastMeterReading = await _unitOfWork.MeterReadingRepo.SelectLastRowFromDate(firstMeterReading.RegistrationDate, CurrentEnergyType.Id, CurrentAddress.Id);

        lastMeterReading ??= firstMeterReading;

        // Load existing data
        var minNewRange = importedMeterReadings.Min(m => m.RegistrationDate);
        var maxNewRange = importedMeterReadings.Max(m => m.RegistrationDate);

        _unitOfWork.meterReadings = (await _unitOfWork.MeterReadingRepo.SelectByRange(minNewRange.AddDays(-7), maxNewRange, CurrentEnergyType.Id, CurrentAddress.Id)).ToList();

        foreach (EnergyUse.Models.MeterReading importedReading in importedMeterReadings.OrderBy(o => o.RegistrationDate))
        {
            EnergyUse.Models.Meter meter = meterByDate[importedReading.RegistrationDate.Date];
            EnergyUse.Models.MeterReading existingMeterReading = (await _unitOfWork.MeterReadingRepo.SelectByExists(importedReading.RegistrationDate.Date, CurrentEnergyType.Id, meter.Id)).FirstOrDefault();

            // Als datum gelijk aan start meter datum dan is er geen vorige reading, dus een reset
            if (importedReading.RegistrationDate.Date == meter.ActiveFrom.Date ||
                lastMeterReading?.MeterId != meter.Id)
            {
                //New meter
                if (existingMeterReading == null || existingMeterReading.Id == 0)
                    existingMeterReading = null;
                else
                {
                    //Bestaand record alleen vorige record resetten
                    lastMeterReading = null;
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

        if (CboMeters.SelectedItem is EnergyUse.Models.Meter selectedMeter &&
            selectedMeter.Id == currentMeter.Id)
        {
            showReadingsForSelectedMeter();
        }
    }

    private static string getMissingMeterMessage(
        List<EnergyUse.Models.Meter> meters,
        EnergyUse.Models.EnergyType energyType,
        DateTime registrationDate)
    {
        var previousMeter = meters
            .Where(meter => meter.ActiveFrom.Date <= registrationDate)
            .OrderByDescending(meter => meter.ActiveFrom)
            .FirstOrDefault();

        if (previousMeter?.ActiveTill is DateTime activeTill && activeTill.Date < registrationDate)
        {
            return $"Meter '{previousMeter.Description}' was closed on {activeTill:yyyy-MM-dd}. " +
                   $"No meter is available for {energyType.Name} on {registrationDate:yyyy-MM-dd}.";
        }

        return $"No meter is available for {energyType.Name} on {registrationDate:yyyy-MM-dd}.";
    }

    private void clearImportPreview()
    {
        _unitOfWork.CancelChanges();
        _unitOfWork.meterReadings = new List<EnergyUse.Models.MeterReading>();
        bsMeterReading.DataSource = _unitOfWork.meterReadings;
        bsMeterReading.ResetBindings(false);
    }

    private void showReadingsForSelectedMeter()
    {
        if (CboMeters.SelectedItem is not EnergyUse.Models.Meter selectedMeter)
        {
            bsMeterReading.DataSource = new List<EnergyUse.Models.MeterReading>();
        }
        else
        {
            bsMeterReading.DataSource = _unitOfWork.meterReadings
                .Where(reading => reading.MeterId == selectedMeter.Id)
                .OrderByDescending(reading => reading.RegistrationDate)
                .ToList();
        }

        bsMeterReading.ResetBindings(false);
    }

    private async Task mergeCsvDataForSelectedMeter(EnergyUse.Models.Meter selectedMeter)
    {
        if (string.IsNullOrWhiteSpace(TxtImportFile.Text) || !File.Exists(TxtImportFile.Text))
            return;

        var importedReadings = getImportedData(TxtImportFile.Text, CurrentEnergyType, selectedMeter)
            .Where(reading => reading.RegistrationDate.Date >= selectedMeter.ActiveFrom.Date
                           && (selectedMeter.ActiveTill == null ||
                               reading.RegistrationDate.Date <= selectedMeter.ActiveTill.Value.Date))
            .OrderBy(reading => reading.RegistrationDate)
            .ToList();

        foreach (var importedReading in importedReadings)
        {
            var existingReading = _unitOfWork.meterReadings.FirstOrDefault(reading =>
                reading.MeterId == selectedMeter.Id &&
                reading.RegistrationDate.Date == importedReading.RegistrationDate.Date);

            if (existingReading == null)
            {
                importedReading.Id = null;
                importedReading.MeterId = selectedMeter.Id;
                importedReading.EnergyTypeId = CurrentEnergyType.Id;
                importedReading.WeekNo = ISOWeek.GetWeekOfYear(importedReading.RegistrationDate);
                _unitOfWork.meterReadings.Add(importedReading);
                continue;
            }

            existingReading.RateNormal = importedReading.RateNormal;
            existingReading.RateLow = importedReading.RateLow;
            existingReading.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
            existingReading.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;
            existingReading.WeekNo = ISOWeek.GetWeekOfYear(existingReading.RegistrationDate);
        }

        if (_unitOfWork.meterReadings.Count > 0)
        {
            var libMeterReading = new LibMeterReading(Managers.Config.GetDbFileName());
            _unitOfWork.meterReadings = await libMeterReading
                .RecalculateReadingsDiffPreviousDay(_unitOfWork.meterReadings);
        }
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

        clearImportPreview();
        TxtImportFile.Text = string.Empty;

        showHideColumns();
        fillMeterCombo();
    }

    private async void fillMeterCombo()
    {
        List<EnergyUse.Models.Meter> meters = new();
        EnergyUse.Models.Meter defaultMeter = null;

        if (CurrentEnergyType != null && CurrentAddress != null)
        {
            meters = (await _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id)).ToList();
            defaultMeter = meters.Where(x => x.Active == true).FirstOrDefault();
            defaultMeter ??= meters.FirstOrDefault();
        }

        _isFillingMeters = true;
        try
        {
            bsMeter.DataSource = meters;
            CboMeters.SelectedIndex = -1;

            if (defaultMeter != null)
                CboMeters.SelectedItem = defaultMeter;
        }
        finally
        {
            _isFillingMeters = false;
        }

        await loadSelectedMeterData();
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
