using System.Net.Http.Json;
using FluentAssertions;
using Foodly.Core;
namespace Foodly.Api.Integrationtests;

public class ApiControllerTests
{
    readonly Dish dish = new Dish { Name = "Pasta", Price = 10 };
    FoodlyWebApplicationFactory application;
    readonly HttpClient client;

    public ApiControllerTests()
    {
        application = new FoodlyWebApplicationFactory();
        client = application.CreateClient();
        client.DefaultRequestHeaders.Add("secretpasskey", "pingvin"); //Add a password header to the request
    }

    [Fact]
    public async Task PostingDishReturnsDishIfPasswordIsCorrect()
    {
        // Arrange / Act
        var response = await client.PostAsJsonAsync("/dishes", dish);
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Dish? dishResponse = await response.Content.ReadFromJsonAsync<Dish>();

        // Assert
        dishResponse.Should().NotBeNull();
        dishResponse!.Name.Should().Be(dish.Name);
        dishResponse!.Price.Should().Be(dish.Price);
    }
}