using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views
{
    public partial class frmMeters : Form
    {
        #region FormProperties

        private MeterController _controller;

        #endregion

        #region InitForm

        public frmMeters()
        {
            _controller = new MeterController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            InitializeComponent();
            setBaseFormSettings();
            setComboEnergyTypes();
            setComboAddresses();
        }

        private void setComboEnergyTypes()
        {
            bsEnergyTypes.DataSource = _controller.UnitOfWork.EnergyTypeRepo.GetAll();
            cboEnergyType.SelectedIndex = -1;
        }

        private void setComboAddresses()
        {
            bsAddresses.DataSource = _controller.UnitOfWork.AddressRepo.GetAll();
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

            if (_controller.UnitOfWork.HasChanges())
                e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
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
            var entity = _controller.UnitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newmeter", "New meter"));

            bsMeters.DataSource = _controller.UnitOfWork.Meters;
            bsMeters.ResetBindings(false);

            bsMeters.Position = _controller.UnitOfWork.GetPosition(entity);
        }

        private void setMeter()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgMeters.Focus();

            _controller.UnitOfWork.Complete();
        }

        private void cancelMeter()
        {
            _controller.UnitOfWork.CancelChanges();
        }

        private void deleteMeter()
        {
            var entity = (EnergyUse.Models.Meter)bsMeters.Current;
            _controller.UnitOfWork.Delete(entity);
            bsMeters.DataSource = _controller.UnitOfWork.Meters;
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
            _controller.UnitOfWork.Meters = _controller.UnitOfWork.MeterRepo.GetAll().ToList();
            bsMeters.DataSource = _controller.UnitOfWork.Meters;
        }

        private void setBaseFormSettings()
        {
            _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.Meter(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgMeters.BackgroundColor = this.BackColor;
        }


        #endregion
    }
}