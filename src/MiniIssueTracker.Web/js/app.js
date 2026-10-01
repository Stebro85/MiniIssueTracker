console.log ("Mini Issue Tracker JavaScript geladen.")

const issueList = document.getElementById("issue-list");

console.log(issueList);

const createIssueForm = document.getElementById("create-issue-form");

const editSection = document.getElementById("edit-section");

const editIssueForm = document.getElementById("edit-issue-form");

let editingIssueId = null;

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

editIssueForm.addEventListener("submit", event =>
{
    event.preventDefault();

    const title = document.getElementById("edit-title").value;
    const description = document.getElementById("edit-description").value;
    const type = document.getElementById("edit-type").value;

    const editedIssue=
    {
        title: title,
        description: description,
        type: type
    };
    
    console.log(`Issue #${editingIssueId} wordt opgeslagen.`);
    console.log(editedIssue);

    fetch(`http://localhost:5203/api/issues/${editingIssueId}`,
    {
        method:"PUT",
        headers:
        {
            "content-Type": "application/json"
        },
        body: JSON.stringify(editedIssue)
    })
    .then(response =>
    {
        console.log(response);

        if (response.ok)
        {
            return response.json();
        }
    })
    .then(updatedIssue =>
    {
        console.log(updatedIssue);

        const existingIssueItem =
            document.getElementById(`issue-${updatedIssue.id}`);
        
        console.log(existingIssueItem);

        displayIssue(updatedIssue, existingIssueItem);

        editSection.hidden = true;
        editingIssueId = null;
    });
});

function displayIssue(issue, existingIssueItem = null)
{
    const issueItem = document.createElement("div");
    issueItem.id = `issue-${issue.id}`;

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

    const editButton = document.createElement("button");
    editButton.textContent = "Bewerken";

    editButton.addEventListener("click", () =>
    {
        editingIssueId = issue.id;

        editSection.hidden = false;

        document.getElementById("edit-title").value = issue.title;
        document.getElementById("edit-description").value = issue.description;
        document.getElementById("edit-type").value = issue.type;
    });

    issueItem.appendChild(issueId);
    issueItem.appendChild(issueTitle);
    issueItem.appendChild(issueDescription);
    issueItem.appendChild(issueType);
    issueItem.appendChild(issuePriority);
    issueItem.appendChild(issueStatus);
    issueItem.appendChild(issueCreatedAt);

    if (issue.status === "Open")
    {
        issueItem.appendChild(editButton);
    }

    if (existingIssueItem)
    {
        existingIssueItem.replaceWith(issueItem);
    }
    else
    {
        issueList.appendChild(issueItem);
    }
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
