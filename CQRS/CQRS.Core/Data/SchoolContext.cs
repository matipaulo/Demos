using CQRS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Core.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public SchoolContext(DbContextOptions options) : base(options)
        {
        }
    }
}