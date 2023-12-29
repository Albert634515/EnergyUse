namespace WinFormsEF.Views
{
    public partial class frmPayments : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Payment _unitOfWork;

        #endregion

        #region InitForm

        public frmPayments()
        {
            InitializeComponent();
            setBaseFormSettings();

            LoadComboAddresses();
            LoadPreSelectePeriods();
        }

        private void LoadComboAddresses()
        {
            var addressList = _unitOfWork.AddressRepo.GetAll().ToList();
            bsAddresses.DataSource = addressList;
            
            var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
            if (defaultAddress != null)
                CboAddress.SelectedItem = defaultAddress;
            else
                CboAddress.SelectedIndex = -1;
        }

        private void LoadPreSelectePeriods()
        {
            var PreDefinedPeriods = _unitOfWork.PreDefinedPeriodRepo.GetAll().ToList();

            CboPreSelectedPeriods.DataSource = PreDefinedPeriods;
            CboPreSelectedPeriods.DisplayMember = "Description";
            CboPreSelectedPeriods.ValueMember = "Id";
            CboPreSelectedPeriods.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void CboPreSelectedPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            initPayments();
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addPayment();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setPayment();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelPayment();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deletePayment();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            refreshPayment();
        }

        private void tbsClose_Click(object sender, EventArgs e)
        {
            closePayment();
        }

        #endregion

        #region Methods

        private void initPayments()
        {
            var address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            if (address == null)
                return;

            var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;
            if (preDefinedPeriod == null)
                return;

            _unitOfWork.Payments = new List<EnergyUse.Models.Payment>();

            _unitOfWork.Payments = _unitOfWork.PaymentRepo.SelectByAddressAndPeriod(address.Id, preDefinedPeriod.Id).ToList();

            _unitOfWork.SetListSorted();
            bsPayments.DataSource = _unitOfWork.Payments;
            bsPayments.ResetBindings(false);
        }

        private void addPayment()
        {
            if (!validateInput())
                return;

            EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            EnergyUse.Models.PreDefinedPeriod preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;

            var entity = _unitOfWork.AddDefaultEntity("", address.Id, preDefinedPeriod.Id);

            bsPayments.DataSource = _unitOfWork.Payments;
            bsPayments.ResetBindings(false);

            bsPayments.Position = _unitOfWork.GetPosition(entity);
        }

        private void setPayment()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            DgPayments.Focus();

            //var payment = (EnergyUse.Models.Payment)bsPayments.Current;

            _unitOfWork.Complete();
        }

        private void cancelPayment()
        {
            _unitOfWork.CancelChanges();
        }

        private void deletePayment()
        {
            if (bsPayments.Current != null)
            {
                var message = Managers.Languages.GetResourceString("PaymentAskDelete", "Are you sure you want to delete this payment?");
                var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
                if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = (EnergyUse.Models.Payment)bsPayments.Current;
                    _unitOfWork.Delete(entity);

                    bsPayments.DataSource = _unitOfWork.Payments;
                    bsPayments.ResetBindings(false);
                }
            }
        }

        private void refreshPayment()
        {
            if (!validateInput())
                return;

            initPayments();
        }

        private void closePayment()
        {
            Close();
        }

        private bool validateInput()
        {
            if (CboAddress.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectAddress", "Select an address");
                MessageBox.Show(this, message);
                CboAddress.Focus();
                return false;
            }

            if (CboPreSelectedPeriods.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectPeriod", "Select a period");
                MessageBox.Show(this, message);
                CboPreSelectedPeriods.Focus();
                return false;
            }

            return true;
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Payment(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                DgPayments.BackgroundColor = this.BackColor;
        }

        #endregion
    }
}
