using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmEditMeterReading : Form
{
    #region FormProperties

    public long ReturnValue = 0;

    private MeterReadingController _controller;
    private EnergyUse.Models.EnergyType _CurrentEnergyType;
    private long _CurrentAddressId;
    private long _meterAddressId;

    #endregion

    #region InitForm

    public frmEditMeterReading(EnergyUse.Models.EnergyType currentEnergyType, long addressId, long meterReadingId)
    {
        _controller = new MeterReadingController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();

        _CurrentAddressId = addressId;
        _CurrentEnergyType = currentEnergyType;
        _meterAddressId = meterReadingId;

        initializeForm();
    }

    private void initializeForm()
    {
        setMeters();
        setCurrentDataSource(_CurrentEnergyType, _CurrentAddressId, _meterAddressId);
    }

    private async void setMeters()
    {
        bsMeters.DataSource = (await _controller.UnitOfWork.MeterRepo.SelectByAddressAndEnergyType(_CurrentAddressId, _CurrentEnergyType.Id)).ToList();
        cboMeters.SelectedIndex = -1;
    }

    private async void setCurrentDataSource(EnergyUse.Models.EnergyType currentEnergyType, long addressId, long meterReadingId)
    {
        if (meterReadingId == 0)
            await _controller.UnitOfWork.AddDefaultEntity(addressId, currentEnergyType.Id);
        else
            _controller.UnitOfWork.MeterReadingRepo.Get(meterReadingId);

        bsMeterReading.DataSource = _controller.UnitOfWork.MeterReadings;
        bsMeterReading.ResetBindings(false);
        bsMeterReading.Position = 0;
    }

    #endregion

    #region Events

    private void frmEditMeterReading_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = statusStrip1.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region ButtonEvents

    private void cmdSave_Click(object sender, EventArgs e)
    {
        setEditMeterReading();
    }

    #endregion

    private void cmdClose_Click(object sender, EventArgs e)
    {
        closeEditMeterReading();
    }

    #region Methods

    private void setEditMeterReading()
    {
        // Set focus to force valdition and update of bindingsource form interfaces
        _ = statusStrip1.Focus();

        _controller.UnitOfWork.Complete();
        closeEditMeterReading();
    }

    private void closeEditMeterReading()
    {
        _ = statusStrip1.Focus();

        if (_controller.UnitOfWork.HasChanges())
        {
            if (Managers.GeneralDialogs.WarningUnsavedChanges(this))
                return;
        }

        Close();
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
    }

    #endregion
}