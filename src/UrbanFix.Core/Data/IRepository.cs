
using UrbanFix.Core.DomainObjects;

namespace UrbanFix.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
