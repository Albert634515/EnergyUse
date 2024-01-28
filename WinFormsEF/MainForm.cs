using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Data;
using System.Windows.Forms;
using WinFormsEF.Views;

namespace WinFormsEF
{
    public partial class MainForm : Form
    {
        #region FormProperties

        private bool _initSettings;

        private MainController _controller;

        #endregion

        public MainForm()
        {
            _controller = new MainController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            try
            {
                initializeDb(this);

                Managers.Settings.SetLanguage();
                InitializeComponent();
                setBaseFormSettings();
                setComboAddresses();
                setComboEnergyTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                Environment.Exit(0);
            }
        }

        #region LoadForm

        private void MainForm_Load(object sender, EventArgs e)
        {
            _initSettings = true;

            setLastControl(splitContainer1.Panel1, "ucData");
            setLastControl(splitContainer1.Panel2, "ucChartRatesLiveChart");

            _initSettings = false;
        }

        #endregion

        #region Events

        private void MainForm_Shown(object sender, EventArgs e)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetSetting("HideInfoFormOnStart");
            if (setting == null || !(setting.KeyValue.ToUpper() == "YES"))
            {
                using frmInfo frmInfo = new();
                frmInfo.ShowDialog();
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            string splitterName;

            if (_initSettings == false && splitContainer1.Panel1.Controls.Count > 0)
            {
                int splitterDistance = splitContainer1.SplitterDistance;
                splitterName = $"{splitContainer1.Panel1.Controls[0].Name}MainSplitter";
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                libSettings.SaveSetting(splitterName, splitterDistance.ToString());
            }
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            Color color;

            if (s != null)
            {
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                color = libSettings.GetColorSetting("SliderColor", Color.LightGray);
                e.Graphics.FillRectangle(new SolidBrush(color), s.SplitterRectangle);
            }
        }

        private void cmbAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            setComboEnergyTypes();

            RefreshPanel(splitContainer1.Panel1, true, false);
            RefreshPanel(splitContainer1.Panel2, true, false);
        }

        private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanel(splitContainer1.Panel1, false, true);
            RefreshPanel(splitContainer1.Panel2, false, true);
        }

        #endregion

        #region Toolbar

        private void tsbData_Click(object sender, EventArgs e)
        {
            _initSettings = true;
            getControl("ucData", splitContainer1.Panel1);
            _initSettings = false;
        }

        private void tsbImportData_Click(object sender, EventArgs e)
        {
            _initSettings = true;
            getControl("ucImport", splitContainer1.Panel1);
            _initSettings = false;
        }

        private void tsbUsageGraph_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartDefault", splitContainer1.Panel2);
        }

        private void tsbCompareChart_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartCompare", splitContainer1.Panel2);
        }

        private void tsbRates_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartRates", splitContainer1.Panel2);
        }

        private void tsbPayBackTime_Click(object sender, EventArgs e)
        {
            using frmPayBackTime frmPayBackTime = new();
            frmPayBackTime.ShowDialog();
        }

        private void tsbPdfReport_Click(object sender, EventArgs e)
        {
            getSettlementReport();
        }

        private void rateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getRateReport();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            RefreshPanel(splitContainer1.Panel1, false, false);
            RefreshPanel(splitContainer1.Panel2, false, false);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region ToolStrip

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmSettings frmSettings = new();
            frmSettings.ShowDialog();

            _initSettings = true;
            setBaseFormSettings();
            _initSettings = false;
        }

        private void energyTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmEnergyTypes frmEnergyTypes = new();
            _ = frmEnergyTypes.ShowDialog();
        }

        private void addressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmAddresses frmAddresses = new();
            _ = frmAddresses.ShowDialog();
        }

        private void metersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmMeters frmMeters = new();
            _ = frmMeters.ShowDialog();
        }

        private void costCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmCostcategories frmCostcategories = new();
            _ = frmCostcategories.ShowDialog();
        }

        private void tarifGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmTariffGroups frmTarifGroups = new();
            _ = frmTarifGroups.ShowDialog();
        }

        private void energyRatesAndTarifsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FrmRates frmRates = new();
            _ = frmRates.ShowDialog();
        }

        private void predefinedPeriodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmPreDefinedPeriod frmPreDefinedPeriod = new();
            _ = frmPreDefinedPeriod.ShowDialog();
        }

        private void correctionFactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmCorrectionFactor frmCorrectionFactor = new();
            _ = frmCorrectionFactor.ShowDialog();
        }

        private void nettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmNetting frmNetting = new();
            _ = frmNetting.ShowDialog();
        }

        private void calculatedUnitPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmCalculatedUnitPrice frmCalculatedUnitPrice = new();
            _ = frmCalculatedUnitPrice.ShowDialog();
        }

        private void backUpAndRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmBackUpAndRestore frmBackUpAndRestore = new();
            _ = frmBackUpAndRestore.ShowDialog();
        }

        private void importMeterRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmImport frmImport = new();
            _ = frmImport.ShowDialog();
        }

        private void ratesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmExport frmExport = new();
            _ = frmExport.ShowDialog();
        }

        private void vatTarifsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmVatTariffs frmVatTarifs = new();
            _ = frmVatTarifs.ShowDialog();
        }

        private void recalculateCurrentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;

            Control myControl1 = findControl(splitContainer1.Panel1, "ucData");
            if (myControl1 != null && myControl1 is ucControls.ucData)
            {
                Cursor = Cursors.WaitCursor;
                ((ucControls.ucData)myControl1).RecalculateCurrentSelection();
                ((ucControls.ucData)myControl1).InitFormData(address, energyType);
                Cursor = Cursors.Default;
            }
        }

        private void recalculateAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;

            var message = Managers.Languages.GetResourceString("MainRecalculate", "Do you want to recalculate all data for energytype '%s'?");
            message = message.Replace("%s", energyType.Name);
            var message2 = Managers.Languages.GetResourceString("MainRecalculateAll", "Recalculate all?");

            if (MessageBox.Show(this, message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                _controller.RecalculateReadingsDiffPreviousDay(DateTime.MinValue, DateTime.MinValue, energyType.Id, address.Id);
                RefreshPanel(splitContainer1.Panel1, false, false);
                Cursor = Cursors.Default;
            }
        }

        private void setupNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FrmSetupNewFile frmSetupNewFile = new();
            _ = frmSetupNewFile.ShowDialog();
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmPayments frmPayments = new();
            _ = frmPayments.ShowDialog();
        }

        #region ToolStripGraph

        private void createDemoDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmCreateDemoData frmCreateDemoData = new();
            _ = frmCreateDemoData.ShowDialog();
        }

        private void usageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartDefault", splitContainer1.Panel2);
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartCompare", splitContainer1.Panel2);
        }

        private void ratesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            getChartControl("ucChartRates", splitContainer1.Panel2);
        }

        #endregion

        #region Reports

        private void settlementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getSettlementReport();
        }

        #endregion

        #region Info

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using frmInfo frmInfo = new();
            _ = frmInfo.ShowDialog();
        }

        #endregion

        #endregion

        #region Methods

        private void getSettlementReport()
        {
            var address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            EnergyUse.Models.Common.ParameterSelection parameterSelection;
            using (var frmSelectParameters = new FrmSelectSettelementParameters(address))
            {
                frmSelectParameters.ShowDialog();
                if (frmSelectParameters.ReturnValue <= 0)
                    return;
                else
                    parameterSelection = frmSelectParameters.GetSelectedParameters();
            }

            Cursor = Cursors.WaitCursor;
            var fileName = _controller.GetSettlementPdf(parameterSelection);
            Cursor = Cursors.Default;

            if (!string.IsNullOrWhiteSpace(fileName))
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileName) { UseShellExecute = true });
        }

        private void getRateReport()
        {
            var address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            EnergyUse.Models.Common.ParameterSelection parameterSelection;
            using (var frmSelectParameters = new FrmSelectSettelementParameters(address))
            {
                frmSelectParameters.ShowDialog();
                if (frmSelectParameters.ReturnValue <= 0)
                    return;
                else
                    parameterSelection = frmSelectParameters.GetSelectedParameters();
            }

            Cursor = Cursors.WaitCursor; ;
            var fileName = _controller.GetRatingReportPdf(address, parameterSelection);
            Cursor = Cursors.Default;

            if (!string.IsNullOrWhiteSpace(fileName))
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileName) { UseShellExecute = true });
        }

        private void setComboAddresses()
        {
            var addressList = _controller.GetAllAddresses();
            bsAddresses.DataSource = addressList;

            var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
            defaultAddress ??= addressList.FirstOrDefault();

            if (defaultAddress != null)
                CboAddress.SelectedItem = defaultAddress;
            else
                CboAddress.SelectedIndex = -1;
        }

        private void setComboEnergyTypes()
        {
            CboEnergyType.SelectedIndex = -1;
            CboEnergyType.SelectedItem = null;
            CboEnergyType.ResetText();

            var address = (Address)CboAddress.SelectedItem;
            if (address != null)
            {
                var energyTypes = _controller.getEnergyTypesByAddressId(address.Id);

                bsEnergyTypes.DataSource = energyTypes;
                bsEnergyTypes.ResetBindings(false);

                var energyType = energyTypes.Where(x => x.DefaultType == true).FirstOrDefault();
                if (energyType != null)
                    CboEnergyType.SelectedItem = energyType;
            }
        }

        private void setLastControl(SplitterPanel targetPanel, string defaulUc)
        {
            var lastUcLoaded = string.Empty;

            var setting = _controller.GetKey($"LastUcLoaded_{targetPanel.Tag}");
            if (setting != null)
                lastUcLoaded = setting.KeyValue;
            else if (string.IsNullOrWhiteSpace(lastUcLoaded))
                lastUcLoaded = defaulUc;

            getControl(lastUcLoaded, targetPanel);
        }

        private void getControl(string controlName, SplitterPanel targetPanel)
        {
            string splitterName = string.Empty;
            Address address = (Address)CboAddress.SelectedItem;
            EnergyType energyType = (EnergyType)CboEnergyType.SelectedItem;

            targetPanel.Controls.Clear();

            switch (controlName)
            {
                case "ucData":
                    ucControls.ucData ucData = new(address, energyType);
                    ucData.Dock = DockStyle.Fill;

                    targetPanel.Controls.Add(ucData);
                    splitterName = $"{splitContainer1.Panel1.Controls[0].Name}MainSplitter";

                    break;
                case "ucImport":
                    ucControls.ucImport ucImport = new(address, energyType);
                    ucImport.Dock = DockStyle.Fill;

                    targetPanel.Controls.Add(ucImport);
                    splitterName = $"{splitContainer1.Panel1.Controls[0].Name}MainSplitter";
                    break;
                case "ucChartDefaultLiveCharts":
                    var energyTypes = (List<EnergyType>)bsEnergyTypes.DataSource;
                    ucControls.ucChartDefaultLiveCharts ucChartDefaultLiveCharts = new(address, energyType, energyTypes);
                    ucChartDefaultLiveCharts.Dock = DockStyle.Fill;

                    targetPanel.Controls.Add(ucChartDefaultLiveCharts);
                    break;
                case "ucChartCompareLiveCharts":
                    ucControls.ucChartCompareLiveCharts ucChartCompareLiveCharts = new(address, energyType);
                    ucChartCompareLiveCharts.Dock = DockStyle.Fill;

                    targetPanel.Controls.Add(ucChartCompareLiveCharts);
                    break;
                case "ucChartRatesLiveCharts":
                    ucControls.ucChartRatesLiveCharts ucChartRatesLiveCharts = new(address, energyType);
                    ucChartRatesLiveCharts.Dock = DockStyle.Fill;

                    targetPanel.Controls.Add(ucChartRatesLiveCharts);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(splitterName))
                splitContainer1.SplitterDistance = _controller.GetMainSpitterDistance(splitterName);

            _controller.SaveSetting($"LastUcLoaded_{targetPanel.Tag}", controlName);
        }

        private void getChartControl(string controlName, SplitterPanel targetPanel)
        {
            EnergyUse.Common.Enums.GraphType graphType;
            var setting = _controller.GetKey("GraphType");
            if (setting == null)
                graphType = EnergyUse.Common.Enums.GraphType.LiveCharts;
            else
                graphType = (EnergyUse.Common.Enums.GraphType)Enum.Parse(typeof(EnergyUse.Common.Enums.GraphType), setting.KeyValue, true);

            getControl($"{controlName}{graphType}", targetPanel);
        }

        private void RefreshPanel(SplitterPanel panel, bool addressChanged, bool energyTypeChanged)
        {
            Control myControl1;
            var address = (Address)CboAddress.SelectedItem;
            var energyType = (EnergyType)CboEnergyType.SelectedItem;

            myControl1 = findControl(panel, "ucData");
            if (myControl1 != null && myControl1 is ucControls.ucData)
            {
                if (addressChanged)
                    ((ucControls.ucData)myControl1).InitFormData(address, energyType);
                else if (energyTypeChanged)
                    ((ucControls.ucData)myControl1).ResetCurrentEnergyType(energyType);
                else
                    ((ucControls.ucData)myControl1).RefreshMeterReadingList();
            }

            myControl1 = findControl(panel, "ucImport");
            if (myControl1 != null && myControl1 is ucControls.ucImport)
            {
                if (addressChanged)
                    ((ucControls.ucImport)myControl1).ResetSelection(address, energyType);
                else if (energyTypeChanged)
                    ((ucControls.ucImport)myControl1).LoadData(address, energyType);
                else
                    ((ucControls.ucImport)myControl1).LoadData();
            }

            myControl1 = findControl(panel, "ucChartDefaultLiveCharts");
            if (myControl1 != null && myControl1 is ucControls.ucChartDefaultLiveCharts)
            {
                if (addressChanged && address != null)
                    ((ucControls.ucChartDefaultLiveCharts)myControl1).ResetCurrentAddress(address, energyType);
                else if (energyTypeChanged && energyType != null)
                    ((ucControls.ucChartDefaultLiveCharts)myControl1).ResetCurrentEnergyType(energyType);
                else if (address != null && energyType != null)
                    ((ucControls.ucChartDefaultLiveCharts)myControl1).LoadChart();
            }

            myControl1 = findControl(panel, "ucChartCompareLiveCharts");
            if (myControl1 != null && myControl1 is ucControls.ucChartCompareLiveCharts)
            {
                if (addressChanged && address != null)
                    ((ucControls.ucChartCompareLiveCharts)myControl1).ResetCurrentAddress(address, energyType);
                else if (energyTypeChanged && energyType != null)
                    ((ucControls.ucChartCompareLiveCharts)myControl1).ResetCurrentEnergyType(energyType);
                else if (address != null && energyType != null)
                    ((ucControls.ucChartCompareLiveCharts)myControl1).LoadChart();
            }

            myControl1 = findControl(panel, "ucChartRatesLiveCharts");
            if (myControl1 != null && myControl1 is ucControls.ucChartRatesLiveCharts)
            {
                if (addressChanged && address != null)
                    ((ucControls.ucChartRatesLiveCharts)myControl1).ResetCurrentAddress(address, energyType);
                else if (energyTypeChanged && energyType != null)
                    ((ucControls.ucChartRatesLiveCharts)myControl1).ResetCurrentEnergyType(energyType);
                else if (address != null && energyType != null)
                    ((ucControls.ucChartRatesLiveCharts)myControl1).LoadChart();
            }
        }

        private Control findControl(Control parent, string name)
        {
            // Check the parent.
            if (parent.Name == name) return parent;

            // Recursively search the parent's children.
            foreach (Control ctl in parent.Controls)
            {
                Control found = findControl(ctl, name);
                if (found != null || ctl.Name == name)
                    return found;
            }

            // If we still haven't found it, it's not here.
            return null;
        }

        private void setBaseFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                splitContainer1.BackColor = BackColor;
        }

        private void initializeDb(IWin32Window owner)
        {
            var sourceDb = Managers.Config.GetDbFileName();
            string message;

            if (string.IsNullOrWhiteSpace(sourceDb))
            {
                setDbSetup();
            }
            else if (!File.Exists(sourceDb))
            {
                message = Managers.Languages.GetResourceString("MainErrorDbNotExist", "Current selected database in the config does not exist or is not accessible, the database needs to be set up before this program can be used.");
                MessageBox.Show(owner, message);
                setDbSetup();
            }

            sourceDb = Managers.Config.GetDbFileName();
            if (string.IsNullOrWhiteSpace(sourceDb) || !File.Exists(sourceDb))
            {
                throw new Exception(Managers.Languages.GetResourceString("MainErrorDbNotSetup", "Database not set up or not accessible, application will be closed."));
            }
        }

        private void setDbSetup()
        {
            using FrmSetupNewFile frmSetupNewFile = new();
            _ = frmSetupNewFile.ShowDialog();
        }

        #endregion

        private void chkLbEnergyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPanel(splitContainer1.Panel1, false, true);
            RefreshPanel(splitContainer1.Panel2, false, true);
        }
    }
}