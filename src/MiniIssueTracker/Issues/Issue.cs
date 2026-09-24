namespace MiniIssueTracker.Issues;

public class Issue
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required IssueType Type { get; set; }

    public required IssuePriority Priority { get; set; }

    public required IssueStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
