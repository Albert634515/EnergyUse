using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmTariffGroups : Form
{
    #region FormProperties

    private TariffGroupController _controller;
    private readonly CheckedListBox _costCategories = new();
    private bool _loadingCategories;

    #endregion

    #region InitForm

    public frmTariffGroups()
    {
        InitializeComponent();
        initializeForm();
    }

    private void initializeForm()
    {
        _controller = new TariffGroupController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        setBaseFormSettings();
        setTariffGroupTypeCombo("");
        var categoryGroup = new GroupBox
        {
            Text = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "nl"
                ? "Toegestane kostencategorieën" : "Allowed cost categories",
            Location = new Point(12, 105),
            Size = new Size(gbCostCategory.ClientSize.Width - 24, gbCostCategory.ClientSize.Height - 117),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };
        _costCategories.Dock = DockStyle.Fill;
        _costCategories.CheckOnClick = true;
        _costCategories.DisplayMember = "Name";
        _costCategories.FormattingEnabled = true;
        _costCategories.Format += (_, e) =>
        {
            if (e.ListItem is EnergyUse.Models.CostCategory category)
                e.Value = $"{category.EnergyType?.Name}: {category.Name}";
        };
        categoryGroup.Controls.Add(_costCategories);
        gbCostCategory.Controls.Add(categoryGroup);
        BsTarifGroups.CurrentChanged += (_, _) => setCostCategories();
        _costCategories.ItemCheck += (_, e) =>
        {
            if (!_loadingCategories && BsTarifGroups.Current is EnergyUse.Models.TariffGroup group
                && _costCategories.Items[e.Index] is EnergyUse.Models.CostCategory category)
                _controller.UnitOfWork.SetCostCategoryAllowed(group, category.Id, e.NewValue == CheckState.Checked);
        };
        setTarifGroups();
    }

    private void setCostCategories()
    {
        _loadingCategories = true;
        try
        {
            _costCategories.Items.Clear();
            if (BsTarifGroups.Current is EnergyUse.Models.TariffGroup group)
                foreach (var category in _controller.UnitOfWork.CostCategories)
                    _costCategories.Items.Add(category, _controller.UnitOfWork.IsCostCategoryAllowed(group, category.Id));
        }
        finally { _loadingCategories = false; }
    }

    private void setTariffGroupTypeCombo(string currentValue)
    {
        var items = WinFormsEF.Managers.SelectionItemList.GetTariffGroupTypeList();
        bsTariffGroupTypes.DataSource = items;

        if (currentValue == null)
            currentValue = ((int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts).ToString();

        typeComboBox.SelectedIndex = typeComboBox.FindString(currentValue);
    }

    #endregion

    #region Events

    private void FrmTarifGroups_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = DgTarifGroups.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region Toolbar

    private void TsbAdd_Click(object sender, EventArgs e)
    {
        addTarifGroup();
    }

    private void TsbSave_Click(object sender, EventArgs e)
    {
        setTarifGroup();
    }

    private void TbsCancel_Click(object sender, EventArgs e)
    {
        cancelTarifGroup();
    }

    private void TsbDelete_Click(object sender, EventArgs e)
    {
        deleteTarifGroup();
    }

    private void TsbRefresh_Click(object sender, EventArgs e)
    {
        setTarifGroups();
    }

    private void TsbClose_Click(object sender, EventArgs e)
    {
        closeTarifGroup();
    }

    #endregion

    #region Methods

    private void setTarifGroups()
    {
        _controller.UnitOfWork.TariffGroups = _controller.UnitOfWork.TariffGroupRepo.GetAll().GetAwaiter().GetResult().ToList();

        BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
        BsTarifGroups.ResetBindings(false);
    }

    private void addTarifGroup()
    {
        var entity = _controller.UnitOfWork.SetDefaultEntity(Managers.Languages.GetResourceString("TarifGroupNewGroup", "New group"));

        BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
        BsTarifGroups.ResetBindings(false);

        BsTarifGroups.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void cancelTarifGroup()
    {
        _controller.UnitOfWork.CancelChanges();
        setTarifGroups();
    }

    private void setTarifGroup()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        DgTarifGroups.Focus();

        _controller.UnitOfWork.Complete();
    }

    private void deleteTarifGroup()
    {
        if (BsTarifGroups.Current != null)
        {
            var message = Managers.Languages.GetResourceString("TariffGroupsAskDelete", "Are you sure you want to delete this tarif group?");
            var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
            if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var entity = (EnergyUse.Models.TariffGroup)BsTarifGroups.Current;
                _controller.UnitOfWork.Delete(entity);

                BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
                BsTarifGroups.ResetBindings(false);
            }
        }
    }

    private void closeTarifGroup()
    {
        Close();
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (BackColor != Color.Empty)
            DgTarifGroups.BackgroundColor = BackColor;
    }

    #endregion
}
