using Microsoft.EntityFrameworkCore;
using Playground.Api.Data.Entities;

namespace Playground.Api.Data
{
    public class LibraryContext : DbContext
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            
        }
    }
}