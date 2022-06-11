using System.Net;
using System.Net.Http.Json;

using Microsoft.Extensions.DependencyInjection;

using Tempus.Common;
using Tempus.TimeBilling.Data;

namespace Tempus.TimeBilling.Tests;

public class TimeBillingTests : BaseApiTests<Program>
{
  private readonly TimeBill _newTimeBill;

  public TimeBillingTests() : base()
  {
    using var scope = _application.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetService<TimeBillingContext>()!;
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    _newTimeBill = new TimeBill()
    {
      CustomerId = 1,
      Hours = 1.1m,
      Rate = 325.00m,
      WorkerId = 1,
      WorkPerformed = "Consulting",
      Notes = "Some Notes"
    };
  }

  [Fact]
  public async void TestGetTimeBills()
  {
    var response = await _client.GetFromJsonAsync<TimeBill[]>("/api/timebills");

    Assert.Equal(new TimeBill[] { }, response);
  }

  [Fact]
  public async Task TestCreateTimeBill()
  {
    var response = await _client.PostAsJsonAsync<TimeBill>("/api/timebills", _newTimeBill);

    var updated = await response.Content.ReadFromJsonAsync<TimeBill>()!;

    Assert.NotNull(updated);
    Assert.Equal(1, updated?.CustomerId);
    Assert.Equal("Consulting", updated?.WorkPerformed);
    Assert.Equal(325m, updated?.Rate);
    Assert.Equal(1.1m, updated?.Hours);
  }

  [Fact]
  public async Task TestUpdateTimeBill()
  {
    var response = await _client.PostAsJsonAsync("/api/timebills", _newTimeBill);

    var created = await response.Content.ReadFromJsonAsync<TimeBill>();

    Assert.NotNull(created);

    created!.Hours = 1.5m;
    var updating = await _client.PutAsJsonAsync($"/api/timebills/{created.Id}", created);

    Assert.True(updating.StatusCode == HttpStatusCode.OK, "Update Failed");

    var updated = await updating.Content.ReadFromJsonAsync<TimeBill>();

    Assert.Equal(1.5m, updated?.Hours);
  }

  [Fact]
  public async Task TestDeleteTimeBill()
  {
    var response = await _client.PostAsJsonAsync("/api/timebills", _newTimeBill);

    var created = await response.Content.ReadFromJsonAsync<TimeBill>();

    Assert.NotNull(created);

    response = await _client.DeleteAsync($"/api/timebills/{created?.Id}");

    Assert.True(response.StatusCode == HttpStatusCode.OK, $"Delete Status Code failure: {response.StatusCode}");

    var isMissing = await _client.GetAsync($"/api/timebills/{created?.Id}");

    Assert.True(isMissing.StatusCode == HttpStatusCode.NotFound, "Finding Deleted Status Code failure");

  }

}