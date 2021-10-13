using System.Linq;
using HotChocolate.Resolvers;
using Playground.Api.Data;
using Playground.Api.Data.Entities;

namespace Playground.Api.GraphQL.Resolvers
{
    public class AuthorResolver
    {
        private readonly LibraryContext _context;

        public AuthorResolver(LibraryContext context)
        {
            _context = context;
        }

        public Author GetAuthor(Book book, IResolverContext ctx)
        {
            return  _context.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
        }
    }
}