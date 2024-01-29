using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views
{
    public partial class frmCreateDemoData : Form
    {
        #region FormProperties

        private DemoDataController _controller;

        #endregion

        #region InitForm

        public frmCreateDemoData()
        {
            _controller = new DemoDataController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            InitializeComponent();
            setBaseFormSettings();
            LoadComboEnergyTypes();
            LoadComboSourceAddresses();
            LoadComboTargetAddresses();
        }

        private void LoadComboSourceAddresses()
        {
            var addressList = _controller.UnitOfWork.AddressRepo.GetAll().ToList();
            cboSourceAddress.DataSource = addressList;
            cboSourceAddress.DisplayMember = "Description";
            cboSourceAddress.ValueMember = "Id";

            cboSourceAddress.SelectedIndex = -1;
        }

        private void LoadComboTargetAddresses()
        {
            var addressList = _controller.UnitOfWork.AddressRepo.GetAll().ToList();
            cboTargetAddress.DataSource = addressList;
            cboTargetAddress.DisplayMember = "Description";
            cboTargetAddress.ValueMember = "Id";

            cboTargetAddress.SelectedIndex = -1;
        }

        private void LoadComboEnergyTypes()
        {
            var energyTypes = _controller.UnitOfWork.EnergyTypeRepo.GetAll().ToList();
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
        }

        #endregion
    }
}