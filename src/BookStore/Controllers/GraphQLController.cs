using System;
using System.Threading.Tasks;
using BookStore.GraphQL;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;
        private DataLoaderDocumentListener dataLoaderDocumentListener;

        public GraphQLController(IDocumentExecuter executer, ISchema schema, DataLoaderDocumentListener dataLoaderDocumentListener = null)
        {
            this.executer = executer;
            this.schema = schema;
            this.dataLoaderDocumentListener = dataLoaderDocumentListener;
        }

        [HttpPost]
        public async Task<ActionResult<ExecutionResult>> PostAsync(GraphQLQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var inputs = query.Variables.ToInputs();
            var queryToExecute = query.Query;

            var result = await executer.ExecuteAsync(options =>
            {
                options.Schema = schema;
                options.Query = queryToExecute;
                options.OperationName = query.OperationName;
                options.Inputs = inputs;

                if (dataLoaderDocumentListener != null)
                {
                    options.Listeners.Add(dataLoaderDocumentListener);
                }

                options.UserContext = new BookStoreUserContext { UserName = "User1" };

                //_.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 5 };

            }).ConfigureAwait(false);

            //if (result.Errors?.Count > 0)
            //{
            //    return BadRequest(result);
            //}

            return result;
        }        
    }
}
