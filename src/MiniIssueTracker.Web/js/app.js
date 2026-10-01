console.log ("Mini Issue Tracker JavaScript geladen.")

const issueList = document.getElementById("issue-list");

console.log(issueList);

fetch("http://localhost:5203/api/issues")
    .then(response => response.json())
    .then(issues => 
    {
        console.log(issues);

        issues.forEach(issue =>
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

            issueItem.appendChild(issueId);
            issueItem.appendChild(issueTitle);
            issueItem.appendChild(issueDescription);
            issueItem.appendChild(issueType);
            issueItem.appendChild(issuePriority);
            issueItem.appendChild(issueStatus);

            issueList.appendChild(issueItem);
        });
    });
