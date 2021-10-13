using HotChocolate.Types;
using Playground.Api.Data.Entities;

namespace Playground.Api.GraphQL.Types
{
    public class AuthorType: ObjectType<Author>
    {
        public AuthorType()
        {
            Name = nameof(Author);
        }

        /// <inheritdoc />
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Name).Type<StringType>();
        }
    }
}