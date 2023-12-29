namespace WinFormsEF.Views
{
    public partial class frmNetting : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Netting _unitOfWork;
        
        #endregion

        #region InitForm

        public frmNetting()
        {
            InitializeComponent();
            SetBaseFormSettings();
            LoadComboEnergyTypes();
        }

        private void LoadComboEnergyTypes()
        {
            var energyTypes = _unitOfWork.EnergyTypeRepo.GetAll().ToList();
            cboEnergyType.DataSource = energyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.ValueMember = "Id";

            cboEnergyType.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void frmNetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgNetting.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void tsbAddNetting_Click(object sender, EventArgs e)
        {
            AddNetting();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveNetting();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            CancelNetting();
        }

        private void tsbDeleteNetting_Click(object sender, EventArgs e)
        {
            DeleteNetting();
        }

        private void tsbRefreshNetting_Click(object sender, EventArgs e)
        {
            RefreshNetting();
        }

        private void tsbCloseNetting_Click(object sender, EventArgs e)
        {
            CloseNetting();
        }

        #endregion

        #region Methods

        private void AddNetting()
        {
            var entity = _unitOfWork.AddDefaultEntity();

            bsNetting.DataSource = _unitOfWork.Nettings;
            bsNetting.Position = _unitOfWork.GetPosition(entity);
        }

        private void SaveNetting()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgNetting.Focus();

            _unitOfWork.Complete();
        }

        private void CancelNetting()
        {
            _unitOfWork.CancelChanges();
            RefreshNetting();
        }

        private void DeleteNetting()
        {
            EnergyUse.Models.Netting entity = (EnergyUse.Models.Netting)bsNetting.Current;
            _unitOfWork.Delete(entity);
            bsNetting.DataSource = _unitOfWork.Nettings;
            bsNetting.ResetBindings(false);
        }

        private void RefreshNetting()
        {
            if (!validateInput())
                return;

            var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            InitNetting(energyType.Id);
        }

        private void CloseNetting()
        {
            Close();
        }

        private bool validateInput()
        {
            if (cboEnergyType.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectEnergyType", "Select an energy type");
                MessageBox.Show(this, message);
                cboEnergyType.Focus();
                return false;
            }

            return true;
        }

        private void InitNetting(long energyTypeId)
        {
            if (energyTypeId == 0)
                _unitOfWork.Nettings = new List<EnergyUse.Models.Netting>();
            else
                _unitOfWork.Nettings = _unitOfWork.NettingRepo.SelectByEnergyType(energyTypeId).ToList();

            _unitOfWork.SetListSorted();

            bsNetting.DataSource = _unitOfWork.Nettings;
            bsNetting.ResetBindings(false);
        }

        private void SetBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Netting(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgNetting.BackgroundColor = this.BackColor;
        }

        #endregion
    }
}