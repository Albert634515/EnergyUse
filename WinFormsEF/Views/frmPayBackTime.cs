using System.Data;
using EnergyUse.Common.Extensions;
using EnergyUse.Core.Controllers;
using EnergyUse.Models.Common;

namespace WinFormsEF.Views;

public partial class frmPayBackTime : Form
{
    #region FormProperties

    private PayBackTimeController _controller;
    private List<PayBackTime> _payBackTimeList = new();

    #endregion

    #region InitForm

    public frmPayBackTime()
    {
        InitializeComponent();
        initializeForm();
    }

    private void initializeForm()
    {
        _controller = new PayBackTimeController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        setFormSettings();
        setComboAddresses();
        setComboEnergyTypes();
        setGeneralSettings();
    }

    private async void setComboAddresses()
    {
        var addressList = (await _controller.UnitOfWork.AddressRepo.GetAll()).ToList();
        bsAddresses.DataSource = addressList;

        var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
        if (defaultAddress != null)
            cmbAddress.SelectedItem = defaultAddress;
    }

    private void setComboEnergyTypes()
    {
        var energyTypes = new List<EnergyUse.Models.EnergyType>();
        EnergyUse.Models.Address address;

        if (cmbAddress.SelectedIndex > -1)
        {
            address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
            energyTypes = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(address.Id).ToList();
            energyTypes = energyTypes.Where(x => x.HasEnergyReturn == true).ToList();
            bsEnergyTypes.DataSource = energyTypes;

            if (energyTypes.Count == 0)
            {
                var message = Managers.Languages.GetResourceString("PayBackTimeNoReturns", "There are no energy types that return energy");
                MessageBox.Show(this, message);
            }

            var energyType = energyTypes.FirstOrDefault();
            if (energyType != null)
                cboEnergyType.SelectedItem = energyType;
        }
    }

    private void setGeneralSettings()
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting(nudMaxYears.Tag.ToString());
        if (setting != null && !string.IsNullOrWhiteSpace(setting.KeyValue))
            nudMaxYears.Value = int.Parse(setting.KeyValue);
    }

    private void setAddressSettings()
    {
        var address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;

        txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels_A";
        txtPurchaseAmount.Tag = "PurchaseAmount_A";
        txtSubsidyAmount.Tag = "SubsidyAmount_A";
        dtpPurchaseDate.Tag = "PurchaseDate_A";

        if (address != null && address.Id > 0)
        {
            txtTotalCapacitySolarPanels.Text = address.TotalCapacity.ToString();
            txtQualityReductionSolarPanels.Tag = $"{txtQualityReductionSolarPanels.Tag}{address.Id}";
            txtPurchaseAmount.Tag = $"{txtPurchaseAmount.Tag}{address.Id}";
            txtSubsidyAmount.Tag = $"{txtSubsidyAmount.Tag}{address.Id}";

            Managers.Settings.GetSettingTextBox(txtQualityReductionSolarPanels);
            if (string.IsNullOrWhiteSpace(txtQualityReductionSolarPanels.Text))
                txtQualityReductionSolarPanels.Text = "0";

            Managers.Settings.GetSettingTextBox(txtPurchaseAmount);
            if (string.IsNullOrWhiteSpace(txtPurchaseAmount.Text))
                txtPurchaseAmount.Text = "0";

            Managers.Settings.GetSettingTextBox(txtSubsidyAmount);
            if (string.IsNullOrWhiteSpace(txtSubsidyAmount.Text))
                txtSubsidyAmount.Text = "0";

            Managers.Settings.GetSettingDateBox(dtpPurchaseDate, DateTime.Now);
        }
    }

    #endregion

    #region Events

    private void frmPayBackTime_Load(object sender, EventArgs e)
    {
        setAddressSettings();
    }

    private void cmbAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        setComboEnergyTypes();
        setAddressSettings();
    }

    private void txtPurchaseAmount_TextChanged(object sender, EventArgs e)
    {
        double.TryParse(txtPurchaseAmount.Text, out double value);
        txtPurchaseAmount.Text = string.Format("{0:f2}", value);
    }

    private void txtSubsidyAmount_TextChanged(object sender, EventArgs e)
    {
        double.TryParse(txtSubsidyAmount.Text, out double value);
        txtSubsidyAmount.Text = string.Format("{0:f2}", value);
    }

    private void nudMaxYears_ValueChanged(object sender, EventArgs e)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveSetting(nudMaxYears.Tag.ToString(), nudMaxYears.Value.ToString());
    }

    private void txtQualityReductionSolarPanels_TextChanged(object sender, EventArgs e)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveSetting(txtQualityReductionSolarPanels.Tag.ToString(), txtQualityReductionSolarPanels.Text);
    }

    private void dtpPurchaseDate_ValueChanged(object sender, EventArgs e)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveDateSetting(dtpPurchaseDate.Tag.ToString(), dtpPurchaseDate.Value);
    }

    private void dgvPayBackTime_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        DataGridViewRow row = dgPayBackTime.Rows[e.RowIndex];
        decimal roi = (decimal)row.Cells["ReturnOnInvestmentTotal"].Value;

        if (roi < 0)
        {
            e.CellStyle.BackColor = Color.Red;
            e.CellStyle.ForeColor = Color.White;
        }
        else if (roi > 0)
        {
            e.CellStyle.BackColor = Color.Green;
            e.CellStyle.ForeColor = Color.White;
        }
        else
        {
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;
        }
    }

    #endregion

    #region ButtonEvents

    private async void tsbCalculate_Click(object sender, EventArgs e)
    {
        if (!await validateInputAsync())
            return;

        Cursor.Current = Cursors.WaitCursor;

        EnergyUse.Models.Address address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
        EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;

        await calculatePayBackTimeAsync(energyType, address);

        Cursor.Current = Cursors.Default;
    }

    private void tsbClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region Toolbar

    #endregion

    #region Methods

    private async Task calculatePayBackTimeAsync(EnergyUse.Models.EnergyType energyType, EnergyUse.Models.Address address)
    {
        List<SettlementData> settlementDataList = new();
        _payBackTimeList = new List<PayBackTime>();
        DateTime lastPeriodStart = dtpPurchaseDate.Value;
        int startYear = dtpPurchaseDate.Value.Year - 1;
        decimal initialInvestment = getInitialInvestement();
        decimal lastRoi = 0 - Math.Abs(initialInvestment);
        toolStripProgressBar1.Visible = true;
        toolStripStatusLabel1.Visible = true;
        toolStripStatusLabel1.Text = "";
        var message = Managers.Languages.GetResourceString("Progress", "Progress");
        

        for (int i = 1; i <= nudMaxYears.Value; i++)
        {
            toolStripStatusLabel1.Text = $"{message} {i}/{nudMaxYears.Value}";
            int dProgress = (int)(i / nudMaxYears.Value * 100);
            toolStripProgressBar1.Value = dProgress;
            Application.DoEvents();

            ParameterCalcPeriod parameterCalcPeriod
                = new ParameterCalcPeriod()
                {
                    PeriodId = i,
                    PeriodStart = lastPeriodStart,
                    Address = address,
                    EnergyType = energyType,
                    InitialInvestment = initialInvestment,
                    QualityReductionSolarPanels = decimal.Parse(txtQualityReductionSolarPanels.Text),
                    TotalCapacitySolarPanels = decimal.Parse(txtTotalCapacitySolarPanels.Text),
                    AverageReturn = decimal.Parse(txtAverageReturn.Text)
                };

            PayBackTime payBackTime = await _controller.CalculatePayBackPeriodAsync(parameterCalcPeriod);

            lastPeriodStart = payBackTime.StartPeriod;
            payBackTime.ReturnOnInvestmentTotal = lastRoi + payBackTime.ReturnOnInvestment;

            _payBackTimeList.Add(payBackTime);
            lastPeriodStart = payBackTime.EndPeriod.AddDays(1);
            lastRoi = payBackTime.ReturnOnInvestmentTotal;

            //Clear data lists
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        toolStripProgressBar1.Visible = false;
        toolStripStatusLabel1.Visible = false;
        bsPayBackTimes.DataSource = _payBackTimeList;
    }

    private async Task<bool> validateInputAsync()
    {
        if (cmbAddress.SelectedIndex == -1)
        {
            var message = Managers.Languages.GetResourceString("SelectAddress", "Select an address");
            MessageBox.Show(this, message);
            return false;
        }

        if (!txtPurchaseAmount.Text.IsNumeric())
        {
            var message = Managers.Languages.GetResourceString("PayBackTimeShouldBeNumeric", "Purchase amount should be a numeric value");
            MessageBox.Show(this, message);
            return false;
        }

        if (decimal.Parse(txtPurchaseAmount.Text) < 0)
        {
            var message = Managers.Languages.GetResourceString("PayBackTimeGreaterZero", "Purchase amount should be greater then zero");
            MessageBox.Show(this, message);
            return false;
        }

        if (!txtSubsidyAmount.Text.IsNumeric())
        {
            var message = Managers.Languages.GetResourceString("PayBackTimeSubsidyShouldBeNumeric", "Subsidy amount should be a numeric value");
            MessageBox.Show(this, message);
            return false;
        }

        EnergyUse.Models.Address address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
        EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;

        int startYear = dtpPurchaseDate.Value.Year;
        for (int i = startYear; i <= nudMaxYears.Value; i++)
        {
            var defaultTarifGroupId = address.DefaultTariffGroupId ?? 0;
            var pricePerUnit = await _controller.GetPricePerUnitPerYear(i, defaultTarifGroupId, energyType.Id);
            if (pricePerUnit < 0)
            {
                var message = Managers.Languages.GetResourceString("PayBackTimeNoPricePerUnit", "There is no price unit for year %s");
                message = message.Replace("%s", i.ToString());
                MessageBox.Show(this, message);
                return false;
            }
        }

        return true;
    }

    private decimal getInitialInvestement()
    {
        return decimal.Parse(txtPurchaseAmount.Text) - decimal.Parse(txtSubsidyAmount.Text);
    }

    private void setFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (BackColor != Color.Empty)
            dgPayBackTime.BackgroundColor = BackColor;
    }

    #endregion
}