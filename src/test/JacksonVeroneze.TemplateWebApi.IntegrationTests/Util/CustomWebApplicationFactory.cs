using System.Data.Common;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.IntegrationTests.Util;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultScheme = null;
            //     options.DefaultForbidScheme = null;
            //     options.DefaultChallengeScheme = null;
            //     options.DefaultAuthenticateScheme = null;
            // });
            //
            // services.AddAuthorization(options =>
            // {
            //
            // });

            ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(dbContextDescriptor!);

            ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor!);

            // Create open SqliteConnection so EF won't automatically close it.
            // services.AddSingleton<DbConnection>(container =>
            // {
            //     SqliteConnection connection = new("DataSource=:memory:");
            //     connection.Open();
            //
            //     return connection;
            // });

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                //DbConnection connection = container.GetRequiredService<DbConnection>();

                options.UseInMemoryDatabase("dbname");
            });
        });

        base.ConfigureWebHost(builder);
    }
}
