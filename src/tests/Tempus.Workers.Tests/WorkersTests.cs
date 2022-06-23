using System.Net;
using System.Net.Http.Json;

using Tempus.Common;
using Tempus.Workers.Data;

namespace Tempus.Workers.Tests;

public class WorkersTests : BaseApiTests<Program, WorkerContext>
{
  private readonly Worker _newWorker;

  public WorkersTests() : base()
  {

    _newWorker = new Worker()
    {
      BaseRate = 300m,
      FirstName = "Shawn",
      LastName = "Smith",
      Email = "shawn@aol.com",
      UserName = "shawnwildermuth"
    };
  }

  [Fact]
  public async void TestGetWorkers()
  {
    var response = await _client.GetFromJsonAsync<ResultSet<Worker>>("/api/workers");

    Assert.Equal(new Worker[] { }, response.Results);
  }

  [Fact]
  public async Task TestCreateWorker()
  {
    var response = await _client.PostAsJsonAsync<Worker>("/api/workers", _newWorker);

    var updated = await response.Content.ReadFromJsonAsync<Worker>()!;

    Assert.NotNull(updated);
    Assert.Equal("shawnwildermuth", updated?.UserName);
    Assert.Equal(300m, updated?.BaseRate);
  }

  [Fact]
  public async Task TestUpdateWorker()
  {
    var response = await _client.PostAsJsonAsync("/api/workers", _newWorker);

    var created = await response.Content.ReadFromJsonAsync<Worker>();

    Assert.NotNull(created);

    created!.BaseRate = 325m;
    var updating = await _client.PutAsJsonAsync($"/api/workers/{created.Id}", created);

    Assert.True(updating.StatusCode == HttpStatusCode.OK, "Update Failed");

    var updated = await updating.Content.ReadFromJsonAsync<Worker>();

    Assert.Equal(325m, updated?.BaseRate);
  }

  [Fact]
  public async Task TestDeleteWorker()
  {
    var response = await _client.PostAsJsonAsync("/api/workers", _newWorker);

    var created = await response.Content.ReadFromJsonAsync<Worker>();

    Assert.NotNull(created);

    response = await _client.DeleteAsync($"/api/workers/{created?.Id}");

    Assert.True(response.StatusCode == HttpStatusCode.OK, $"Delete Status Code failure: {response.StatusCode}");

    var isMissing = await _client.GetAsync($"/api/workers/{created?.Id}");

    Assert.True(isMissing.StatusCode == HttpStatusCode.NotFound, "Finding Deleted Status Code failure");

  }
}