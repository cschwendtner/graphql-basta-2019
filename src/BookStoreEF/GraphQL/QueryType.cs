using BookStore.Data;
using GraphQL;
using GraphQL.Types;

namespace BookStore.GraphQL
{
    public class QueryType : ObjectGraphType
    {
        public QueryType(IDependencyResolver dependencyResolver)
        {
            Field<StringGraphType>()
                .Name("hello")
                .Resolve(context => "world");

            Field<BookType>()
                .Name("book")
                .Argument<NonNullGraphType<IntGraphType>>("id", "id of the book")
                .ResolveAsync(async context =>
                {
                    var bookRepository = dependencyResolver.Resolve<IBookRepository>();
                    var id = context.GetArgument<int>("id");
                    return await bookRepository.GetBookByIdAsync(id);
                });

            Field<ListGraphType<BookType>>()
                .Name("books")
                .ResolveAsync(async context =>
                {
                    var bookRepository = dependencyResolver.Resolve<IBookRepository>();
                    return await bookRepository.GetBooksAsync();
                });
        }

    }
}
