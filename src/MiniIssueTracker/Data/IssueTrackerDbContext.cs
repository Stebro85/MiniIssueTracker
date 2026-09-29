using Microsoft.EntityFrameworkCore;
using MiniIssueTracker.Issues;

namespace MiniIssueTracker.Data;

public class IssueTrackerDbContext : DbContext
{
    public DbSet<Issue> Issues { get; set; }
    
    public IssueTrackerDbContext(
        DbContextOptions<IssueTrackerDbContext> options)
        : base(options)
    {
        
    }
}