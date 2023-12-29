using System.Data;

namespace WinFormsEF.Views
{
    public partial class frmImport : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Import _unitOfWork;

        #endregion

        #region InitForm

        public frmImport()
        {
            InitializeComponent();
            setBaseFormSettings();
            LoadComboAddresses();
            LoadComboEnergyTypes();
        }

        private void LoadComboAddresses()
        {
           var addressList = _unitOfWork.AddressRepo.GetAll().ToList();
            bsAddresses.DataSource = addressList;

            var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
            if (defaultAddress != null)
                cmbAddress.SelectedItem = defaultAddress;
        }

        private void LoadComboEnergyTypes()
        {
            List<EnergyUse.Models.EnergyType> energyTypes = new();
            EnergyUse.Models.Address address;

            cboEnergyType.SelectedIndex = -1;
            cboEnergyType.SelectedItem = null;
            cboEnergyType.ResetText();

            address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
            if (address != null)
            {
                energyTypes = _unitOfWork.EnergyTypeRepo.SelectByAddressId(address.Id).ToList();
                bsEnergyTypes.DataSource = energyTypes;

                var energyType = energyTypes.Where(x => x.DefaultType == true).FirstOrDefault();
                if (energyType != null)
                    cboEnergyType.SelectedItem = energyType;
            }
        }

        #endregion

        #region Events

        #endregion

        #region Toolbar

        private void tsbCloseImport_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Import(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
        }

        #endregion
    }
}
