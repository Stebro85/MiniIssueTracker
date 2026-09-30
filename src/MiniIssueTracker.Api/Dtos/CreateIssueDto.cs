using MiniIssueTracker.Issues;

namespace MiniIssueTracker.Api.Dtos;

public class CreateIssueDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required IssueType Type { get; set; }
}
