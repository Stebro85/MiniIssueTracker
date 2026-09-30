namespace MiniIssueTracker.Issues;

public class Issue
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required IssueType Type { get; set; }

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public IssueStatus Status { get; set; } = IssueStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool CanBeEdited()
    {
        return Status == IssueStatus.Open;
    }

    public bool CanBeDeleted()
    {
        return Status == IssueStatus.Open;
    }
}
