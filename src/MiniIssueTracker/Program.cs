using MiniIssueTracker.Issues;

Console.WriteLine("Hello, World!");

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

Console.WriteLine(myFirstIssue.Id);
Console.WriteLine(myFirstIssue.Title);
Console.WriteLine(myFirstIssue.Description);
Console.WriteLine(myFirstIssue.Type);
Console.WriteLine(myFirstIssue.Priority);
Console.WriteLine(myFirstIssue.Status);
Console.WriteLine(myFirstIssue.CreatedAt);

Console.WriteLine(mySecondIssue.Id);
Console.WriteLine(mySecondIssue.Title);
Console.WriteLine(mySecondIssue.Description);
Console.WriteLine(mySecondIssue.Type);
Console.WriteLine(mySecondIssue.Priority);
Console.WriteLine(mySecondIssue.Status);
Console.WriteLine(mySecondIssue.CreatedAt);
