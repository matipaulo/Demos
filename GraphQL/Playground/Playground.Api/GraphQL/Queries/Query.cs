using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playground.Api.Data;
using Playground.Api.Data.Entities;

namespace Playground.Api.GraphQL.Queries
{
    public class Query
    {
        public async Task<ICollection<Author>> Authors([FromServices] LibraryContext context)
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<ICollection<Book>> Books([FromServices] LibraryContext context)
        {
            var books = await context.Books.Include(x => x.Author).ToListAsync();

            return books;
        }
    }
}