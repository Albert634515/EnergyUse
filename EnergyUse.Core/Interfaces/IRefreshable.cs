using EnergyUse.Models;

namespace WpfUI.Interfaces;

public interface IRefreshable
{
    void Refresh(Address address, EnergyType energyType, bool addressChanged);
}