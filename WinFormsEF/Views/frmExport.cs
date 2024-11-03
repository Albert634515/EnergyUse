using EnergyUse.Core.Controllers;
using System.Data;

namespace WinFormsEF.Views;

public partial class frmExport : Form
{
    #region FormProperties

    private ExportController _controller;

    #endregion

    #region InitForm

    public frmExport()
    {
        _controller = new ExportController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        setDefaultExportFile();
        LoadComboAddresses();
        LoadComboEnergyTypes();
    }

    private void LoadComboAddresses()
    {
        var addressList = _controller.UnitOfWork.AddressRepo.GetAll();
        bsAddresses.DataSource = addressList;
        cmbAddress.DataSource = bsAddresses;

        cmbAddress.SelectedIndex = -1;

        var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
        if (defaultAddress != null)
            cmbAddress.SelectedItem = defaultAddress;
    }

    private void LoadComboEnergyTypes()
    {
        var energyTypes = new List<EnergyUse.Models.EnergyType>();
        EnergyUse.Models.Address address;

        if (cmbAddress.SelectedIndex > -1)
        {
            address = (EnergyUse.Models.Address)cmbAddress.SelectedItem;
            energyTypes = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(address.Id).ToList();
            bsEnergyTypes.DataSource = energyTypes;
            cboEnergyType.DataSource = bsEnergyTypes;

            var energyType = energyTypes.Where(x => x.DefaultType == true).FirstOrDefault();
            if (energyType != null)
                cboEnergyType.SelectedItem = energyType;
        }
    }

    #endregion

    #region Events

    #endregion

    #region Toolbar

    private void tsbCloseExport_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region Buttons

    private void cmdSelectExportFile_Click(object sender, EventArgs e)
    {
        try
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = getInitialExportDirectory();
            saveFileDialog1.Title = "Save Excel sheet";
            saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
            saveFileDialog1.FileName = getInitialExportFileName();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                txtExportFile.Text = saveFileDialog1.FileName;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void cmdExport_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region Methods

    private void setDefaultExportFile()
    {
        txtExportFile.Text = Path.Combine(getInitialExportDirectory(), getInitialExportFileName());
    }

    private string getInitialExportDirectory()
    {
        string initialDirectory;

        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting("ExportDirectory");
        if (setting != null && setting.Id > 0)
            initialDirectory = setting.KeyValue;
        else
            initialDirectory = string.Empty;

        return initialDirectory;
    }

    private string getInitialExportFileName()
    {
        string intialFileName;

        if (cboEnergyType.SelectedIndex != -1)
        {
            var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            intialFileName = "ExcelSheet_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + energyType.Name + ".xlsx";
        }
        else
            intialFileName = "ExcelSheet_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

        return intialFileName;
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
    }

    #endregion
}