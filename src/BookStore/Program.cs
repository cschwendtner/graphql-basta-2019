using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BookStore.GraphQL;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("======================");
            //Console.WriteLine("=== GraphTypeFirst ===");
            //Console.WriteLine("======================");

            //var testQuery = @"
            //    query { 
            //        TODO
            //    }
            //";

            //var schema = new BookStoreSchema();

            //var json = schema.Execute(options =>
            //{
            //    options.Query = testQuery;
            //});

            //Console.WriteLine(json);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
