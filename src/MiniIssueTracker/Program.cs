using MiniIssueTracker.Issues;

Console.WriteLine("Geef de titel van het Issue:");
string? title = Console.ReadLine();
while (string.IsNullOrWhiteSpace(title))
{
    Console.WriteLine("Titel is verplicht");
    Console.WriteLine("Geef de titel van het Issue:");
    title = Console.ReadLine();
}

Console.WriteLine("Geef de beschrijving van het Issue:");
string? description = Console.ReadLine();
while (string.IsNullOrWhiteSpace(description))
{
    Console.WriteLine("Beschrijving is verplicht");
    Console.WriteLine("Geef de beschrijving van het Issue:");
    description = Console.ReadLine();
}

Console.WriteLine("Kies het type:");
Console.WriteLine("1. Bug");
Console.WriteLine("2. Feature");
Console.WriteLine("3. Task");
string? typeChoice = Console.ReadLine();

while (typeChoice != "1" &&
       typeChoice != "2" &&
       typeChoice != "3")
{
    Console.WriteLine("Geen geldige invoer");
    Console.WriteLine("Geef een geldige invoer: 1. Bug, 2. Feature, 3. Task");
    typeChoice = Console.ReadLine();
}

IssueType issueType;

switch (typeChoice)
{
    case "1":
        issueType = IssueType.Bug;
        break;

    case "2":
        issueType = IssueType.Feature;
        break;

    case "3":
        issueType = IssueType.Task;
        break;

    default:
        throw new InvalidOperationException("Onverwachte waarde voor typeChoice.");
}

Issue myFirstIssue = new Issue
{
    Title = title,
    Description = description,
    Type = issueType
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
