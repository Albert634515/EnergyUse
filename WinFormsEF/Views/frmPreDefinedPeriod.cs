using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmPreDefinedPeriod : Form
{
    #region FormProperties

    private PreDefinedPeriodController _controller;

    #endregion

    #region InitForm
    public frmPreDefinedPeriod()
    {
        _controller = new PreDefinedPeriodController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        LoadPreDefinedPeriods();
    }

    private void LoadPreDefinedPeriods()
    {
        _controller.UnitOfWork.PreDefinedPeriods = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll().ToList();
        bsPreDefinedPeriod.DataSource = _controller.UnitOfWork.PreDefinedPeriods;
    }

    private void frmPreDefinedPeriod_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = dgPeriods.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);

        if (ucDatePredefined1.HasChanges())
        {
            var unSavedIn = Managers.Languages.GetResourceString("PredefinedDates", " predefined dates");                
            e.Cancel = e.Cancel = Managers.GeneralDialogs.WarningUnsavedChangesIn(this, unSavedIn);
        }
    }

    #endregion

    #region Events

    private void bsPreDefinedPeriod_CurrentChanged(object sender, EventArgs e)
    {
        long preDefinedPeriodId = 0;

        if (bsPreDefinedPeriod.Current != null)
        {
            var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)bsPreDefinedPeriod.Current;
            preDefinedPeriodId = preDefinedPeriod.Id;
        }

        ucDatePredefined1.SetPredefinedPeriodDates(preDefinedPeriodId);
    }

    #endregion

    #region Toolbar

    private void tsbAddPredefinedPeriods_Click(object sender, EventArgs e)
    {
        addPredefinedPeriod();
    }

    private void tsbSavePredefinedPeriod_Click(object sender, EventArgs e)
    {
        setPredefinedPeriod();
    }

    private void tbsCancel_Click(object sender, EventArgs e)
    {
        cancelPredefinedPeriod();
    }

    private void tsbDeletePredefinedPeriods_Click(object sender, EventArgs e)
    {
        deletePredefinedPeriod();
    }

    private void tsbRefeshPredefinedPeriods_Click(object sender, EventArgs e)
    {
        refreshDates();
    }

    private void tbsClosePredefinedPeriods_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region Methods

    private void addPredefinedPeriod()
    {           
        var entity = _controller.UnitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newperiod", "New period"));

        bsPreDefinedPeriod.DataSource = _controller.UnitOfWork.PreDefinedPeriods;
        bsPreDefinedPeriod.ResetBindings(false);
        bsPreDefinedPeriod.Position = _controller.UnitOfWork.PreDefinedPeriods.IndexOf(entity);
    }

    private void setPredefinedPeriod()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgPeriods.Focus();

        ucDatePredefined1.SavePredefinedPeriodDate();
        _controller.UnitOfWork.Complete();
    }

    private void cancelPredefinedPeriod()
    {
        _controller.UnitOfWork.CancelChanges();
        refreshDates();
    }

    private void deletePredefinedPeriod()
    {
        if (bsPreDefinedPeriod.Current != null)
        {
            var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)bsPreDefinedPeriod.Current;
            if (preDefinedPeriod.Id > 0)
                ucDatePredefined1.DeletePredefinedPeriodByPeriodId(preDefinedPeriod.Id);

            _controller.UnitOfWork.Delete(preDefinedPeriod);
            bsPreDefinedPeriod.DataSource = _controller.UnitOfWork.PreDefinedPeriods;
            bsPreDefinedPeriod.ResetBindings(false);
        }
    }

    private void refreshDates()
    {
        long preDefinedPeriodId = 0;
        if (bsPreDefinedPeriod.Current != null)
        {
            var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)bsPreDefinedPeriod.Current;
            if (preDefinedPeriod != null)
                preDefinedPeriodId = preDefinedPeriod.Id;
        }

        if (ucDatePredefined1 != null)
            ucDatePredefined1.SetPredefinedPeriodDates(preDefinedPeriodId);
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            dgPeriods.BackgroundColor = this.BackColor;

        ucDatePredefined1.setBaseFormSettings();
    }

    #endregion
}