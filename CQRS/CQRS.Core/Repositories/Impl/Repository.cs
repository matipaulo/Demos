using CQRS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Repositories.Impl
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SchoolContext SchoolDbContext;

        public Repository(SchoolContext schoolDbContext)
        {
            SchoolDbContext = schoolDbContext;
        }

        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await SchoolDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await SchoolDbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entityEntry = await SchoolDbContext.AddAsync(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public void Update(TEntity entity, CancellationToken cancellationToken)
        {
            SchoolDbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity, CancellationToken cancellationToken)
        {
            SchoolDbContext.Set<TEntity>().Remove(entity);
        }
    }
}