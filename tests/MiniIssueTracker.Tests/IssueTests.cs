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
}
