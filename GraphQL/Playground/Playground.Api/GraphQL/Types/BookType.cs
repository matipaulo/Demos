using HotChocolate.Types;
using Playground.Api.Data.Entities;

namespace Playground.Api.GraphQL.Types
{
    public sealed class BookType : ObjectType<Book>
    {
        public BookType()
        {
            Name = nameof(Book);
        }

        /// <inheritdoc />
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Title).Type<StringType>();
            descriptor.Field(x => x.AuthorId).Type<IdType>();
        }
    }
}