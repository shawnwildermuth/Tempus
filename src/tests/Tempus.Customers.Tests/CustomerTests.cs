using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;

using Tempus.Common;
using Tempus.Customers.Data;

namespace Tempus.Customers.Tests;

public class CustomerTests : BaseApiTests<Program, CustomerContext>
{
  private readonly Customer _newCustomer;

  public CustomerTests() : base()
  {
    _newCustomer = new Customer()
    {
      CompanyName = "Acme Tooling",
      CompanyPhone = "404-555-1212",
      Contacts = new List<Contact>()
      {
        new Contact()
        {
          FirstName = "Joe",
          LastName = "Smith",
          Email = "joe@aol.com"
        }
      },
      Location = new Location()
      {
        LineOne = "123 Main Street",
        City = "Atlanta",
        StateProvince = "GA",
        PostalCode = "55555"
      }
    };

  }

  [Fact]
  public async Task TestGetCustomers()
  {
    var response = await _client.GetFromJsonAsync<Customer[]>("/api/customers");

    Assert.Equal(new Customer[] { }, response);
  }

  [Fact]
  public async Task TestCreateCustomer()
  {
    var response = await _client.PostAsJsonAsync<Customer>("/api/customers", _newCustomer);

    var updated = await response.Content.ReadFromJsonAsync<Customer>()!;

    Assert.NotNull(updated);
    Assert.Equal("Acme Tooling", updated?.CompanyName);
    Assert.Equal("404-555-1212", updated?.CompanyPhone);
    //Assert.Equal("Joe", updated?.Contacts.First().FirstName);
    Assert.Equal("123 Main Street", updated?.Location.LineOne);
  }

  [Fact]
  public async Task TestUpdateCustomer()
  {
    var response = await _client.PostAsJsonAsync("/api/customers", _newCustomer);

    var created = await response.Content.ReadFromJsonAsync<Customer>();

    Assert.NotNull(created);

    created!.CompanyName = "Acme Tooling, Inc.";
    created!.Location.LineTwo = "Suite 500";
    var updating = await _client.PutAsJsonAsync($"/api/customers/{created.Id}", created);

    var updated = await updating.Content.ReadFromJsonAsync<Customer>();

    Assert.Equal("Acme Tooling, Inc.", updated?.CompanyName);
    Assert.Equal("Suite 500", updated?.Location.LineTwo);
  }

  [Fact]
  public async Task TestDeleteCustomer()
  {
    var response = await _client.PostAsJsonAsync("/api/customers", _newCustomer);

    var created = await response.Content.ReadFromJsonAsync<Customer>();

    Assert.NotNull(created);

    response = await _client.DeleteAsync($"/api/customers/{created?.Id}");

    Assert.True(response.StatusCode == HttpStatusCode.OK, $"Delete Status Code failure: {response.StatusCode}");

    var isMissing = await _client.GetAsync($"/api/customers/{created?.Id}");

    Assert.True(isMissing.StatusCode == HttpStatusCode.NotFound, "Finding Deleted Status Code failure");

  }

}