using MiniIssueTracker.Issues;

string? mainMenuChoice = null;

int nextId = 1;

List<Issue> issues = new List<Issue>();

// Blijf het hoofdmenu tonen totdat de gebruiker kiest om af te sluiten.
while (mainMenuChoice != "4")
{
    // 1. Menu tonen
    Console.WriteLine("1. Issue aanmaken");
    Console.WriteLine("2. Issues bekijken");
    Console.WriteLine("3. Issue behandelen");
    Console.WriteLine("4. Afsluiten");

    // 2. Keuze lezen
    mainMenuChoice = Console.ReadLine();

    // 3. Keuze verwerken
    switch (mainMenuChoice)
    {
        case "1":
            Issue newIssue = CreateIssue(nextId);
            issues.Add(newIssue);
            nextId = nextId + 1;
            break;

        case "2":
            if (issues.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen issues.");
            }
            else
            {
                Console.WriteLine($"Aantal issues: {issues.Count}");

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
            }
            break;

        case "3":
            if (issues.Count == 0)
            {
                Console.WriteLine("Er zijn nog geen issues om te behandelen.");
            }
            else
            {
                // Lees en valideer het Id van het Issue dat behandeld moet worden.
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

                // Zoek het Issue met het gekozen Id.
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
                    Console.WriteLine("Issue niet gevonden.");
                }

                // Toon de mogelijke aanpassingen voor het gevonden Issue.
                if (selectedIssue != null)
                {
                    Console.WriteLine("Wat wil je aanpassen?");
                    Console.WriteLine("1. Prioriteit");
                    Console.WriteLine("2. Status");
                    Console.WriteLine("3. Type");
                    Console.WriteLine("4. Terug");

                    string? actionChoice = Console.ReadLine();

                    while (actionChoice != "1" &&
                        actionChoice != "2" &&
                        actionChoice != "3" &&
                        actionChoice != "4")
                    {
                        Console.WriteLine("Geen geldige invoer");
                        Console.WriteLine("Geef een geldige invoer: 1. Prioriteit, 2. Status, 3. Type, 4. Terug");
                        actionChoice = Console.ReadLine();
                    }

                    switch (actionChoice)
                    {
                        case "1":
                            ChangePriority(selectedIssue);
                            break;

                        case "2":
                            ChangeStatus(selectedIssue);
                            break;

                        case "3":
                            ChangeType(selectedIssue);
                            break;

                        case "4":
                            break;
                    }
                }
            }
            break;

        case "4":
            break;

        default:
            Console.WriteLine("Geen geldige keuze.");
            break;
    }
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

static void ChangePriority(Issue issue)
{
    Console.WriteLine($"Huidige prioriteit: {issue.Priority}");

    Console.WriteLine("Kies de nieuwe prioriteit:");
    Console.WriteLine("1. Low");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. High");

    string? priorityChoice = Console.ReadLine();

    while (priorityChoice != "1" &&
           priorityChoice != "2" &&
           priorityChoice != "3")
    {
        Console.WriteLine("Geen geldige invoer!");
        Console.WriteLine("Geef een geldige invoer: 1. Low, 2. Medium, 3. High");
        priorityChoice = Console.ReadLine();
    }

    IssuePriority issuePriority;

    switch (priorityChoice)
    {
        case "1":
            issuePriority = IssuePriority.Low;
            break;

        case "2":
            issuePriority = IssuePriority.Medium;
            break;

        case "3":
            issuePriority = IssuePriority.High;
            break;

        default:
            throw new InvalidOperationException("Onverwachte waarde voor priorityChoice.");
    }

    issue.Priority = issuePriority;

    Console.WriteLine($"Nieuwe prioriteit: {issue.Priority}");
}

static void ChangeStatus(Issue issue)
{
    Console.WriteLine($"Huidige status: {issue.Status}");

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

    issue.Status = issueStatus;

    Console.WriteLine($"Nieuwe status: {issue.Status}");
}

static void ChangeType(Issue issue)
{
    Console.WriteLine($"Huidig type: {issue.Type}");

    Console.WriteLine("Kies het nieuwe type:");
    Console.WriteLine("1. Bug");
    Console.WriteLine("2. Feature");
    Console.WriteLine("3. Task");

    string? typeChoice = Console.ReadLine();

    while (typeChoice != "1" &&
           typeChoice != "2" &&
           typeChoice != "3")
    {
        Console.WriteLine("Geen geldige invoer!");
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
            throw new InvalidOperationException("Onverwachte waarde van typeChoice.");
    }

    issue.Type = issueType;

    Console.WriteLine($"Nieuw type: {issue.Type}");
}
