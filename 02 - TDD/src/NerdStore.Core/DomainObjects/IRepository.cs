using NerdStore.Core.Data;

namespace NerdStore.Core.DomainObjects
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
