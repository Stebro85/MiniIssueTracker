using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniIssueTracker.Data;

namespace MiniIssueTracker.Tests.Api;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection = new("Data Source=:memory:");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _connection.Open();

        builder.ConfigureServices(services =>
        {
            services.Remove(
                services.Single(
                    service => service.ServiceType ==
                        typeof(DbContextOptions<IssueTrackerDbContext>)));

            services.AddDbContext<IssueTrackerDbContext>(options =>
                options.UseSqlite(_connection));

            using IServiceScope scope = services.BuildServiceProvider().CreateScope();

            IssueTrackerDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<IssueTrackerDbContext>();

            dbContext.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _connection.Dispose();
        }
    }
}