using Microsoft.EntityFrameworkCore;
using MiniIssueTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/issues", (IssueTrackerDbContext dbContext) =>
{
    return dbContext.Issues.ToList();
});

app.Run();
