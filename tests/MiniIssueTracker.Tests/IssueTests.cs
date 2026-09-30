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

    [Fact]
    public void OpenIssue_ShouldBeEditable()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type =  IssueType.Bug,
            Status = IssueStatus.Open
        };

        bool result = issue.CanBeEdited();

        Assert.True(result);
    }

    [Fact]
    public void InProgressIssue_ShouldNotBeEditable()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug,
            Status = IssueStatus.InProgress
        };

        bool result = issue.CanBeEdited();

        Assert.False(result);
    }

    [Fact]
    public void DoneIssue_ShouldNotBeEditable()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug,
            Status = IssueStatus.Done
        };

        bool result = issue.CanBeEdited();

        Assert.False(result);
    }

    [Fact]
    public void OpenIssue_ShouldBeDeletable()
    {
        Issue issue = new Issue
        {
            Title = "Test Issue",
            Description = "Test description",
            Type = IssueType.Bug,
            Status = IssueStatus.Open
        };

        bool result  =  issue.CanBeDeleted();

        Assert.True(result);
    }

    [Fact]
    public void InProgressIssue_ShouldNotBeDeletable()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug,
            Status = IssueStatus.InProgress
        };

        bool result = issue.CanBeDeleted();

        Assert.False(result);
    }

    [Fact]
    public void DoneIssue_ShouldNotBeDeletable()
    {
        Issue issue = new Issue
        {
            Title = "Test issue",
            Description = "Test description",
            Type = IssueType.Bug,
            Status = IssueStatus.Done
        };

        bool result = issue.CanBeDeleted();

        Assert.False(result);
    }
}
