namespace WinFormsEF.ucControls
{
    public partial class ucStaffel : UserControl
    {
        #region UcProperties

        private long _rateId = 0;
        private EnergyUse.Core.UnitOfWork.Staffel _unitOfWork;

        #endregion

        #region InitControl

        public ucStaffel()
        {
            InitializeComponent();
            setBaseUcControlSettings();
        }

        public void InitStaffels(long rateId)
        {
            _rateId = rateId;
            _unitOfWork.Staffels = new List<EnergyUse.Models.Staffel>();

            if (_rateId > 0)
                _unitOfWork.Staffels = _unitOfWork.StaffelRepo.SelectByRateId(_rateId).ToList();

            _unitOfWork.SetListSorted();
            BsStaffel.DataSource = _unitOfWork.Staffels;
            BsStaffel.ResetBindings(false);
        }

        #endregion

        #region Events

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addStaffel();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setStaffel();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelStaffel();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteStaffel();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            refreshStaffel();
        }

        #endregion

        #region Methods

        private bool validateInput()
        {

            return true;
        }

        private void addStaffel()
        {
            if (!validateInput())
                return;

            EnergyUse.Models.Staffel entity = _unitOfWork.AddDefaultEntity(_rateId);

            BsStaffel.DataSource = _unitOfWork.Staffels;
            BsStaffel.ResetBindings(false);

            BsStaffel.Position = _unitOfWork.GetPosition(entity);
        }

        private void setStaffel()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            DgStaffel.Focus();

            var rate = (EnergyUse.Models.Staffel)BsStaffel.Current;

            _unitOfWork.Complete();
        }

        private void cancelStaffel()
        {
            _unitOfWork.CancelChanges();
        }

        private void deleteStaffel()
        {
            if (BsStaffel.Current != null)
            {
                var message = Managers.Languages.GetResourceString("StaffelAskDelete", "Are you sure you want to delete this staffel?");
                var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
                if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = (EnergyUse.Models.Staffel)BsStaffel.Current;
                    _unitOfWork.Delete(entity);

                    BsStaffel.DataSource = _unitOfWork.Staffels;
                    BsStaffel.ResetBindings(false);
                }
            }
        }

        private void refreshStaffel()
        {
            if (!validateInput())
                return;

            InitStaffels(_rateId);
        }

        private void setBaseUcControlSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Staffel(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseUserControlSettings(this);
            if (BackColor != Color.Empty)
                DgStaffel.BackgroundColor = BackColor;
        }

        #endregion
    }
}