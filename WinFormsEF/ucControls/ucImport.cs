using EnergyUse.Core.Import;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using WinFormsEF.Views;

namespace WinFormsEF.ucControls;

public partial class ucImport : UserControl
{
    private Address CurrentAddress;
    private EnergyType CurrentEnergyType;
    private ImportPreview _preview;
    private bool _busy;
    private int _version;
    private bool _isFillingMeters;

    public ucImport(Address address, EnergyType energyType)
    {
        InitializeComponent();
        Managers.Settings.SetBaseUserControlSettings(this);
        DgImportRows.ReadOnly = true;
        DgImportRows.AllowUserToAddRows = false;
        TsbRecalculate.Text = "Reload preview";
        TxtImportFile.TextChanged += (_, _) => { _version++; _preview = null; };
        ResetSelection(address, energyType);
    }

    private void tsbImport_Click(object sender, EventArgs e) => LoadData();
    private void tsbRecalculate_Click(object sender, EventArgs e) => LoadData();
    private async void tbsSave_Click(object sender, EventArgs e)
    {
        if (_busy) return;
        if (_preview == null || _preview.IsSaved)
        {
            MessageBox.Show(this, "Reload the file to prepare a current import preview.", "Import");
            return;
        }
        using var review = new ImportPreviewForm(_preview, allowSave: true);
        if (review.ShowDialog(this) != DialogResult.OK) return;
        _busy = true;
        setBusy(true);
        try
        {
            var count = await new MeterReadingImport(Managers.Config.GetDbFileName()).SaveAsync(_preview);
            MessageBox.Show(this, $"{count} readings applied. Consumption differences have been recalculated.", "Import saved");
            await loadStoredAsync();
        }
        catch (Exception ex) { MessageBox.Show(this, $"Saving was aborted.\n{ex.Message}", "Import"); }
        finally { _busy = false; setBusy(false); }
    }

    private async void cmdSelectImportFile_Click(object sender, EventArgs e)
    {
        if (_busy) return;
        using var dialog = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv", CheckFileExists = true };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        TxtImportFile.Text = dialog.FileName;
        new LibSettings(Managers.Config.GetDbFileName()).SetLastUsedImportFile(dialog.FileName, getKey());
        await prepareAsync();
    }

    private async void CboMeters_SelectionChangeCommitted(object sender, EventArgs e)
    {
        if (_isFillingMeters || _busy) return;
        _version++;
        _preview = null;
        TxtImportFile.Text = new LibSettings(Managers.Config.GetDbFileName()).GetLastUsedImportFile(getKey()) ?? "";
        await loadStoredAsync();
    }

    private void DgvImportRows_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { }
    public void LoadData(Address address, EnergyType energyType) => ResetSelection(address, energyType);
    public async void LoadData() => await prepareAsync();

    private async Task prepareAsync()
    {
        if (_busy) return;
        _preview = null;
        if (CurrentAddress == null || CurrentEnergyType == null || CboMeters.SelectedItem is not Meter meter || !File.Exists(TxtImportFile.Text))
        {
            MessageBox.Show(this, "Select an existing CSV file, address, energy type and meter.", "Import");
            return;
        }
        var version = _version;
        _busy = true;
        setBusy(true);
        try
        {
            var source = LibEpplus.ReadCsvFile(TxtImportFile.Text, CurrentEnergyType, meter);
            var preview = await new MeterReadingImport(Managers.Config.GetDbFileName()).PrepareAsync(source, CurrentAddress.Id, CurrentEnergyType.Id);
            if (version != _version) return;
            _preview = preview;
            using var review = new ImportPreviewForm(preview, allowSave: false);
            review.ShowDialog(this);
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        finally { _busy = false; setBusy(false); }
    }

    private void setBusy(bool busy)
    {
        ToolStrip1.Enabled = !busy;
        TxtImportFile.Enabled = !busy;
        CboMeters.Enabled = !busy;
        CmdSelectImportFile.Enabled = !busy;
        UseWaitCursor = busy;
    }

    private async Task loadStoredAsync()
    {
        var version = _version;
        if (CboMeters.SelectedItem is not Meter meter) return;
        try
        {
            using var work = new EnergyUse.Core.UnitOfWork.Import(Managers.Config.GetDbFileName());
            var rows = await work.MeterReadingRepo.SelectByMeterId(meter.Id);
            if (version != _version) return;
            bsMeterReading.DataSource = rows.OrderByDescending(row => row.RegistrationDate).ToList();
            bsMeterReading.ResetBindings(false);
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Import"); }
    }

    public async void ResetSelection(Address address, EnergyType energyType)
    {
        CurrentAddress = address;
        CurrentEnergyType = energyType;
        _version++;
        _preview = null;
        TxtImportFile.Text = "";
        var version = _version;
        try
        {
            using var work = new EnergyUse.Core.UnitOfWork.Import(Managers.Config.GetDbFileName());
            var meters = (await work.MeterRepo.SelectByAddressAndEnergyType(address.Id, energyType.Id)).ToList();
            if (version != _version) return;
            _isFillingMeters = true;
            bsMeter.DataSource = meters;
            CboMeters.SelectedItem = meters.FirstOrDefault(m => m.Active) ?? meters.FirstOrDefault();
            _isFillingMeters = false;
            TxtImportFile.Text = new LibSettings(Managers.Config.GetDbFileName()).GetLastUsedImportFile(getKey()) ?? "";
            await loadStoredAsync();
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Import"); }
        finally { _isFillingMeters = false; }
    }

    private string getKey() => CboMeters.SelectedItem is Meter meter ? $"{meter.Id}LastImportFile" : "LastImportFile";
}
