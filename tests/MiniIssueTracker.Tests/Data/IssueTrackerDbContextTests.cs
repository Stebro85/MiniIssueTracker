using MiniIssueTracker.Data;
using MiniIssueTracker.Issues;
using Microsoft.EntityFrameworkCore;

namespace MiniIssueTracker.Tests.Data;

public class IssueTrackerDbContextTests
{
    [Fact]
    public void SaveIssue_ShouldPersistIssue()
    {
        DbContextOptions<IssueTrackerDbContext> options =
            new DbContextOptionsBuilder<IssueTrackerDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

        using IssueTrackerDbContext dbContext = new IssueTrackerDbContext(options);

        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        Issue issue = new Issue
        {
            Title = "Database test",
            Description = "Integration test",
            Type = IssueType.Bug
        };

        dbContext.Issues.Add(issue);
        dbContext.SaveChanges();

        dbContext.ChangeTracker.Clear();

        Issue? storedIssue = dbContext.Issues.Find(issue.Id);

        Assert.NotNull(storedIssue);
        Assert.Equal("Database test", storedIssue.Title);
    }

    [Fact]
    public void UpdateIssue_ShouldPersistChanges()
    {
        DbContextOptions<IssueTrackerDbContext> options =
            new DbContextOptionsBuilder<IssueTrackerDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

        using IssueTrackerDbContext dbContext = new IssueTrackerDbContext(options);

        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        Issue issue = new Issue
        {
            Title = "Oude titel",
            Description = "Integration test",
            Type = IssueType.Bug
        };

        dbContext.Issues.Add(issue);
        dbContext.SaveChanges();

        issue.Title = "Nieuwe titel";
        dbContext.SaveChanges();

        dbContext.ChangeTracker.Clear();

        Issue? storedIssue = dbContext.Issues.Find(issue.Id);

        Assert.NotNull(storedIssue);
        Assert.Equal("Nieuwe titel", storedIssue.Title);
    }

    [Fact]
    public void DeleteIssue_ShouldRemoveIssue()
    {
        DbContextOptions<IssueTrackerDbContext> options =
            new DbContextOptionsBuilder<IssueTrackerDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

        using IssueTrackerDbContext dbContext = new IssueTrackerDbContext(options);

        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        Issue issue = new Issue
        {
            Title = "Te verwijderen issue",
            Description = "Integration test",
            Type = IssueType.Bug
        };

        dbContext.Issues.Add(issue);
        dbContext.SaveChanges();

        int issueId = issue.Id;

        dbContext.Issues.Remove(issue);
        dbContext.SaveChanges();

        dbContext.ChangeTracker.Clear();

        Issue? storedIssue = dbContext.Issues.Find(issueId);

        Assert.Null(storedIssue);
    }

    [Fact]
    public void SaveIssue_ShouldGenerateId()
    {
        DbContextOptions<IssueTrackerDbContext> options =
            new DbContextOptionsBuilder<IssueTrackerDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

        using IssueTrackerDbContext dbContext = new IssueTrackerDbContext(options);

        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        Issue issue = new Issue
        {
            Title = "Id test",
            Description = "Controleer automatisch gegenereerd Id",
            Type = IssueType.Bug
        };

        Assert.Equal(0, issue.Id);

        dbContext.Issues.Add(issue);
        dbContext.SaveChanges();

        Assert.True(issue.Id > 0);
    }
}
