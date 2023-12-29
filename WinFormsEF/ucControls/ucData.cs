using System.Data;
using WinFormsEF.Views;

namespace WinFormsEF.ucControls
{
    public partial class ucData : UserControl
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.MeterReading _unitOfWork;

        private EnergyUse.Models.Address CurrentAddress { get; set; }
        private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }

        private DateTimePicker _dtp = new DateTimePicker();
        private Rectangle _rectangle;

        #endregion

        #region InitControl

        public ucData(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            InitializeComponent();
            setBaseControlSettings();

            initDatePicker();
            dtpFrom.Value = DateTime.Now.AddDays(-365);

            InitFormData(address, energyType);
        }

        private void initDatePicker()
        {
            dgMeterReadings.Controls.Add(_dtp);
            _dtp.Visible = false;
            _dtp.Format = DateTimePickerFormat.Short;
            _dtp.TextChanged += new EventHandler(dgvMeterReadings_TextChange);
        }

        #endregion

        #region Events

        private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            showHideColumns();
            RefreshMeterReadingList();
        }

        private void rbAccumulativeYes_CheckedChanged(object sender, EventArgs e)
        {
            showHideColumns();
        }

        private void rbAccumulativeNo_CheckedChanged(object sender, EventArgs e)
        {
            showHideColumns();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            RefreshMeterReadingList();
        }

        private void dtpTill_ValueChanged(object sender, EventArgs e)
        {
            RefreshMeterReadingList();
        }

        private void dgvMeterReadings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0: // Column index of needed dateTimePicker cell

                    _rectangle = dgMeterReadings.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    _dtp.Size = new Size(_rectangle.Width, _rectangle.Height);
                    _dtp.Location = new Point(_rectangle.X, _rectangle.Y);
                    _dtp.Visible = true;
                    _dtp.Value = getCurrentDateValue();
                    break;
            }
        }

        private void dgvMeterReadings_TextChange(object sender, EventArgs e)
        {
            dgMeterReadings.CurrentCell.Value = _dtp.Text.ToString();
        }

        private void dgvMeterReadings_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            _dtp.Visible = false;
        }

        private void dgvMeterReadings_Scroll(object sender, ScrollEventArgs e)
        {
            _dtp.Visible = false;
        }

        private void dgvMeterReadings_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgMeterReadings.Rows[e.RowIndex].Cells[0].Value = DateTime.Now.ToShortDateString();
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addMeterReading();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            editMeterReading();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteMeterReading();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            RefreshMeterReadingList();
        }

        #endregion

        #region Methods

        private void addMeterReading()
        {
            if (CurrentEnergyType == null)
            {
                var message = Managers.Languages.GetResourceString("SelectEnergyType", "Select an energy type");
                MessageBox.Show(this, message);
                return;
            }

            var meterList = _unitOfWork.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id).ToList();
            if (meterList.Count == 0)
            {
                var message = Managers.Languages.GetResourceString("ucDataNoMeter", "The current address does not have a meter for %e.%nFirst add a %e before registering new meter registrations");
                message = message.Replace("%e", CurrentEnergyType.Name);
                message = message.Replace("%n", Environment.NewLine);
                MessageBox.Show(this, message);
                return;
            }

            using (frmEditMeterReading frmEditMeterReadings = new frmEditMeterReading(CurrentEnergyType, CurrentAddress.Id, 0))
            {
                frmEditMeterReadings.ShowDialog();
                if (frmEditMeterReadings.ReturnValue > 0)
                    RefreshMeterReadingList();
            }
        }

        private void editMeterReading()
        {
            if (bsMeterReadings.Current != null && _unitOfWork.MeterReadings.Count > 0)
            {
                var meterReading = (EnergyUse.Models.MeterReading)bsMeterReadings.Current;
                using (frmEditMeterReading frmEditMeterReadings = new frmEditMeterReading(CurrentEnergyType, CurrentAddress.Id, (long)meterReading.Id))
                {
                    frmEditMeterReadings.ShowDialog();
                    if (frmEditMeterReadings.ReturnValue > 0)
                        RefreshMeterReadingList();
                }
            }
        }

        private void deleteMeterReading()
        {
            if (bsMeterReadings.Current != null)
            {
                var message = Managers.Languages.GetResourceString("ucDataDeleteRate", "Are you sure you want to delete this rate?");
                if (MessageBox.Show(message, Managers.Languages.GetResourceString("DeleteTitle", "Delete"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = (EnergyUse.Models.MeterReading)bsMeterReadings.Current;
                    _unitOfWork.Delete(entity);

                    bsMeterReadings.DataSource = _unitOfWork.MeterReadings;
                    bsMeterReadings.ResetBindings(false);
                }
            }
        }

        public void InitFormData(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            CurrentAddress = address;
            CurrentEnergyType = energyType;

            RefreshMeterReadingList();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            CurrentEnergyType = energyType;
            RefreshMeterReadingList();
        }

        private void showHideColumns()
        {
            if (CurrentEnergyType != null)
            {
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

                if (CurrentEnergyType.HasNormalAndLow == true)
                {
                    dgMeterReadings.Columns["RateNormal"].HeaderText = "Rate Normal";
                    dgMeterReadings.Columns["RateNormal"].Visible = rbAccumulativeYes.Checked;

                    dgMeterReadings.Columns["RateLow"].Visible = rbAccumulativeYes.Checked;

                    dgMeterReadings.Columns["DeltaNormal"].HeaderText = "Delta Normal";
                    dgMeterReadings.Columns["DeltaNormal"].Visible = rbAccumulativeNo.Checked;
                    dgMeterReadings.Columns["DeltaLow"].Visible = rbAccumulativeNo.Checked;
                }
                else
                {
                    dgMeterReadings.Columns["RateNormal"].HeaderText = "Rate";
                    dgMeterReadings.Columns["RateNormal"].Visible = rbAccumulativeYes.Checked;
                    dgMeterReadings.Columns["RateLow"].Visible = false;

                    dgMeterReadings.Columns["DeltaNormal"].HeaderText = "Delta";
                    dgMeterReadings.Columns["DeltaNormal"].Visible = rbAccumulativeNo.Checked;
                    dgMeterReadings.Columns["DeltaLow"].Visible = false;
                }

                if (CurrentEnergyType.HasEnergyReturn == true && CurrentAddress.SolarPanelsAvailable)
                {
                    dgMeterReadings.Columns["ReturnDeliveryNormal"].Visible = rbAccumulativeYes.Checked;
                    dgMeterReadings.Columns["ReturnDeliveryLow"].Visible = rbAccumulativeYes.Checked;

                    dgMeterReadings.Columns["ReturnDeliveryDeltaNormal"].Visible = rbAccumulativeNo.Checked;
                    dgMeterReadings.Columns["ReturnDeliveryDeltaLow"].Visible = rbAccumulativeNo.Checked;
                }
                else
                {
                    dgMeterReadings.Columns["ReturnDeliveryNormal"].Visible = false;
                    dgMeterReadings.Columns["ReturnDeliveryDeltaNormal"].Visible = false;
                    dgMeterReadings.Columns["ReturnDeliveryLow"].Visible = false;
                    dgMeterReadings.Columns["ReturnDeliveryDeltaLow"].Visible = false;
                }
            }
        }

        public void RefreshMeterReadingList()
        {
            _unitOfWork.MeterReadings = new List<EnergyUse.Models.MeterReading>();

            if (CurrentEnergyType != null)
                _unitOfWork.MeterReadings = _unitOfWork.MeterReadingRepo.SelectByRange(dtpFrom.Value, dtpTill.Value, CurrentEnergyType.Id, CurrentAddress.Id).ToList();

            bsMeterReadings.DataSource = _unitOfWork.MeterReadings.OrderByDescending(o => o.RegistrationDate).ToList();
            bsMeterReadings.ResetBindings(false);

            showHideColumns();
        }

        public void RecalculateCurrentSelection()
        {
            if (CurrentAddress == null)
                return;
            if (CurrentEnergyType == null)
                return;

            var message = Managers.Languages.GetResourceString("ucDataRecalculate", "Do you want to recalculate current selected range for energytype '%s'?");
            message = message.Replace("%s", CurrentEnergyType.Name);
            if (MessageBox.Show(this, message, Managers.Languages.GetResourceString("MainRecalculateAll", "Recalculate all"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnergyUse.Core.Manager.LibMeterReading libMeterReading = new EnergyUse.Core.Manager.LibMeterReading(Managers.Config.GetDbFileName());
                libMeterReading.RecalculateReadingsDiffPreviousDay(dtpFrom.Value, dtpTill.Value, CurrentEnergyType.Id, CurrentAddress.Id);
                RefreshMeterReadingList();
            }
        }

        private DateTime getCurrentDateValue()
        {
            DateTime currentDateValue = DateTime.MinValue;

            if (bsMeterReadings.Current != null)
            {
                var meterReading = (EnergyUse.Models.MeterReading)bsMeterReadings.Current;
                currentDateValue = meterReading.RegistrationDate;
            }

            return currentDateValue;
        }

        private void setBaseControlSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseUserControlSettings(this);
            if (BackColor != Color.Empty)
                dgMeterReadings.BackgroundColor = BackColor;

            Application.DoEvents();
        }

        #endregion
    }
}