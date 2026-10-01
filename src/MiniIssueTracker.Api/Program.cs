using Microsoft.EntityFrameworkCore;
using MiniIssueTracker.Data;
using MiniIssueTracker.Issues;
using MiniIssueTracker.Api.Dtos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

string databasePath = Path.GetFullPath(
    Path.Combine(
        builder.Environment.ContentRootPath,
        "..",
        "MiniIssueTracker",
        "Data",
        "miniissuetracker.db"));
    
builder.Services.AddDbContext<IssueTrackerDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/issues", (IssueTrackerDbContext dbContext) =>
{
    return dbContext.Issues.ToList();
});

app.MapGet("/api/issues/{id}", (int id, IssueTrackerDbContext dbContext) =>
{
    Issue? issue = dbContext.Issues.Find(id);

    if (issue is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(issue);
});

app.MapPost("/api/issues", (CreateIssueDto createIssueDto, IssueTrackerDbContext dbContext) =>
{
    if (string.IsNullOrWhiteSpace(createIssueDto.Title))
    {
        return Results.BadRequest("Titel is verplicht.");
    }

    if (string.IsNullOrWhiteSpace(createIssueDto.Description))
    {
        return Results.BadRequest("Beschrijving is verplicht.");
    }

    if (!Enum.IsDefined(createIssueDto.Type))
    {
        return Results.BadRequest("Geen geldig issue type.");
    }

    Issue newIssue = new Issue
    {
        Title = createIssueDto.Title,
        Description = createIssueDto.Description,
        Type = createIssueDto.Type
    };

    dbContext.Issues.Add(newIssue);
    dbContext.SaveChanges();

    return Results.Created($"/api/issues/{newIssue.Id}", newIssue);
});

app.MapPut("/api/issues/{id}", (int id, EditIssueDto editIssueDto, IssueTrackerDbContext dbContext) =>
{
    Issue? issue = dbContext.Issues.Find(id);

    if (issue is null)
    {
        return Results.NotFound();
    }

    if (!issue.CanBeEdited())
    {
        return Results.BadRequest("Dit issue kan niet meer bewerkt worden omdat het al in behandeling is.");
    }

    if (string.IsNullOrWhiteSpace(editIssueDto.Title))
    {
        return Results.BadRequest("Titel is verplicht.");
    }

    if (string.IsNullOrWhiteSpace(editIssueDto.Description))
    {
        return Results.BadRequest("Beschrijving is verplicht.");
    }

    if (!Enum.IsDefined(editIssueDto.Type))
    {
        return Results.BadRequest("Geen geldig issue type.");
    }

    issue.Title = editIssueDto.Title;
    issue.Description = editIssueDto.Description;
    issue.Type = editIssueDto.Type;

    dbContext.SaveChanges();

    return Results.Ok(issue);
});

app.MapPut("/api/issues/{id}/handling", (int id, HandleIssueDto handleIssueDto, IssueTrackerDbContext dbContext) =>
{
    Issue? issue = dbContext.Issues.Find(id);

    if (issue is null)
    {
        return Results.NotFound();
    }

    if (string.IsNullOrWhiteSpace(handleIssueDto.Description))
    {
        return Results.BadRequest("Beschrijving is verplicht.");
    }

    if (!Enum.IsDefined(handleIssueDto.Type))
    {
        return Results.BadRequest("Geen geldig issue type.");
    }

    if (!Enum.IsDefined(handleIssueDto.Priority))
    {
        return Results.BadRequest("Geen geldige prioriteit.");
    }

    if (!Enum.IsDefined(handleIssueDto.Status))
    {
        return Results.BadRequest("Geen geldige status.");
    }

    issue.Description = handleIssueDto.Description;
    issue.Type = handleIssueDto.Type;
    issue.Priority = handleIssueDto.Priority;
    issue.Status = handleIssueDto.Status;

    dbContext.SaveChanges();

    return Results.Ok (issue);
});

app.MapDelete("/api/issues/{id}", (int id, IssueTrackerDbContext dbContext) =>
{
    Issue? issue = dbContext.Issues.Find(id);

    if (issue is null)
    {
        return Results.NotFound();
    }

    if (!issue.CanBeDeleted())
    {
        return Results.BadRequest("Dit issue kan niet meer verwijderd worden omdat het al in behandeling is.");
    }

    dbContext.Issues.Remove(issue);
    dbContext.SaveChanges();

    return Results.NoContent();
});

app.Run();
