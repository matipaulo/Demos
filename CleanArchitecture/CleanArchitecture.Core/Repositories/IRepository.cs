using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(int id);
        Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity, CancellationToken cancellationToken);
        void Delete(TEntity entity, CancellationToken cancellationToken);
    }
}