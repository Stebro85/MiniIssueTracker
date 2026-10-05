using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MiniIssueTracker.Api.Dtos;
using MiniIssueTracker.Issues;

namespace MiniIssueTracker.Tests.Api;

public class IssueApiTests
{
    private static JsonSerializerOptions CreateJsonOptions()
    {
        JsonSerializerOptions jsonOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web);

        jsonOptions.Converters.Add(new JsonStringEnumConverter());

        return jsonOptions;
    }

    [Fact]
    public async Task GetIssues_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response =
            await client.GetAsync("/api/issues");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateIssue_ShouldReturnCreatedIssue()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        CreateIssueDto newIssue = new CreateIssueDto
        {
            Title = "API test issue",
            Description = "Aangemaakt door integration test",
            Type = IssueType.Bug
        };

        // Act
        HttpResponseMessage response =
            await client.PostAsJsonAsync("/api/issues", newIssue);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Issue? createdIssue =
            await response.Content.ReadFromJsonAsync<Issue>(
                CreateJsonOptions());

        Assert.NotNull(createdIssue);
        Assert.True(createdIssue.Id > 0);
        Assert.Equal("API test issue", createdIssue.Title);
        Assert.Equal(
            "Aangemaakt door integration test",
            createdIssue.Description);
        Assert.Equal(IssueType.Bug, createdIssue.Type);
        Assert.Equal(IssuePriority.Medium, createdIssue.Priority);
        Assert.Equal(IssueStatus.Open, createdIssue.Status);
    }

    [Fact]
    public async Task CreateIssue_WithEmptyTitle_ShouldReturnBadRequest()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        CreateIssueDto newIssue = new CreateIssueDto
        {
            Title = "",
            Description = "Beschrijving",
            Type = IssueType.Bug
        };

        // Act
        HttpResponseMessage response =
            await client.PostAsJsonAsync("/api/issues", newIssue);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task EditOpenIssue_ShouldReturnUpdatedIssue()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        Issue createdIssue = await CreateIssueAsync(client);

        EditIssueDto editedIssue = new EditIssueDto
        {
            Title = "Gewijzigde titel",
            Description = "Gewijzigde beschrijving",
            Type = IssueType.Feature
        };

        // Act
        HttpResponseMessage response =
            await client.PutAsJsonAsync(
                $"/api/issues/{createdIssue.Id}",
                editedIssue);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Issue? updatedIssue =
            await response.Content.ReadFromJsonAsync<Issue>(
                CreateJsonOptions());

        Assert.NotNull(updatedIssue);
        Assert.Equal(createdIssue.Id, updatedIssue.Id);
        Assert.Equal("Gewijzigde titel", updatedIssue.Title);
        Assert.Equal(
            "Gewijzigde beschrijving",
            updatedIssue.Description);
        Assert.Equal(IssueType.Feature, updatedIssue.Type);
    }

    [Fact]
    public async Task EditInProgressIssue_ShouldReturnBadRequest()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        Issue createdIssue = await CreateIssueAsync(client);

        HandleIssueDto handling = new HandleIssueDto
        {
            Description = createdIssue.Description,
            Type = createdIssue.Type,
            Priority = IssuePriority.High,
            Status = IssueStatus.InProgress
        };

        HttpResponseMessage handlingResponse =
            await client.PutAsJsonAsync(
                $"/api/issues/{createdIssue.Id}/handling",
                handling);

        Assert.Equal(HttpStatusCode.OK, handlingResponse.StatusCode);

        EditIssueDto editedIssue = new EditIssueDto
        {
            Title = "Mag niet gewijzigd worden",
            Description = "Mag niet gewijzigd worden",
            Type = IssueType.Feature
        };

        // Act
        HttpResponseMessage response =
            await client.PutAsJsonAsync(
                $"/api/issues/{createdIssue.Id}",
                editedIssue);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task HandleIssue_ShouldReturnUpdatedIssue()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        Issue createdIssue = await CreateIssueAsync(client);

        HandleIssueDto handling = new HandleIssueDto
        {
            Description = "Issue wordt onderzocht",
            Type = IssueType.Task,
            Priority = IssuePriority.High,
            Status = IssueStatus.InProgress
        };

        // Act
        HttpResponseMessage response =
            await client.PutAsJsonAsync(
                $"/api/issues/{createdIssue.Id}/handling",
                handling);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Issue? handledIssue =
            await response.Content.ReadFromJsonAsync<Issue>(
                CreateJsonOptions());

        Assert.NotNull(handledIssue);
        Assert.Equal(createdIssue.Id, handledIssue.Id);

        // De behandelaar mag de titel niet wijzigen.
        Assert.Equal(createdIssue.Title, handledIssue.Title);

        Assert.Equal(
            "Issue wordt onderzocht",
            handledIssue.Description);
        Assert.Equal(IssueType.Task, handledIssue.Type);
        Assert.Equal(IssuePriority.High, handledIssue.Priority);
        Assert.Equal(IssueStatus.InProgress, handledIssue.Status);
    }

    [Fact]
    public async Task DeleteOpenIssue_ShouldReturnNoContent()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        Issue createdIssue = await CreateIssueAsync(client);

        // Act
        HttpResponseMessage response =
            await client.DeleteAsync(
                $"/api/issues/{createdIssue.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteInProgressIssue_ShouldReturnBadRequest()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        Issue createdIssue = await CreateIssueAsync(client);

        HandleIssueDto handling = new HandleIssueDto
        {
            Description = createdIssue.Description,
            Type = createdIssue.Type,
            Priority = IssuePriority.High,
            Status = IssueStatus.InProgress
        };

        HttpResponseMessage handlingResponse =
            await client.PutAsJsonAsync(
                $"/api/issues/{createdIssue.Id}/handling",
                handling);

        Assert.Equal(HttpStatusCode.OK, handlingResponse.StatusCode);

        // Act
        HttpResponseMessage response =
            await client.DeleteAsync(
                $"/api/issues/{createdIssue.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetNonExistingIssue_ShouldReturnNotFound()
    {
        // Arrange
        await using CustomWebApplicationFactory factory = new();
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response =
            await client.GetAsync("/api/issues/999999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static async Task<Issue> CreateIssueAsync(
        HttpClient client)
    {
        CreateIssueDto newIssue = new CreateIssueDto
        {
            Title = "Test issue",
            Description = "Issue voor API integration test",
            Type = IssueType.Bug
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync("/api/issues", newIssue);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Issue? createdIssue =
            await response.Content.ReadFromJsonAsync<Issue>(
                CreateJsonOptions());

        Assert.NotNull(createdIssue);

        return createdIssue;
    }
}