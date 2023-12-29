namespace WinFormsEF.Views
{
    public partial class frmCorrectionFactor : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.CorrectionFactor _unitOfWork;
        
        #endregion

        #region InitForm

        public frmCorrectionFactor()
        {
            InitializeComponent();
            setBaseFormSettings();
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

        private void LoadCorrectionFactors(long energyTypeId)
        {
            _unitOfWork.CorrectionFactors = _unitOfWork.CorrectionFactorRepo.SelectByEnergyType(energyTypeId).ToList();
            bsCorrectionFactors.DataSource = _unitOfWork.CorrectionFactors;
        }

        #endregion

        #region Events

        private void frmCorrectionFactor_Load(object sender, EventArgs e)
        {
            LoadCorrectionFactors(getCurrentEnergyType().Id);
        }

        private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCorrectionFactors(getCurrentEnergyType().Id);
        }

        private void frmCorrectionFactor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgCorrectionFactor.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addCorrectionFactor();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setCorrectionFactor();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelCorrectionFactor();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteCorrectionFactor();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadCorrectionFactors(getCurrentEnergyType().Id);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            closeCorrectionFactorForm();
        }

        #endregion

        #region Methods

        private void addCorrectionFactor()
        {
            var entity = _unitOfWork.AddDefaultEntity(getCurrentEnergyType().Id);
           
            bsCorrectionFactors.DataSource = _unitOfWork.CorrectionFactors;
            bsCorrectionFactors.ResetBindings(false);

            bsCorrectionFactors.Position = _unitOfWork.GetPosition(entity);
        }

        private void setCorrectionFactor()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgCorrectionFactor.Focus();

            _unitOfWork.Complete();
        }

        private void cancelCorrectionFactor()
        {
            _unitOfWork.CancelChanges();
            LoadCorrectionFactors(getCurrentEnergyType().Id);
        }

        private void deleteCorrectionFactor()
        {
            var entity = (EnergyUse.Models.CorrectionFactor)bsCorrectionFactors.Current;
            _unitOfWork.Delete(entity);
            bsCorrectionFactors.DataSource = _unitOfWork.CorrectionFactors;
            bsCorrectionFactors.ResetBindings(false);
        }

        private void closeCorrectionFactorForm()
        {
            Close();
        }

        private EnergyUse.Models.EnergyType getCurrentEnergyType()
        {
            EnergyUse.Models.EnergyType energyType;

            if (cboEnergyType.SelectedIndex != -1)
                energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            else
                energyType = new EnergyUse.Models.EnergyType();

            return energyType;
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.CorrectionFactor(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgCorrectionFactor.BackgroundColor = this.BackColor;            
        }

        #endregion
    }
}