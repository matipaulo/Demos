using CQRS.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<ICollection<Student>> GetByName(string name, CancellationToken cancellationToken);
    }
}