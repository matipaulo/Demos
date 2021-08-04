using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<ICollection<Student>> GetByName(string name, CancellationToken cancellationToken);
    }
}