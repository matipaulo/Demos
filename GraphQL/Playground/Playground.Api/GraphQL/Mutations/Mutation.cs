using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playground.Api.Data;
using Playground.Api.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Playground.Api.GraphQL.Mutations
{
    public class Mutation
    {
        public async Task<Book> CreateBook([FromServices] LibraryContext context, CreateBookInput inputBook)
        {
            var book = await context.Books.AddAsync(new Book
            {
                Title = inputBook.Title,
                AuthorId = inputBook.AuthorId
            });

            await context.SaveChangesAsync();

            return book.Entity;
        }

        public async Task<Author> CreateAuthor([FromServices] LibraryContext context, CreateAuthorInput inputAuthor)
        {
            var author = await context.Authors.AddAsync(new Author
            {
                Name = inputAuthor.Name
            });

            await context.SaveChangesAsync();

            return author.Entity;
        }

        public async Task DeleteBook([FromServices] LibraryContext context, DeleteBookInput inputBook)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == inputBook.Id);
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}