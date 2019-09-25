using BookStore.Data.Entities;
using GraphQL.Types;

namespace BookStore.GraphQL
{
    public class AuthorType : ObjectGraphType<AuthorEntity>
    {
        public AuthorType()
        {
            Field(x => x.Id);
            Field(x => x.FirstName);
            Field(x => x.LastName);
        }

    }
}
