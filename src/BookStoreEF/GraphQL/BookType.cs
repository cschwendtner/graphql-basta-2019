using BookStore.Data;
using BookStore.Data.Entities;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace BookStore.GraphQL
{
    public class BookType : ObjectGraphType<BookEntity>
    {
        public BookType(IDependencyResolver dependencyResolver, 
            IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.Title);

            Field<AuthorType>()
                .Name("author")
                .ResolveAsync(async context => {
                    var authorRepository = dependencyResolver.Resolve<IAuthorRepository>();
                    return await authorRepository.GetAuthorByIdAsync(context.Source.AuthorId);

                    //var loader = dataLoaderContextAccessor.Context
                    //    .GetOrAddBatchLoader<int, AuthorEntity>("AuthorsByIdsDataLoader", authorRepository.GetAuthorsByIdsAsync);
                    //return await loader.LoadAsync(context.Source.AuthorId);
                });
        }
    }
}

