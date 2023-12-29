using EnergyUse.Core.Manager;
using System.Data;

namespace WinFormsEF.Views
{
    public partial class FrmSelectSettelementParameters : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.SelectParameter _unitOfWork;
        public EnergyUse.Models.Address CurrentAddress { get; set; }
        public int ReturnValue { get; set; } = 0;

        private int _selectionLineCount { get; set; } = 0;
        private List<EnergyUse.Models.PreDefinedPeriod> _preDefinedPeriods { get; set; }

        #endregion

        #region InitForm

        public FrmSelectSettelementParameters(EnergyUse.Models.Address address)
        {
            InitializeComponent();
            setBaseFormSettings();

            CurrentAddress = address;

            setComboAddresses();
            setPreSelectePeriods();
            setEnergyTypeSelections(address.Id);
            refreshEnergyTypeLists(address);
        }

        private void setComboAddresses()
        {
            var addressList = _unitOfWork.AddressRepo.GetAll().ToList();
            bsAddresses.DataSource = addressList;
            CboAddress.SelectedIndex = -1;

            if (CurrentAddress == null)
            {
                var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
                if (defaultAddress != null)
                    CurrentAddress = defaultAddress;
            }

            if (CurrentAddress != null)
            {
                int position = addressList.IndexOf(addressList.Single(x => x.Description == CurrentAddress.Description));
                CboAddress.SelectedIndex = position;
            }
        }

        private void setPreSelectePeriods()
        {
            _preDefinedPeriods = _unitOfWork.PreDefinedPeriodRepo.GetAll().ToList();

            CboPreSelectedPeriods.DataSource = _preDefinedPeriods;
            CboPreSelectedPeriods.DisplayMember = "Description";
            CboPreSelectedPeriods.ValueMember = "Id";

            long lastSelectedPeriod = getSettingLastSelectedPeriod();
            if (lastSelectedPeriod > 0)
                CboPreSelectedPeriods.SelectedIndex = _preDefinedPeriods.FindIndex(q => q.Id == lastSelectedPeriod);
            else
                CboPreSelectedPeriods.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void cmbAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            var address = getSelectedAddress();
            refreshEnergyTypeLists(address);
            setEnergyTypeSelections(address.Id);
        }

        private void cboPreSelectedPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshSelectionPeriods();
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            ucControls.ucDateSelection ucDateSelectionLast;
            var lastdateSelectionName = getLastSelectedKey();

            ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
            Controls.Remove(ucDateSelectionLast);
            ucDateSelectionLast.Dispose();

            setLocationAddRemoveButton(0 - ucDateSelectionLast.Height);
            Height -= ucDateSelectionLast.Height;
            _selectionLineCount--;

            if (_selectionLineCount != 1)
            {
                ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
                ucDateSelectionLast.SetRemoveButtonVisibilty(true);
            }
        }

        #endregion

        #region ButtonEvents

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Point location;
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            ucControls.ucDateSelection ucDateSelectionLast;

            var lastdateSelectionName = getLastSelectedKey();
            ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
            location = ChkpredictMissingData.Location;
            location.X -= 72;
            location.Y = ucDateSelectionLast.Location.Y;

            if (ucDateSelectionLast != null)
            {
                addDateSelectionLine(_selectionLineCount + 1, address.Id, location);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ReturnValue = -1;
            Close();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            if (validateInput() != false)
            {
                saveSettingLastSelectedPeriod();
                ReturnValue = 2;
                Close();
            }
        }

        #endregion

        #region Toolbar

        #endregion

        #region Methods

        private void saveSettingLastSelectedPeriod()
        {
            if (CboPreSelectedPeriods.SelectedIndex != -1)
            {
                string fileKey = getKeyForLastLastSelectedPeriod();
                var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;

                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                libSettings.SaveSetting(fileKey, preDefinedPeriod.Id.ToString());
            }
        }

        private bool validateInput()
        {
            DateTime startDate, endDate;
            EnergyUse.Models.Address address;

            address = (EnergyUse.Models.Address)CboAddress.SelectedItem;

            if (address == null || address.Id == 0)
            {
                var message = Managers.Languages.GetResourceString("SelectParametersSelectAddress", "Please select an address to calculate the settlement for");
                MessageBox.Show(this, message);
                CboAddress.Focus();
                return false;
            }

            for (int i = 1; i <= _selectionLineCount; i++)
            {
                ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(i, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
                if (ucDateSelection != null)
                {
                    var energyType = (EnergyUse.Models.EnergyType)ucDateSelection.cboEnergyType.SelectedItem;
                    if (energyType != null)
                    {
                        startDate = ucDateSelection.dtpFrom.Value;
                        endDate = ucDateSelection.dtpTill.Value;

                        if (endDate < startDate)
                        {
                            var message = Managers.Languages.GetResourceString("SelectParametersEndDateLarger", "End date should be large or equal to start date");
                            MessageBox.Show(this, message);
                            ucDateSelection.dtpFrom.Focus();
                            return false;
                        }

                        if (ucDateSelection.cboTarifGroups.SelectedIndex == -1)
                        {
                            var message = Managers.Languages.GetResourceString("SelectTarifGroup", "Select an tarif group");
                            MessageBox.Show(this, message);
                            ucDateSelection.cboTarifGroups.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void clearEnergyTypeSelections()
        {
            foreach (var ds in this.Controls.OfType<ucControls.ucDateSelection>())
                this.Controls.Remove(ds);

            _selectionLineCount = 0;
        }

        private void setEnergyTypeSelections(long addressId)
        {
            clearEnergyTypeSelections();

            if (addressId == 0)
                return;

            ucControls.ucDateSelection ucDateSelection;
            int dateSelectionCount = getDateSelectionCount(addressId);
            Point location = ChkpredictMissingData.Location;
            location.X -= 72;

            for (int i = 1; i <= dateSelectionCount; i++)
            {
                ucDateSelection = addDateSelectionLine(i, addressId, location);
                refreshSelectionPeriods();
                location.Y += ucDateSelection.Height;
            }
        }

        private int getDateSelectionCount(long addressId)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            int dateSelectionCount = libSettings.GetNumberOfEnergyTypesOnReport(addressId);
            if (dateSelectionCount == 0)
            {
                var energyTypes = _unitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
                dateSelectionCount = energyTypes.Count;
                libSettings.SaveSetting($"NumberOfEnergyTypesOnReport_A{addressId}", dateSelectionCount.ToString());
            }

            return dateSelectionCount;
        }
        private void setLocationAddRemoveButton(int height)
        {
            Point location = BtnAdd.Location;
            location.Y += height;
            BtnAdd.Location = location;
        }

        private ucControls.ucDateSelection addDateSelectionLine(int lineCount, long addressId, Point location)
        {
            ucControls.ucDateSelection ucDateSelection = new ucControls.ucDateSelection();
            ucDateSelection.EnergyTypeList = _unitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
            ucDateSelection.TarifGroupsList = _unitOfWork.TarifGroupRepo.GetAll().ToList();
            ucDateSelection.SetTarifGroups();

            ucDateSelection.Name = getDateSelectionKey(lineCount, addressId);
            location.Y += ucDateSelection.Height;
            ucDateSelection.Location = location;
            ucDateSelection.SetEnergyTypes(addressId);
            ucDateSelection.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);

            if (lineCount == 1)
            {
                ucDateSelection.SetRemoveButtonVisibilty(false);
                ucDateSelection.SetDefaultEnergyType();
            }

            Controls.Add(ucDateSelection);
            location.Y += ucDateSelection.Height;
            this.Height += ucDateSelection.Height;

            _selectionLineCount++;

            setLocationAddRemoveButton(ucDateSelection.Height);
            setRemoveButtonVisibity(addressId);

            return ucDateSelection;
        }

        private string getDateSelectionKey(int lineCount, long addressId)
        {
            return $"ucDateSelection{lineCount}_A{addressId}";
        }

        private string getLastSelectedKey()
        {
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            return $"ucDateSelection{_selectionLineCount}_A{address.Id}";
        }

        private void setRemoveButtonVisibity(long addressId)
        {
            ucControls.ucDateSelection ucDateSelection = new ucControls.ucDateSelection(); for (int i = 1; i < _selectionLineCount; i++)
            {
                ucDateSelection = this.Controls.Find(getDateSelectionKey(i, addressId), true).FirstOrDefault() as ucControls.ucDateSelection;
                if (ucDateSelection != null)
                    ucDateSelection.SetRemoveButtonVisibilty(false);
            }
        }

        private void refreshEnergyTypeLists(EnergyUse.Models.Address address)
        {
            bool dataselectionFound;
            int maxSelectionCount = getDateSelectionCount(address.Id);
            int lineCount = 1;
            string dateSelectionKey;

            do
            {
                dataselectionFound = false;
                dateSelectionKey = getDateSelectionKey(lineCount,address.Id);

                ucControls.ucDateSelection ucDateSelection = this.Controls.Find(dateSelectionKey, true).FirstOrDefault() as ucControls.ucDateSelection;
                if (ucDateSelection != null)
                {
                    ucDateSelection.SetEnergyTypes(address.Id);
                    if (lineCount == 1)
                        ucDateSelection.SetDefaultEnergyType();

                    if (address.TariffGroup != null)
                        ucDateSelection.SetTarifGroup(address.TariffGroup.Id);

                    lineCount += 1;
                    dataselectionFound = true;
                }
            } while (dataselectionFound);
        }

        private void refreshSelectionPeriods()
        {
            EnergyUse.Models.PreDefinedPeriod preDefinedPeriod;
            List<EnergyUse.Models.PreDefinedPeriodDate> preDefinedPeriodDateList;
            EnergyUse.Models.Address address = getSelectedAddress();
            int lineCount = 1;

            if (CboPreSelectedPeriods.SelectedIndex == -1)
                return;

            preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;
            preDefinedPeriodDateList = _unitOfWork.PreDefinedPeriodDateRepo.GetByPeriodId(preDefinedPeriod.Id).ToList();

            foreach (var preDefinedPeriodDate in preDefinedPeriodDateList)
            {
                ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(lineCount, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
                if (ucDateSelection != null)
                {
                    ucDateSelection.dtpFrom.Value = preDefinedPeriodDate.StartDate;
                    ucDateSelection.dtpTill.Value = preDefinedPeriodDate.EndDate;
                    ucDateSelection.SetEnergyType(preDefinedPeriodDate.EnergyType.Id);

                    if (preDefinedPeriodDate.TariffGroup != null && preDefinedPeriodDate.TariffGroup.Id > 0)
                        ucDateSelection.SetTarifGroup(preDefinedPeriodDate.TariffGroup.Id);
                }

                lineCount += 1;
            }
        }

        private EnergyUse.Models.Address getSelectedAddress()
        {
            var address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            if (address == null)
                address = new EnergyUse.Models.Address();

            return address;
        }

        private long getSettingLastSelectedPeriod()
        {
            long lastSelectedPeriod = 0;

            var fileKey = getKeyForLastLastSelectedPeriod();
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(fileKey);
            if (setting != null && setting.Id > 0)
                lastSelectedPeriod = Convert.ToInt64(setting.KeyValue);

            return lastSelectedPeriod;
        }

        private string getKeyForLastLastSelectedPeriod()
        {
            var currentAddress = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            var fileKey = string.Empty;

            if (currentAddress != null && currentAddress.Id > 0)
                fileKey = $"{currentAddress.Id}{CboPreSelectedPeriods.Tag}";

            return fileKey;
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.SelectParameter(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
        }

        private Control FindControl(Control parent, string name)
        {
            // Check the parent.
            if (parent.Name == name) return parent;

            // Recursively search the parent's children.
            foreach (Control ctl in parent.Controls)
            {
                Control found = FindControl(ctl, name);
                if (found != null) return found;
            }

            // If we still haven't found it, it's not here.
            return null;
        }

        public EnergyUse.Models.Common.ParameterSelection GetSelectedParameters()
        {
            bool dataselectionFound;
            EnergyUse.Models.TariffGroup tarifGroup;

            var selectionCount = 1;

            var parameterSelection = new EnergyUse.Models.Common.ParameterSelection();

            parameterSelection.StartRange = DateTime.Now.AddMonths(-6);
            parameterSelection.EndRange = parameterSelection.StartRange.AddMonths(6);

            var address = (EnergyUse.Models.Address)CboAddress.SelectedItem;
            parameterSelection.AddressId = address.Id;

            var preSelectedPeriod = (EnergyUse.Models.PreDefinedPeriod)CboPreSelectedPeriods.SelectedItem;
            if (preSelectedPeriod != null)
                parameterSelection.PreSelectedPeriodId = preSelectedPeriod.Id;

            parameterSelection.ShowCalculatedData = ChkShowCalculatedData.Checked;
            parameterSelection.PredictMissingData = ChkpredictMissingData.Checked;

            do
            {
                dataselectionFound = false;

                ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(selectionCount, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
                if (ucDateSelection != null)
                {
                    var selectedEnergyType = new EnergyUse.Models.Common.SelectedEnergyType();
                    selectedEnergyType.EnergyType = (EnergyUse.Models.EnergyType)ucDateSelection.cboEnergyType.SelectedItem;
                    selectedEnergyType.StartRange = ucDateSelection.dtpFrom.Value;
                    selectedEnergyType.EndRange = ucDateSelection.dtpTill.Value;

                    tarifGroup = (EnergyUse.Models.TariffGroup)ucDateSelection.cboTarifGroups.SelectedItem;
                    selectedEnergyType.TarifGroup = tarifGroup.Id;

                    if (selectedEnergyType.EnergyType != null)
                        parameterSelection.SelectedEnergyTypeList.Add(selectedEnergyType);

                    selectionCount += 1;
                    dataselectionFound = true;
                }
            } while (dataselectionFound);

            return parameterSelection;
        }

        #endregion
    }
}