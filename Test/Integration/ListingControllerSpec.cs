using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit.Abstractions;
using Infra.Data.Models;

namespace Test.Integration;

public class ListingControllerSpec
{
  private readonly HttpClient _httpClient;
  private readonly ITestOutputHelper _testOutputHelper;

  public ListingControllerSpec(ITestOutputHelper testOutputHelper)
  {
    var webAppFactory = new WebApplicationFactory<ListingController>();
    _httpClient = webAppFactory.CreateDefaultClient();
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public async Task Spec001()
  {
    // ARRANGE
    // constructor

    // ACT
    var response = await _httpClient.GetAsync("/api/listing/ping");
    var responseString = await response.Content.ReadAsStringAsync();
    _testOutputHelper.WriteLine(">> Actual response value: " + responseString);

    // ASSERT
    Assert.Equal("pong", responseString);
  }

  // Should return Http status 200 with a list of Listings when hiting "/api/listing" endpoint
  [Fact]
  public async Task Spec002()
  {
    // ARRANGE
    // constructor
    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    // ACT
    HttpResponseMessage? response = await _httpClient.GetAsync($"/api/listing");
    string responseString = await response.Content.ReadAsStringAsync();
    IEnumerable<Listing>? responseObject = JsonConvert.DeserializeObject<IEnumerable<Listing>>(responseString);

    // ASSERT
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(responseObject);
    Assert.NotEmpty(responseObject);
  }

  // Should return Http status 200 with a single Listing model when hiting "/api/listing/{id}" endpoint
  [Fact]
  public async Task Spec003()
  {
    // ARRANGE
    // constructor
    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    // ACT
    HttpResponseMessage? response = await _httpClient.GetAsync($"/api/listing/1");
    string responseString = await response.Content.ReadAsStringAsync();
    Listing? responseObject = JsonConvert.DeserializeObject<Listing>(responseString);

    // ASSERT
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(responseObject);
  }
}
