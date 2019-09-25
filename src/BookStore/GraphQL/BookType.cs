using BookStore.Data;
using BookStore.Data.Entities;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using BookStore.Data.InMem;

namespace BookStore.GraphQL
{
    public class BookType : ObjectGraphType<BookEntity>
    {
        public BookType(IAuthorRepository authorRepository,
            IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            //Field<IntGraphType>()
            //    .Name("id")
            //    .Resolve(context => context.Source.Id);

            //Field<StringGraphType>()
            //    .Name("title")
            //    .Resolve(context => context.Source.Title);

            Field(bookEntity => bookEntity.Id);
            Field(bookEntity => bookEntity.Title);

            Field<AuthorType>()
                .Name("author")
                .ResolveAsync(async context =>
                {
                    var loader = dataLoaderContextAccessor.Context
                        .GetOrAddBatchLoader<int, AuthorEntity>("AuthorsByIdsDataLoader", 
                        authorRepository.GetAuthorsByIdsAsync);

                    return await loader.LoadAsync(context.Source.AuthorId);

                    //return await authorRepository.GetAuthorByIdAsync(context.Source.AuthorId);
                });

        }
    }

}



