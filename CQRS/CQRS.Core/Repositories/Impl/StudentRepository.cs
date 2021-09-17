using CQRS.Core.Data;
using CQRS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Repositories.Impl
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext schoolDbContext) : base(schoolDbContext)
        {
        }

        public async Task<ICollection<Student>> GetByName(string name, CancellationToken cancellationToken)
        {
            return await SchoolDbContext.Students.Where(x => string.Equals(x.Name, name, System.StringComparison.CurrentCultureIgnoreCase)).ToListAsync(cancellationToken);
        }
    }
}