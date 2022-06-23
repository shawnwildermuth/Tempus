using System.Net;
using System.Net.Http.Json;

using Tempus.Common;
using Tempus.WorkTypes.Data;

namespace Tempus.WorkTypes.Tests;

public class WorkTypesTests : BaseApiTests<Program, WorkTypeContext>
{
  private readonly WorkType _newWorkType;

  public WorkTypesTests() : base()
  {

    _newWorkType = new WorkType()
    {
      DefaultRate = 325.25m,
      Name = "Consulting"
    };
  }

  [Fact]
  public async void TestGetWorkTypes()
  {
    var response = await _client.GetFromJsonAsync<ResultSet<WorkType>>("/api/worktypes");

    Assert.Equal(new WorkType[] { }, response?.Results);
  }

  [Fact]
  public async Task TestCreateWorkType()
  {
    var response = await _client.PostAsJsonAsync<WorkType>("/api/worktypes", _newWorkType);

    var updated = await response.Content.ReadFromJsonAsync<WorkType>()!;

    Assert.NotNull(updated);
    Assert.Equal("Consulting", updated?.Name);
    Assert.Equal(325.25m, updated?.DefaultRate);
  }

  [Fact]
  public async Task TestUpdateWorkType()
  {
    var response = await _client.PostAsJsonAsync("/api/worktypes", _newWorkType);

    var created = await response.Content.ReadFromJsonAsync<WorkType>();

    Assert.NotNull(created);

    created!.DefaultRate = 350m;
    created!.Description = "This is for most consulting projects.";

    var updating = await _client.PutAsJsonAsync($"/api/worktypes/{created.Id}", created);

    Assert.True(updating.StatusCode == HttpStatusCode.OK, "Update Failed");

    var updated = await updating.Content.ReadFromJsonAsync<WorkType>();

    Assert.Equal(350m, updated?.DefaultRate);
    Assert.Equal("This is for most consulting projects.", updated?.Description);
  }

  [Fact]
  public async Task TestDeleteWorkType()
  {
    var response = await _client.PostAsJsonAsync("/api/worktypes", _newWorkType);

    var created = await response.Content.ReadFromJsonAsync<WorkType>();

    Assert.NotNull(created);

    response = await _client.DeleteAsync($"/api/worktypes/{created?.Id}");

    Assert.True(response.StatusCode == HttpStatusCode.OK, $"Delete Status Code failure: {response.StatusCode}");

    var isMissing = await _client.GetAsync($"/api/worktypes/{created?.Id}");

    Assert.True(isMissing.StatusCode == HttpStatusCode.NotFound, "Finding Deleted Status Code failure");

  }
}