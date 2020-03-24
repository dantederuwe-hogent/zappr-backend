using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zappr.Api.Data.Repositories;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            /*services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"))
            );*/

            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

            services.AddHttpContextAccessor();

            //External API
            services.AddSingleton<TvdbService>();

            //Repos
            services.AddTransient<IUserRepository, UserRepository>();

            //Schema
            services.AddSingleton<ISchema, ZapprSchema>();

            // Types
            services.AddSingleton<SeriesType>();
            services.AddSingleton<UserType>();

            // Queries
            services.AddSingleton<UserQuery>();
            services.AddSingleton<SeriesQuery>();
            services.AddSingleton<ZapprQuery>();


            services.AddGraphQL();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<ISchema>("/graphql");

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/"
            });
        }
    }
}
