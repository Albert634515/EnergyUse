using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views
{
    public partial class frmEnergyTypes : Form
    {
        #region FormProperties

        private EnergyTypesController _controller;

        #endregion

        #region InitForm

        public frmEnergyTypes()
        {
            _controller = new EnergyTypesController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            InitializeComponent();
            setBaseFormSettings();
            LoadForm();
        }

        private void LoadForm()
        {
            LoadEnergyTypes();
            LoadUnitList();
        }

        private void LoadEnergyTypes()
        {
            _controller.UnitOfWork.EnergyTypes = _controller.UnitOfWork.EnergyTypeRepo.GetAll().ToList();
            bsEnergyTypes.DataSource = _controller.UnitOfWork.EnergyTypes;
        }

        private void LoadUnitList()
        {
            _controller.UnitOfWork.Units = _controller.UnitOfWork.UnitRepo.GetAll().ToList();
            bsUnits.DataSource = _controller.UnitOfWork.Units;
        }

        #endregion

        #region Events

        private void bsEnergyTypes_CurrentChanged(object sender, EventArgs e)
        {
            initColors();
        }

        private void frmEnergyTypes_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgEnergyTypes.Focus();

            if (_controller.UnitOfWork.HasChanges())
                e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
        }

        private void chkHasNormalAndLow_CheckedChanged(object sender, EventArgs e)
        {
            lblColorLow.Visible = chkHasNormalAndLow.Checked;
            txtColorLow.Visible = chkHasNormalAndLow.Checked;

            lblReturnDeliveryLow.Visible = chkHasNormalAndLow.Checked && chkHasEnergyReturn.Checked;
            txtReturnDeliveryLow.Visible = chkHasNormalAndLow.Checked && chkHasEnergyReturn.Checked;
        }

        private void chkHasEnergyReturn_CheckedChanged(object sender, EventArgs e)
        {
            lblReturnDeliveryHigh.Visible = chkHasEnergyReturn.Checked;
            txtReturnDeliveryHigh.Visible = chkHasEnergyReturn.Checked;

            lblReturnDeliveryLow.Visible = chkHasNormalAndLow.Checked && chkHasEnergyReturn.Checked;
            txtReturnDeliveryLow.Visible = chkHasNormalAndLow.Checked && chkHasEnergyReturn.Checked;
        }

        private void txtColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Managers.Settings.SaveColorSetting(txtColor, colorDialog1.Color);

            initColors();
        }

        private void txtColorHigh_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Managers.Settings.SaveColorSetting(txtColorHigh, colorDialog1.Color);

            initColors();
        }

        private void txtColorLow_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Managers.Settings.SaveColorSetting(txtColorLow, colorDialog1.Color);

            initColors();
        }

        private void txtReturnDeliveryHigh_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Managers.Settings.SaveColorSetting(txtReturnDeliveryHigh, colorDialog1.Color);

            initColors();
        }

        private void txtReturnDeliveryLow_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                Managers.Settings.SaveColorSetting(txtReturnDeliveryLow, colorDialog1.Color);

            initColors();
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addEnergyType();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setEnergyType();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelEnergyType();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteEnergyType();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadEnergyTypes();
        }

        private void TsbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void initColors()
        {
            if (bsEnergyTypes.Current != null)
            {
                EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)bsEnergyTypes.Current;

                //Set tags
                txtColor.Tag = $"Color{energyType.Id}";
                txtColorLow.Tag = $"Color{SubEnergyType.Low}{energyType.Id}";
                txtColorHigh.Tag = $"Color{SubEnergyType.Normal}{energyType.Id}";
                txtReturnDeliveryLow.Tag = $"Color{SubEnergyType.ReturnLow}{energyType.Id}";
                txtReturnDeliveryHigh.Tag = $"Color{SubEnergyType.ReturnNormal}{energyType.Id}";

                if (energyType != null && energyType.Id > 0)
                {
                    Managers.Settings.LoadColorSetting(txtColor);
                    Managers.Settings.LoadColorSetting(txtColorLow);
                    Managers.Settings.LoadColorSetting(txtColorHigh);
                    Managers.Settings.LoadColorSetting(txtReturnDeliveryLow);
                    Managers.Settings.LoadColorSetting(txtReturnDeliveryHigh);
                }

                setColorFieldsVisible();
            }
        }

        private void setColorFieldsVisible()
        {
            if (bsEnergyTypes.Current != null)
            {
                EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)bsEnergyTypes.Current;

                txtColor.Visible = !energyType.HasNormalAndLow;
                lblColor.Visible = !energyType.HasNormalAndLow;

                lblColorLow.Visible = energyType.HasNormalAndLow;
                txtColorLow.Visible = energyType.HasNormalAndLow;
                lblColorHigh.Visible = energyType.HasNormalAndLow;
                txtColorHigh.Visible = energyType.HasNormalAndLow;

                lblReturnDeliveryLow.Visible = energyType.HasEnergyReturn;
                txtReturnDeliveryLow.Visible = energyType.HasEnergyReturn;
                lblReturnDeliveryHigh.Visible = energyType.HasEnergyReturn;
                txtReturnDeliveryHigh.Visible = energyType.HasEnergyReturn;
            }
        }

        private void addEnergyType()
        {
            var entity = _controller.UnitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newenergytype", "New energy type"));

            bsEnergyTypes.DataSource = _controller.UnitOfWork.EnergyTypes;
            bsEnergyTypes.Position = _controller.UnitOfWork.GetPosition(entity);
        }

        private void setEnergyType()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgEnergyTypes.Focus();

            _controller.UnitOfWork.Complete();
        }

        private void cancelEnergyType()
        {
            _controller.UnitOfWork.CancelChanges();
            LoadEnergyTypes();
        }

        private void deleteEnergyType()
        {
            var entity = (EnergyUse.Models.EnergyType)bsEnergyTypes.Current;

            Managers.Settings.DeleteSettingTextBox(txtColor);
            Managers.Settings.DeleteSettingTextBox(txtColorLow);
            Managers.Settings.DeleteSettingTextBox(txtColorHigh);
            Managers.Settings.DeleteSettingTextBox(txtReturnDeliveryLow);
            Managers.Settings.DeleteSettingTextBox(txtReturnDeliveryHigh);

            _controller.UnitOfWork.Delete(entity);

            bsEnergyTypes.DataSource = _controller.UnitOfWork.EnergyTypes;
            bsEnergyTypes.ResetBindings(false);
        }

        private void setBaseFormSettings()
        {
            _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.EnergyType(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgEnergyTypes.BackgroundColor = this.BackColor;
        }

        #endregion
    }
}