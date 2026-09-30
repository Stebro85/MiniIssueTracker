using MiniIssueTracker.Issues;

namespace MiniIssueTracker.Api.Dtos;

public class HandleIssueDto
{
    public required string Description { get; set; }
    public required IssueType Type { get; set; }
    public required IssuePriority Priority { get; set; }
    public required IssueStatus Status { get; set; }
}
