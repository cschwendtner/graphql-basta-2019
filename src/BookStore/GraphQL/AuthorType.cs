using BookStore.Data.Entities;
using GraphQL.Types;

namespace BookStore.GraphQL
{
    public class AuthorType : ObjectGraphType<AuthorEntity>
    {
        public AuthorType()
        {
            Field(authorEntity => authorEntity.Id);
            Field(authorEntity => authorEntity.FirstName);
            Field(authorEntity => authorEntity.LastName);


            Field<StringGraphType>()
                .Name("profilePic")
                .Resolve(context =>
                {
                    return $"https://robohash.org/{context.Source.Id}";
                });
        }
    }
}
