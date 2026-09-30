using MiniIssueTracker.Issues;

namespace MiniIssueTracker.Tests;

public class IssueTests
{
    [Fact]
    public void NewIssue_ShouldHaveOpenStatus()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug
        };

        Assert.Equal(IssueStatus.Open, issue.Status);
    }

    [Fact]
    public void NewIssue_ShouldHaveMediumPriority()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug
        };

        Assert.Equal(IssuePriority.Medium, issue.Priority);
    }

    [Fact]
    public void NewIssue_ShouldSetCreatedAt()
    {
        DateTime before = DateTime.UtcNow;

        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug
        };

        DateTime after = DateTime.UtcNow;

        Assert.InRange(issue.CreatedAt, before, after);
    }
}
