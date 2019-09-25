using BookStore.Data;
using BookStore.Data.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.GraphQL
{
    public class MutationType : ObjectGraphType
    {
        public MutationType(IAuthorRepository authorRepository)
        {
            Field<AuthorType>()
                .Name("createAuthor")
                .Argument<NonNullGraphType<StringGraphType>>("firstName", "the firstName of the new author")
                .Argument<NonNullGraphType<StringGraphType>>("lastName", "the lastName of the new author")
                .ResolveAsync(async context =>
                {
                    var firstName = context.GetArgument<string>("firstName");
                    var lastName = context.GetArgument<string>("lastName");

                    var newAuthor = new AuthorEntity() { FirstName = firstName, LastName = lastName };
                    return await authorRepository.AddAuthorAsync(newAuthor);
                });
        }
    }
}
