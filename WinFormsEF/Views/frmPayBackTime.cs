using System.Data;
using EnergyUse.Common.Extensions;
using EnergyUse.Common.Libs;
using EnergyUse.Core.Controllers;
using EnergyUse.Models.Common;

namespace WinFormsEF.Views
{
    public partial class frmPayBackTime : Form
    {
        #region FormProperties

        private PayBackTimeController _controller;
        private List<PayBackTime> _payBackTimeList = new();

        #endregion

        #region InitForm

        public frmPayBackTime()
        {
            _controller = new PayBackTimeController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            InitializeComponent();
            setFormSettings();
            setComboAddresses();
            setComboEnergyTypes();
            setGeneralSettings();
        }

        private void setComboAddresses()
        {
            var addressList = _controller.UnitOfWork.AddressRepo.GetAll().ToList();
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

        private void tsbCalculate_Click(object sender, EventArgs e)
        {
            if (!validateInput())
                return;

            Cursor = Cursors.WaitCursor;

            EnergyUse.Models.Address address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;

            calculatePayBackTime(energyType, address);

            Cursor = Cursors.Default;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Toolbar

        #endregion

        #region Methods

        private void calculatePayBackTime(EnergyUse.Models.EnergyType energyType, EnergyUse.Models.Address address)
        {
            List<SettlementData> settlementDataList = new();
            _payBackTimeList = new List<PayBackTime>();
            DateTime lastPeriodStart = dtpPurchaseDate.Value;
            int startYear = dtpPurchaseDate.Value.Year - 1;
            decimal initialInvestment = getInitialInvestement();
            decimal lastRoi = 1 - Math.Abs(initialInvestment); //Initial value
            toolStripProgressBar1.Visible = true;
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel1.Text = "";
            var message = Managers.Languages.GetResourceString("Progress", "Progress");
            var libPeriodicDate = new EnergyUse.Core.Manager.LibPeriodicDate(Managers.Config.GetDbFileName());

            for (int i = 1; i <= nudMaxYears.Value; i++)
            {
                toolStripStatusLabel1.Text = $"{message} {i}/{nudMaxYears.Value}";
                int dProgress = (int)(i / nudMaxYears.Value * 100);
                toolStripProgressBar1.Value = dProgress;
                decimal quantityReduction = 0;

                Application.DoEvents();

                PayBackTime payBackTime = new();
                payBackTime.PeriodId = i;
                payBackTime.StartPeriod = lastPeriodStart;
                payBackTime.EndPeriod = lastPeriodStart.AddYears(1);

                settlementDataList = new List<SettlementData>();
                List<PeriodicData> periodicData = new();

                long tarifGroupId = address.TariffGroup.Id;

                if (payBackTime.StartPeriod >= DateTime.Now)
                    quantityReduction = LibGeneral.GetQuantityReduction(decimal.Parse(txtQualityReductionSolarPanels.Text), i);

                var totalCapacity = decimal.Parse(txtTotalCapacitySolarPanels.Text);
                if (quantityReduction != 0)
                    totalCapacity = totalCapacity * (quantityReduction / 100);

                ParameterPeriod parameterPeriod = new ParameterPeriod();
                parameterPeriod.EnergyType = energyType;
                parameterPeriod.AddressId = address.Id;
                parameterPeriod.StartRange = payBackTime.StartPeriod;
                parameterPeriod.EndRange = payBackTime.EndPeriod;
                parameterPeriod.ShowType = EnergyUse.Common.Enums.ShowType.Value;
                parameterPeriod.PeriodType = EnergyUse.Common.Enums.Period.SettlementDay;
                parameterPeriod.PredictMissingData = true;
                parameterPeriod.TarifGroupId = tarifGroupId;
                parameterPeriod.QuantityReduction = quantityReduction / 100;

                periodicData = libPeriodicDate.GetRange(parameterPeriod);
                payBackTime.ValueConsumed = Math.Round(periodicData.Sum(s => s.ValueYNormal + s.ValueYLow), 2);
                payBackTime.ValueProduced = Math.Round(periodicData.Sum(s => s.ValueYReturnNormal + s.ValueYReturnLow), 2);

                payBackTime.ValueProducedAndConsumed = Math.Round(totalCapacity - payBackTime.ValueProduced, 2);
                payBackTime.MonetaryValueProducedAndConsumed = Math.Round(payBackTime.ValueProducedAndConsumed * getPricePerUnitPerYear(startYear + i, address.TariffGroup.Id), 2);

                payBackTime.MonetaryValueConsumed = Math.Round(periodicData.Sum(s => (s.ValueYNormal * s.RateNormal) + (s.ValueYLow * s.RateLow)), 2);
                payBackTime.MonetaryValueProduced = Math.Round(periodicData.Sum(s => (s.ValueYReturnNormal * s.RateReturnNormal) + (s.ValueYReturnLow * s.RateReturnLow)), 2);

                // Voor alle cost category met te betalen waarden
                List<EnergyUse.Models.CostCategory> costCategories = _controller.UnitOfWork.CostCategoryRepo.SelectByEnergyTypeAndUntit(energyType.Id, "kWh").ToList();
                costCategories = costCategories.Where(x => x.EnergySubType.Id == 5 || x.EnergySubType.Id == 6 || x.EnergySubType.Id == 7).ToList();
                foreach (EnergyUse.Models.CostCategory costCategory in costCategories)
                {
                    tarifGroupId = (long)((costCategory.TariffGroup == null || costCategory.TariffGroup.Id <= 0) ? address.TariffGroup.Id : costCategory.TariffGroup.Id);
                    var libSettlementData = new EnergyUse.Core.Manager.LibSettlementData(Managers.Config.GetDbFileName());
                    settlementDataList.AddRange(libSettlementData.GetSettlementCost(energyType.Id, periodicData, costCategory, tarifGroupId));
                }

                payBackTime.OtherCostConsumed = Math.Round(settlementDataList.Sum(s => (s.ValueBaseConsumed * s.Rate)), 2);
                payBackTime.OtherCostProduced = Math.Round(settlementDataList.Sum(s => (s.ValueBaseProduced * s.Rate)), 2);

                payBackTime.ReturnOnInvestment = Math.Abs(payBackTime.MonetaryValueProduced) + Math.Abs(payBackTime.OtherCostProduced) + Math.Abs(payBackTime.MonetaryValueProducedAndConsumed);
                payBackTime.ReturnOnInvestmentTotal = lastRoi + payBackTime.ReturnOnInvestment;

                payBackTime.Return = Math.Round((payBackTime.ReturnOnInvestment / initialInvestment) * 100, 2);

                _payBackTimeList.Add(payBackTime);
                lastPeriodStart = payBackTime.EndPeriod.AddDays(1);
                lastRoi = payBackTime.ReturnOnInvestmentTotal;

                //Clear data lists
                periodicData.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            bsPayBackTimes.DataSource = _payBackTimeList;
        }

        private bool validateInput()
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

            int startYear = dtpPurchaseDate.Value.Year;
            for (int i = startYear; i <= nudMaxYears.Value; i++)
            {
                var pricePerUnit = getPricePerUnitPerYear(i, address.TariffGroup.Id);
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

        private decimal getPricePerUnitPerYear(int year, long tarifGroupId)
        {
            decimal price = 0;
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;

            if (year <= DateTime.Now.Year)
            {
                var calculatedUnitPrice = _controller.UnitOfWork.CalculatedUnitPriceRepo.GetByYear(year, energyType.Id, (long)address.TariffGroupId);
                if (calculatedUnitPrice != null)
                    price = calculatedUnitPrice.Price;
            }

            if (price == 0)
            {
                price = _controller.UnitOfWork.CalculatedUnitPriceRepo.GetByAverage(energyType.Id, (long)address.TariffGroupId);
            }

            return price;
        }

        private void setFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                dgPayBackTime.BackgroundColor = BackColor;
        }

        #endregion
    }
}