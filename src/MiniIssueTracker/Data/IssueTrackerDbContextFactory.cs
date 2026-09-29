using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniIssueTracker.Data;

public class IssueTrackerDbContextFactory
: IDesignTimeDbContextFactory<IssueTrackerDbContext>
{
    public IssueTrackerDbContext CreateDbContext(string[] args)
    {
        string databasePath = Path.Combine("Data", "miniissuetracker.db");

        DbContextOptionsBuilder<IssueTrackerDbContext> optionsBuilder = new();

        optionsBuilder.UseSqlite($"Data Source={databasePath}");

        DbContextOptions<IssueTrackerDbContext> options = optionsBuilder.Options;

        return new IssueTrackerDbContext(options);
    }
}