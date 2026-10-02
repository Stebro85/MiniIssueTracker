console.log ("Mini Issue Tracker JavaScript geladen.");

const issueList = document.getElementById("issue-list");
const issueError = document.getElementById("issue-error");

console.log(issueList);

const createIssueForm = document.getElementById("create-issue-form");
const roleSelect = document.getElementById("role");
const createSection = document.getElementById("create-section");
const createError = document.getElementById("create-error");

console.log(roleSelect);

const editSection = document.getElementById("edit-section");
const editIssueForm = document.getElementById("edit-issue-form");
const editError = document.getElementById("edit-error");
const handleSection = document.getElementById("handle-section");
const handleIssueForm = document.getElementById("handle-issue-form");
const handleError = document.getElementById("handle-error");

let editingIssueId = null;
let handlingIssueId = null;
let issues = [];

roleSelect.addEventListener("change", updateRoleView);

updateRoleView() ; 

createIssueForm.addEventListener("submit", event =>
{
    event.preventDefault();

    createError.hidden = true;
    createError.textContent = "";

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
        else
        {
            throw new Error("Issue kon niet worden aangemaakt.");
        }
    })
    .then(createdIssue =>
    {
        console.log(createdIssue);

        issues.push(createdIssue);

        displayIssue(createdIssue);
    })
    .catch(error =>
    {
        console.error(error);

        createError.textContent = error.message;
        createError.hidden = false;
    });
});

editIssueForm.addEventListener("submit", event =>
{
    event.preventDefault();

    editError.hidden =true;
    editError.textContent = "";
    
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
        else
        {
            throw new Error("Issue kon niet worden bijgewerkt.");
        }
    })
    .then(updatedIssue =>
    {
        console.log(updatedIssue);

        const issueIndex =
            issues.findIndex(issue => issue.id === updatedIssue.id);
        
            issues[issueIndex] = updatedIssue;

        const existingIssueItem =
            document.getElementById(`issue-${updatedIssue.id}`);
        
        console.log(existingIssueItem);

        displayIssue(updatedIssue, existingIssueItem);

        editSection.hidden = true;
        editingIssueId = null;
    })
    .catch(error =>
    {
        console.error(error);

        editError.textContent = error.message;
        editError.hidden = false;
    });
});

handleIssueForm.addEventListener("submit", event =>
{
    event.preventDefault();

    handleError.hidden = true;
    handleError.textContent = "";

    const description =
        document.getElementById("handle-description").value;

    const type =
        document.getElementById("handle-type").value;

    const priority =
        document.getElementById("handle-priority").value;

    const status = 
        document.getElementById("handle-status").value;

    const handleIssue =
    {
        description: description,
        type: type,
        priority: priority,
        status: status
    };

    console.log(`Issue #${handlingIssueId} wordt behandeld.`);
    console.log(handleIssue);

    fetch(`http://localhost:5203/api/issues/${handlingIssueId}/handling`,
    {
        method: "PUT",
        headers: 
        {
            "Content-Type": "application/json"
        },
        body:JSON.stringify(handleIssue)
    })
    .then(response =>
    {
        console.log(response);

        if (response.ok)
        {
            return response.json();
        }
        else
        {
            throw new Error("Issue kon niet worden behandeld.");
        }
    })
    .then(handledIssue =>
    {
        console.log(handledIssue);

        const issueIndex = 
            issues.findIndex(issue => issue.id === handledIssue.id);
               
        issues[issueIndex] = handledIssue;

        const existingIssueItem =
            document.getElementById(`issue-${handledIssue.id}`);

        displayIssue(handledIssue, existingIssueItem);

        handleSection.hidden = true;
        handlingIssueId = null;
    })
    .catch(error =>
    {
        console.error(error);

        handleError.textContent = error.message;
        handleError.hidden = false;
    });
});

function updateRoleView()
{
    console.log(`Gekozen rol: ${roleSelect.value}`);

    if (roleSelect.value === "Reporter")
    {
        createSection.hidden = false;
    }
    else
    {
        createSection.hidden = true;
    }

    editSection.hidden = true;
    editingIssueId = null;

    handleSection.hidden = true;
    handlingIssueId = null;

    displayIssues();
}

function displayIssues()
{
    issueList.innerHTML = "";

    issues.forEach(issue =>
    {
        displayIssue(issue);
    });
}

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

    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Verwijderen";

    const handleButton = document.createElement("button");
    handleButton.textContent = "Behandelen";

    handleButton.addEventListener("click",() =>
    {
        handlingIssueId = issue.id;

        handleSection.hidden = false;

        document.getElementById("handle-description").value = issue.description;
        document.getElementById("handle-type").value = issue.type;
        document.getElementById("handle-priority").value = issue.priority;
        document.getElementById("handle-status").value = issue.status;
    });
    
    deleteButton.addEventListener("click", () =>
    {
        fetch(`http://localhost:5203/api/issues/${issue.id}`,
        {
            method: "DELETE"
        })
        .then(response =>
        {
            console.log(response);

            if (response.ok)
            {
                issueError.hidden = true;
                issueError.textContent = "";

                const issueItem =
                    document.getElementById(`issue-${issue.id}`);
                
                issues = issues.filter(existingIssue => existingIssue.id !== issue.id);

                issueItem.remove();
            }
            else
            {
                throw new Error("Issue kan niet worden verwijderd.");
            }
        })
        .catch(error =>
        {
            console.error(error);

            issueError.textContent = error.message;
            issueError.hidden = false;

            loadIssues();
        });
    });

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

    if (roleSelect.value === "Reporter" && issue.status === "Open")
    {
        issueItem.appendChild(editButton);
        issueItem.appendChild(deleteButton);
    }

    if (roleSelect.value === "Handler" && issue.status !== "Done")
    {
        issueItem.appendChild(handleButton);
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

function loadIssues()
{
    fetch("http://localhost:5203/api/issues")
        .then(response => 
        {
            if (response.ok)
            {
                return response.json();
            }
            else
            {
                throw new Error("Issues konden niet worden geladen.");
            }
        })
        .then(loadedIssues => 
        {
            console.log(loadedIssues);

            issueError.hidden = true;
            issueError.textContent = "";
            
            issues = loadedIssues;

            displayIssues();
        })
        .catch(error =>
        {
            console.error(error);

            issueError.textContent = "Issues konden niet worden geladen."
            issueError.hidden = false;
        });
}

loadIssues();
