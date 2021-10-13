namespace Playground.Api.GraphQL.Mutations
{
    public class CreateBookInput
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
    }
}