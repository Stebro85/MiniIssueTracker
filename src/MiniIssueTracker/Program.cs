using MiniIssueTracker.Issues;

Issue myFirstIssue = new Issue
{
    Title = "Login werkt niet",
    Description = "Wanneer ik op de login-knop klik, gebeurt er niets.",
    Type = IssueType.Bug,
    Priority = IssuePriority.High,
    Status = IssueStatus.Open
};

Issue mySecondIssue = new Issue
{
    Title = "Database fout",
    Description = "De applicatie kan geen verbinding maken met de database.",
    Type = IssueType.Task,
    Priority = IssuePriority.Medium,
    Status = IssueStatus.InProgress
}; 

List<Issue> issues = new List<Issue>();
issues.Add(myFirstIssue);
issues.Add(mySecondIssue);
Console.WriteLine(issues.Count);

foreach (Issue issue in issues)
{
    Console.WriteLine(issue.Id);
    Console.WriteLine(issue.Title);
    Console.WriteLine(issue.Description);
    Console.WriteLine(issue.Type);
    Console.WriteLine(issue.Priority);
    Console.WriteLine(issue.Status);
    Console.WriteLine(issue.CreatedAt);
}
