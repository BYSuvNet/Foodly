using System.Net.Http.Json;
using FluentAssertions;
using Foodly.Core;
namespace Foodly.Api.Integrationtests;

public class ApiControllerTests
{
    [Fact]
    public async Task PostingDish_ReturnsDish()
    {
        // Arrange
        var application = new FoodlyWebApplicationFactory();

        var client = application.CreateClient();

        var dish = new Dish { Name = "P1", Price = 10 };

        // Act
        //Add a header to the request
        client.DefaultRequestHeaders.Add("secretpasskey", "pingvin");
        var response = await client.PostAsJsonAsync("/dishes", dish);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        Dish? dishResponse = await response.Content.ReadFromJsonAsync<Dish>();

        dishResponse.Should().NotBeNull();
        dishResponse!.Name.Should().Be("P1");
        dishResponse!.Price.Should().Be(10);
    }

    [Fact]
    public async Task GetDish_ReturnsAllDishes()
    {
        // Arrange
        var application = new FoodlyWebApplicationFactory();

        var client = application.CreateClient();

        var dish = new Dish { Name = "P1", Price = 10 };

        // Act
        //Add a header to the request
        client.DefaultRequestHeaders.Add("secretpasskey", "pingvin");
        var response = await client.PostAsJsonAsync("/dishes", dish);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        Dish? dishResponse = await response.Content.ReadFromJsonAsync<Dish>();

        dishResponse.Should().NotBeNull();
        dishResponse!.Name.Should().Be("P1");
        dishResponse!.Price.Should().Be(10);
    }
}