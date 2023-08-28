using System.Net;
using System.Text.Json;
using FluentAssertions;
using Inventory.Api.Endpoints.Categories;
using Inventory.Tests.EndpointTests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Inventory.Tests.EndpointTests;
[Collection(nameof(DbCollectionFixture))]
public class GetInventoryCategoryByIdTests(ApplicationFactory factory, CategoriesDbFixture dbFixture) : IClassFixture<ApplicationFactory>
{
    private readonly WebApplicationFactory<GetCategoriesEndpoint> _factory = factory.WithConnectionString(dbFixture.ConnectionString);

    [Fact]
    public async Task GetCategories_WhenValidIdIsPassed_ShouldReturnCategories()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/categories/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseContent);

        responseJson.RootElement.Should().NotBeNull();
        responseJson.RootElement.GetProperty("id").GetInt32().Should().Be(1);
    }
    
    [Fact]
    public async Task GetCategories_WhenInvalidIdIsPassed_ShouldReturnNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/categories/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}