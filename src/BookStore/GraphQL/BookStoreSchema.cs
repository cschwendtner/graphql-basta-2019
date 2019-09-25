using GraphQL;
using GraphQL.Types;

namespace BookStore.GraphQL
{
    //public class BookStoreSchema : Schema
    //{
    //    public BookStoreSchema()
    //    {
    //        Query = new QueryType();
    //    }
    //}


    public class BookStoreSchema : Schema
    {
        public BookStoreSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<QueryType>();

            Mutation = dependencyResolver.Resolve<MutationType>();
        }
    }

}


