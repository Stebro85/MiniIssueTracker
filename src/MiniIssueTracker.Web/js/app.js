console.log ("Mini Issue Tracker JavaScript geladen.")

const issueList = document.getElementById("issue-list");

console.log(issueList);

const createIssueForm = document.getElementById("create-issue-form");

createIssueForm.addEventListener("submit", event =>
{
    event.preventDefault();

    const title = document.getElementById("title").value;
    const description =  document.getElementById("description").value;
    const type = document.getElementById("type").value;

    const newIssue = 
    {
        title: title,
        description: description,
        type: type
    };

    console.log(newIssue);

    fetch ("http://localhost:5203/api/issues",
    {
        method: "POST",
        headers:
        {
            "content-Type": "application/json"
        },
        body: JSON.stringify(newIssue)
    })
    .then(response =>
    {
        console.log(response);

        if (response.ok)
        {
            createIssueForm.reset();

            return response.json()
        }
    })
    .then(createdIssue =>
    {
        console.log(createdIssue);

        displayIssue(createdIssue);
    });
});

function displayIssue(issue)
{
    const issueItem = document.createElement("div");

    const issueId = document.createElement("p");
    issueId.textContent = `Issue #${issue.id}`;

    const issueTitle = document.createElement("p");
    issueTitle.textContent = issue.title;

    const issueDescription = document.createElement("p");
    issueDescription.textContent = issue.description;

    const issueType = document.createElement("p");
    issueType.textContent = `Type: ${issue.type}`;

    const issuePriority = document.createElement("p");
    issuePriority.textContent = `Prioriteit: ${issue.priority}`;

    const issueStatus = document.createElement("p");
    issueStatus.textContent = `Status: ${issue.status}`;

    const createdAt = new Date(issue.createdAt);

    const issueCreatedAt = document.createElement("p");
    issueCreatedAt.textContent = `Aangemaakt: ${createdAt.toLocaleString("nl-BE")}`;

    issueItem.appendChild(issueId);
    issueItem.appendChild(issueTitle);
    issueItem.appendChild(issueDescription);
    issueItem.appendChild(issueType);
    issueItem.appendChild(issuePriority);
    issueItem.appendChild(issueStatus);
    issueItem.appendChild(issueCreatedAt);

    issueList.appendChild(issueItem);
}

fetch("http://localhost:5203/api/issues")
    .then(response => response.json())
    .then(issues => 
    {
        console.log(issues);

        issues.forEach(issue =>
        {
            displayIssue(issue);
        });
    });
