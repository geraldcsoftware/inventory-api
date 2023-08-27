using System.Net;
using System.Text.Json;
using FluentAssertions;
using Inventory.Tests.EndpointTests.Fixtures;

namespace Inventory.Tests.EndpointTests;

[Collection(nameof(DbCollectionFixture))]
public class GetInventoryCategoriesTests(ApplicationFactory factory, CategoriesDbFixture dbFixture): IClassFixture<ApplicationFactory>
{
    [Fact]
    public async Task GetCategories_ShouldReturnCategories()
    {
        // Arrange
        var client = factory.WithConnectionString(dbFixture.ConnectionString).CreateClient();

        // Act
        var response = await client.GetAsync("/categories");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseContent);

        responseJson.RootElement.EnumerateArray().Should().NotBeEmpty();
    }
}