using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmPayments : Form
{
    #region FormProperties

    private PaymentsController _controller;

    #endregion

    #region InitForm

    public frmPayments()
    {
        InitializeComponent();
        initizeForm();
    }

    private void initizeForm()
    {
        _controller = new PaymentsController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        setBaseFormSettings();

        setComboAddresses();
        setPreSelectePeriods();
    }

    private async void setComboAddresses()
    {
        var addressList = (await _controller.UnitOfWork.AddressRepo.GetAll()).ToList();
        bsAddresses.DataSource = addressList;

        var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
        if (defaultAddress != null)
            CboAddress.SelectedItem = defaultAddress;
        else
            CboAddress.SelectedIndex = -1;
    }

    private void setPreSelectePeriods()
    {
        var PreDefinedPeriods = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll().ToList();

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

        _controller.UnitOfWork.Payments = new List<EnergyUse.Models.Payment>();

        _controller.UnitOfWork.Payments = _controller.UnitOfWork.PaymentRepo.SelectByAddressAndPeriod(address.Id, preDefinedPeriod.Id).ToList();

        _controller.UnitOfWork.SetListSorted();
        bsPayments.DataSource = _controller.UnitOfWork.Payments;
        bsPayments.ResetBindings(false);
    }

    private void addPayment()
    {
        if (!validateInput())
            return;

        EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
        EnergyUse.Models.PreDefinedPeriod preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;

        var entity = _controller.UnitOfWork.AddDefaultEntity("", address.Id, preDefinedPeriod.Id);

        bsPayments.DataSource = _controller.UnitOfWork.Payments;
        bsPayments.ResetBindings(false);

        bsPayments.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void setPayment()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        DgPayments.Focus();

        //var payment = (EnergyUse.Models.Payment)bsPayments.Current;

        _controller.UnitOfWork.Complete();
    }

    private void cancelPayment()
    {
        _controller.UnitOfWork.CancelChanges();
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
                _controller.UnitOfWork.Delete(entity);

                bsPayments.DataSource = _controller.UnitOfWork.Payments;
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
        _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.Payment(Managers.Config.GetDbFileName());

        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            DgPayments.BackgroundColor = this.BackColor;
    }

    #endregion
}