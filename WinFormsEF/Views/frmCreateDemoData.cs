namespace WinFormsEF.Views
{
    public partial class frmCreateDemoData : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.DemoData _unitOfWork;

        #endregion

        #region InitForm

        public frmCreateDemoData()
        {
            InitializeComponent();
            setBaseFormSettings();
            LoadComboEnergyTypes();
            LoadComboSourceAddresses();
            LoadComboTargetAddresses();
        }

        private void LoadComboSourceAddresses()
        {
            var addressList = _unitOfWork.AddressRepo.GetAll().ToList();
            cboSourceAddress.DataSource = addressList;
            cboSourceAddress.DisplayMember = "Description";
            cboSourceAddress.ValueMember = "Id";

            cboSourceAddress.SelectedIndex = -1;
        }

        private void LoadComboTargetAddresses()
        {
            var addressList = _unitOfWork.AddressRepo.GetAll().ToList();
            cboTargetAddress.DataSource = addressList;
            cboTargetAddress.DisplayMember = "Description";
            cboTargetAddress.ValueMember = "Id";

            cboTargetAddress.SelectedIndex = -1;
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

        #endregion

        #region Toolbar

        #endregion

        #region Methods

        private void setBaseFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);

            _unitOfWork = new EnergyUse.Core.UnitOfWork.DemoData(Managers.Config.GetDbFileName());            
        }

        #endregion
    }
}