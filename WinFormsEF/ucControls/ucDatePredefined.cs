using System.Data;

namespace WinFormsEF.ucControls;

public partial class ucDatePredefined : UserControl
{
    #region FormProperties

    private EnergyUse.Core.UnitOfWork.PredefinedPeriodDate _unitOfWork;
    private List<EnergyUse.Models.PreDefinedPeriodDate> _predefinedPeriodDates;
    private long _currentId { get; set; }

    #endregion

    #region InitControl
    public ucDatePredefined()
    {
        InitializeComponent();
    }

    private void setEnergyTypes()
    {
        bsEnergyTypes.DataSource = _unitOfWork.EnergyTypeRepo.GetAll().ToList();

    }

    private void setTarifGroups()
    {
        bsTarifGroups.DataSource = _unitOfWork.TarifGroupRepo.GetAll().ToList();
        cboTarifGroup.SelectedIndex = -1;
    }

    public void SetPredefinedPeriodDates(long preDefinedPeriodId)
    {
        _currentId = preDefinedPeriodId;

        _predefinedPeriodDates = _unitOfWork.PreDefinedPeriodDateRepo.GetByPeriodId(preDefinedPeriodId).ToList();
        bsPreDefinedPeriodDates.DataSource = _predefinedPeriodDates;
    }

    #endregion

    #region ToolBar

    private void tsbAddPredefinedPeriods_Click(object sender, EventArgs e)
    {
        addPredefinedPeriodDate();
    }

    private void tsbSavePredefinedPeriod_Click(object sender, EventArgs e)
    {
        SavePredefinedPeriodDate();
    }

    private void tbsCancel_Click(object sender, EventArgs e)
    {
        cancelPredefinedPeriodDate();
    }

    private void tsbDeletePredefinedPeriods_Click(object sender, EventArgs e)
    {
        deletePredefinedPeriodDate();
    }

    private void tsbRefeshPredefinedPeriods_Click(object sender, EventArgs e)
    {
        SetPredefinedPeriodDates(_currentId);
    }

    #endregion

    #region Methods

    public bool HasChanges()
    {
        dgPredefinedPeriodDates.Focus();

        return _unitOfWork.HasChanges();
    }

    private void addPredefinedPeriodDate()
    {
        if (_currentId <= 0)
            return;

        var entity = new EnergyUse.Models.PreDefinedPeriodDate();
        entity.PreDefinedPeriodId = _currentId;
        entity.StartDate = DateTime.Now.Date;
        entity.EndDate = DateTime.Now.Date.AddYears(1);
        _unitOfWork.PreDefinedPeriodDateRepo.Add(entity);

        _predefinedPeriodDates.Add(entity);
        bsPreDefinedPeriodDates.DataSource = _predefinedPeriodDates;
        bsPreDefinedPeriodDates.ResetBindings(false);

        var index = _predefinedPeriodDates.IndexOf(entity);
        bsPreDefinedPeriodDates.Position = index;
    }

    public void SavePredefinedPeriodDate()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgPredefinedPeriodDates.Focus();

        _unitOfWork.Complete();
    }

    private void cancelPredefinedPeriodDate()
    {
        _unitOfWork.CancelChanges();
    }

    private void deletePredefinedPeriodDate()
    {
        if (bsPreDefinedPeriodDates.Current != null)
        {
            var preDefinedPeriodDate = (EnergyUse.Models.PreDefinedPeriodDate)bsPreDefinedPeriodDates.Current;
            if (preDefinedPeriodDate.Id > 0)
                _unitOfWork.PreDefinedPeriodDateRepo.Remove(preDefinedPeriodDate);

            _predefinedPeriodDates.Remove(preDefinedPeriodDate);
            bsPreDefinedPeriodDates.DataSource = _predefinedPeriodDates;
            bsPreDefinedPeriodDates.ResetBindings(false);
        }
    }

    public void DeletePredefinedPeriodByPeriodId(long periodId)
    {
        var _predefinedPeriodDatesToDelete = _predefinedPeriodDates.Where(x => x.PreDefinedPeriod.Id == periodId).ToList();
        if (_predefinedPeriodDatesToDelete.Count > 0)
        {
            foreach (EnergyUse.Models.PreDefinedPeriodDate predefinedPeriodDate in _predefinedPeriodDatesToDelete)
            {
                _predefinedPeriodDates.Remove(predefinedPeriodDate);
                _unitOfWork.PreDefinedPeriodDateRepo.Remove(predefinedPeriodDate);
            }

            bsPreDefinedPeriodDates.DataSource = _predefinedPeriodDates;
            bsPreDefinedPeriodDates.ResetBindings(false);
        }
    }

    public void setBaseFormSettings()
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.PredefinedPeriodDate(Managers.Config.GetDbFileName());

        Managers.Settings.SetBaseUserControlSettings(this);
        if (BackColor != Color.Empty)
            dgPredefinedPeriodDates.BackgroundColor = BackColor;

        setEnergyTypes();
        setTarifGroups();
    }


    #endregion
}