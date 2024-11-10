using iText.Commons.Utils.Collections;
using System.Data;

namespace WinFormsEF.ucControls;

public partial class ucDateSelection : UserControl
{
    #region ControlProperties

    public List<EnergyUse.Models.EnergyType> EnergyTypeList;
    public List<EnergyUse.Models.TariffGroup> TarifGroupsList;

    #endregion

    public ucDateSelection()
    {
        InitializeComponent();
        SetTarifGroups();
    }

    #region Events

    #endregion

    #region Methods

    public void SetTarifGroups()
    {
        if (TarifGroupsList is null)
            TarifGroupsList = new List<EnergyUse.Models.TariffGroup>();

        TarifGroupsList = TarifGroupsList.Where(w => w.TypeId == (int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts).ToList();
        bsTarifGroups.DataSource = TarifGroupsList;

        cboTarifGroups.SelectedIndex = -1;
    }

    public void SetEnergyTypes(long addressId)
    {
        if (EnergyTypeList is null)
            EnergyTypeList = new List<EnergyUse.Models.EnergyType>();

        bsEngeryTypes.DataSource = EnergyTypeList;

        cboEnergyType.SelectedIndex = -1;
    }

    public void SetDefaultEnergyType()
    {
        if (EnergyTypeList == null || EnergyTypeList.Count == 0)
            return;

        var energyType = EnergyTypeList.Where(x => x.DefaultType == true).FirstOrDefault();
        if (energyType != null)
            cboEnergyType.SelectedItem = energyType;
    }

    public void SetEnergyType(long energyTypeId)
    {
        if (energyTypeId <= 0)
            return;

        var energyType = EnergyTypeList.Where(x => x.Id == energyTypeId).FirstOrDefault();
        if (energyType != null)
            cboEnergyType.SelectedItem = energyType;
    }

    public void SetRemoveButtonVisibilty(bool visibilty)
    {
        cmdRemove.Visible = visibilty;
    }

    internal void SetTarifGroup(long tarifGroupId)
    {
        if (tarifGroupId <= 0)
            return;

        EnergyUse.Models.TariffGroup tarifGroup = TarifGroupsList.Where(x => x.Id == tarifGroupId && x.TypeId == (int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts).FirstOrDefault();
        if (tarifGroup != null)
            cboTarifGroups.SelectedItem = tarifGroup;
    }

    #endregion
}