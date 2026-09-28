using MiniIssueTracker.Issues;

int nextId = 1;

Issue myFirstIssue = CreateIssue(nextId);

nextId = nextId + 1;

Issue mySecondIssue = new Issue
{
    Id = nextId,
    Title = "Database fout",
    Description = "De applicatie kan geen verbinding maken met de database.",
    Type = IssueType.Task,
    Priority = IssuePriority.Medium,
    Status = IssueStatus.InProgress
};

nextId = nextId + 1;

List<Issue> issues = new List<Issue>();
issues.Add(myFirstIssue);
issues.Add(mySecondIssue);

Console.WriteLine("Geef het Id van het Issue dat je wilt behandelen:");

string? issueIdInput = Console.ReadLine();

bool isValidId = int.TryParse(issueIdInput, out int selectedId);

while (!isValidId)
{
    Console.WriteLine("Geen geldig Id.");
    Console.WriteLine("Geef het Id van het Issue dat je wilt behandelen:");
    issueIdInput = Console.ReadLine();

    isValidId = int.TryParse(issueIdInput, out selectedId);
}

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

Issue? selectedIssue = null;

foreach (Issue issue in issues)
{
    if (issue.Id == selectedId)
    {
        selectedIssue = issue;
        Console.WriteLine(issue.Title);
    }
}

if (selectedIssue == null)
{
    Console.WriteLine("Issue not found.");
}

if (selectedIssue != null)
{
    Console.WriteLine($"Huidige status: {selectedIssue.Status}");

    Console.WriteLine("Kies de nieuwe status:");
    Console.WriteLine("1. Open");
    Console.WriteLine("2. InProgress");
    Console.WriteLine("3. Done");

    string? statusChoice = Console.ReadLine();

    while (statusChoice != "1" &&
           statusChoice != "2" &&
           statusChoice != "3")
    {
        Console.WriteLine("Geen geldige invoer!");
        Console.WriteLine("Geef een geldige invoer: 1. Open, 2. InProgress, 3. Done.");
        statusChoice = Console.ReadLine();
    }

    IssueStatus issueStatus;

    switch (statusChoice)
    {
        case "1":
            issueStatus = IssueStatus.Open;
            break;

        case "2":
            issueStatus = IssueStatus.InProgress;
            break;

        case "3":
            issueStatus = IssueStatus.Done;
            break;

        default:
            throw new InvalidOperationException("Onverwachte waarde voor statusChoice.");
    }

    selectedIssue.Status = issueStatus;

    Console.WriteLine($"Nieuwe status: {selectedIssue.Status}");
}

static Issue CreateIssue(int id)
{
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

    Issue newIssue = new Issue
    {
        Id = id,
        Title = title,
        Description = description,
        Type = issueType
    };

    return newIssue;
}
