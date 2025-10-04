using EnergyUse.Core.Interfaces;
using EnergyUse.Models;

namespace EnergyUse.Core.Controllers;

public class AddressController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Address? UnitOfWork { get; set; } = null;

    #endregion

    public AddressController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Address(_dbFileName);
    }

    public async Task<List<Address>> GetAllAdressesAsync()
    {
        if (UnitOfWork?.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

        var addresses = await UnitOfWork.AddressRepo.GetAll();
        UnitOfWork.Addresses = (List<Address>)addresses;
        return UnitOfWork.Addresses;
    }

    public List<Models.TariffGroup> GetTariffGroups(int typeId)
    {
        if (UnitOfWork?.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

        var tariffGroupList = UnitOfWork.RepoTariffGroupRepo.GetAll().ToList();
        tariffGroupList = tariffGroupList.Where(x => x.TypeId == typeId).ToList();
        return tariffGroupList;
    }

    public Models.Address AddDefaultEntity(string defaultDescription)
    {
        if (UnitOfWork?.AddressRepo == null)
        {
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");
        }

        return UnitOfWork.AddDefaultEntity(defaultDescription);
    }


}