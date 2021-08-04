using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Repositories;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext schoolDbContext) : base(schoolDbContext)
        {
        }

        public async Task<ICollection<Student>> GetByName(string name, CancellationToken cancellationToken)
        {
            return await SchoolDbContext.Students.Where(x => x.Name.ToLower() == name.ToLower()).ToListAsync(cancellationToken);
        }
    }
}