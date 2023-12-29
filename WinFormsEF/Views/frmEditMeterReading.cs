namespace WinFormsEF.Views
{
    public partial class frmEditMeterReading : Form
    {
        #region FormProperties

        public long ReturnValue = 0;

        private EnergyUse.Core.UnitOfWork.MeterReading _unitOfWork;
        private EnergyUse.Models.EnergyType _CurrentEnergyType;
        private long _CurrentAddressId;

        #endregion

        #region InitForm

        public frmEditMeterReading(EnergyUse.Models.EnergyType currentEnergyType, long addressId, long meterReadingId)
        {
            InitializeComponent();
            setBaseFormSettings();

            _CurrentAddressId = addressId;
            _CurrentEnergyType = currentEnergyType;

            LoadMeters();
            SetCurrentDataSource(currentEnergyType, addressId, meterReadingId);
        }

        private void LoadMeters()
        {
            bsMeters.DataSource = _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(_CurrentAddressId, _CurrentEnergyType.Id).ToList();
            cboMeters.SelectedIndex = -1;
        }

        private void SetCurrentDataSource(EnergyUse.Models.EnergyType currentEnergyType, long addressId, long meterReadingId)
        {
            if (meterReadingId == 0)
                 _unitOfWork.AddDefaultEntity(addressId, currentEnergyType.Id);
            else
                 _unitOfWork.MeterReadingRepo.Get(meterReadingId);

            bsMeterReading.DataSource = _unitOfWork.MeterReadings;
            bsMeterReading.ResetBindings(false);
            bsMeterReading.Position = 0;
        }

        #endregion

        #region Events

        private void frmEditMeterReading_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = statusStrip1.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
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

            _unitOfWork.Complete();
            closeEditMeterReading();
        }

        private void closeEditMeterReading()
        {
            _ = statusStrip1.Focus();

            if (_unitOfWork.HasChanges())
            {
                if (Managers.General.WarningUnsavedChanges(this))
                    return;
            }

            Close();
        }

        private void setBaseFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);

            _unitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(Managers.Config.GetDbFileName());            
        }

        #endregion

    }
}