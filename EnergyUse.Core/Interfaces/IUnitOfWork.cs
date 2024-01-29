namespace EnergyUse.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();

        bool HasChanges();

        void CancelChanges();
    }
}
