using BookStore.Data;
using GraphQL;
using GraphQL.Types;
using BookStore.Data.InMem;

namespace BookStore.GraphQL
{
    public class QueryType : ObjectGraphType
    {
        public QueryType(IBookRepository bookRepository)
        {
            Field<StringGraphType>()
                .Name("hello")
                .Resolve(context => "world");

            Field<BookType>()
                .Name("book")
                .Argument<NonNullGraphType<IntGraphType>>("id", "the id of the book")
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await bookRepository.GetBookByIdAsync(id);
                });

            Field<ListGraphType<BookType>>()
                .Name("books")
                .ResolveAsync(async context =>
                {
                    return await bookRepository.GetBooksAsync();
                });
        }
    }
}
