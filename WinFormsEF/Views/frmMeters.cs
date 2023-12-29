namespace WinFormsEF.Views
{
    public partial class frmMeters : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Meter _unitOfWork;

        #endregion

        #region InitForm

        public frmMeters()
        {
            InitializeComponent();
            setBaseFormSettings();
            setComboEnergyTypes();
            setComboAddresses();
        }

        private void setComboEnergyTypes()
        {
            bsEnergyTypes.DataSource = _unitOfWork.EnergyTypeRepo.GetAll();
            //bsEnergyTypes.ResetBindings(false);
            cboEnergyType.SelectedIndex = -1;
        }

        private void setComboAddresses()
        {
            bsAddresses.DataSource = _unitOfWork.AddressRepo.GetAll();
            //bsAddresses.ResetBindings(false);
            cboAddress.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void frmMeters_Load(object sender, EventArgs e)
        {
            getMeters();
        }

        private void frmMeters_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgMeters.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addMeter();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setMeter();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelMeter();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteMeter();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            getMeters();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            closeMeters();
        }

        #endregion

        #region Methods

        private void addMeter()
        {
            var entity = _unitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newmeter", "New meter"));

            bsMeters.DataSource = _unitOfWork.Meters;
            bsMeters.ResetBindings(false);

            bsMeters.Position = _unitOfWork.GetPosition(entity);
        }

        private void setMeter()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgMeters.Focus();

            _unitOfWork.Complete();
        }

        private void cancelMeter()
        {
            _unitOfWork.CancelChanges();
        }

        private void deleteMeter()
        {
            var entity = (EnergyUse.Models.Meter)bsMeters.Current;
            _unitOfWork.Delete(entity);
            bsMeters.DataSource = _unitOfWork.Meters;
            bsMeters.ResetBindings(false);
        }

        private void closeMeters()
        {
            //TODO
            //if (_meterList.Where(x => x.HasChanged() == true).Count() > 0)
            //{
            //    DialogResult dialogResult = MessageBox.Show(this, "There are unsaved change, are you sure you want to continue", "Unsaved changed", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.No)
            //        return;
            //}

            //foreach (var meter in _meterList)
            //    meter.RejectChanges();

            Close();
        }

        private void getMeters()
        {
            _unitOfWork.Meters = _unitOfWork.MeterRepo.GetAll().ToList();
            bsMeters.DataSource = _unitOfWork.Meters;
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Meter(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgMeters.BackgroundColor = this.BackColor;
        }


        #endregion
    }
}