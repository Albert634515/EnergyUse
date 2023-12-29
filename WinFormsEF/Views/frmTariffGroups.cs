namespace WinFormsEF.Views
{
    public partial class frmTariffGroups : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.TariffGroup _unitOfWork;
        
        #endregion

        #region InitForm

        public frmTariffGroups()
        {
            InitializeComponent();
            setBaseFormSettings();
            setTarifGroups();
        }

        #endregion

        #region Events

        private void FrmTarifGroups_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = DgTarifGroups.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void TsbAdd_Click(object sender, EventArgs e)
        {
            addTarifGroup();
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {
            setTarifGroup();
        }

        private void TbsCancel_Click(object sender, EventArgs e)
        {
            cancelTarifGroup();
        }

        private void TsbDelete_Click(object sender, EventArgs e)
        {
            deleteTarifGroup();
        }

        private void TsbRefresh_Click(object sender, EventArgs e)
        {
            setTarifGroups();
        }

        private void TsbClose_Click(object sender, EventArgs e)
        {
            closeTarifGroup();
        }

        #endregion

        #region Methods

        private void setTarifGroups()
        {
            _unitOfWork.TariffGroups = _unitOfWork.TariffGroupRepo.GetAll().ToList();

            BsTarifGroups.DataSource = _unitOfWork.TariffGroups;
            BsTarifGroups.ResetBindings(false);
        }

        private void addTarifGroup()
        {
            var entity = _unitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("TarifGroupNewGroup", "New group"));

            BsTarifGroups.DataSource = _unitOfWork.TariffGroups;
            BsTarifGroups.ResetBindings(false);

            BsTarifGroups.Position = _unitOfWork.GetPosition(entity);
        }

        private void cancelTarifGroup()
        {
            _unitOfWork.CancelChanges();
            setTarifGroups();
        }

        private void setTarifGroup()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            DgTarifGroups.Focus();

            _unitOfWork.Complete();
        }

        private void deleteTarifGroup()
        {
            if (BsTarifGroups.Current != null)
            {
                var message = Managers.Languages.GetResourceString("TariffGroupsAskDelete", "Are you sure you want to delete this tarif group?");
                var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
                if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = (EnergyUse.Models.TariffGroup)BsTarifGroups.Current;
                    _unitOfWork.Delete(entity);

                    BsTarifGroups.DataSource = _unitOfWork.TariffGroups;
                    BsTarifGroups.ResetBindings(false);
                }
            }
        }

        private void closeTarifGroup()
        {
            Close();
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.TariffGroup(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                DgTarifGroups.BackgroundColor = BackColor;
        }

        #endregion
    }
}