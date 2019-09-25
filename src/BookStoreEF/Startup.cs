using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.EF;
using BookStore.GraphQL;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();

            // ### BookStore EF ###
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<ISchema, BookStoreSchema>();
            services.AddSingleton<QueryType>();
            services.AddSingleton<MutationType>();
            services.AddSingleton<BookType>();
            services.AddSingleton<AuthorType>();
            
            services.AddTransient<IBookRepository, Data.EF.BookRepository>();
            services.AddTransient<IAuthorRepository, Data.EF.AuthorRepository>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IDependencyResolver>(sp => new FuncDependencyResolver(type =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext.RequestServices.GetRequiredService(type);
            }));

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:BookStoreDb"]),
                ServiceLifetime.Transient);
            // ######
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookStoreContext bookStoreContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // ### BookStore ###
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            bookStoreContext.SeedData();
            // ######
        }
    }
}
