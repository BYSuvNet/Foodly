using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foodly.Core;
using Foodly.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

FoodlyInfrastructure.AddDbContext(builder.Configuration, builder.Services);
builder.Services.AddScoped<IFoodOrderRepository, EFFoodOrderRepository>();
builder.Services.AddScoped<IDishRepository, EFDishRepository>();
builder.Services.AddScoped<IFoodOrderService, FoodOrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication webApp = builder.Build();

webApp.UseSwagger().UseSwaggerUI().UseCors("AllowAll");

webApp.MapGet("/orders", async (IFoodOrderService fd) => await fd.GetAllOrdersAsync());
webApp.MapGet("/orders/{orderNo}", async (string orderNo, IFoodOrderService fd) => await fd.GetOrderByOrderNoAsync(orderNo));
webApp.MapPost("/orders", async (FoodOrderRequest fo, IFoodOrderService fd) =>
{
    string orderNo = await fd.OrderFoodAsync(fo.DishId);

    if (string.IsNullOrEmpty(orderNo))
    {
        return Results.BadRequest("Nope!");
    }

    return Results.Created("/orders/" + orderNo, fo);
});

webApp.MapGet("/dishes", async (AppDbContext db) => await db.Dishes.Where(d => d.Name != "Noname").ToListAsync()).RequireCors("AllowAll");
webApp.MapPost("/dishes", (Dish dish, AppDbContext db, [FromHeader(Name = "secretpasskey")] string password) =>
{
    if (password != "pingvin") return Results.Unauthorized();

    if (dish.Name == "Noname" || dish.Price < 5) return Results.BadRequest("Äh skärp dig.");

    db.Add(dish);
    db.SaveChanges();

    return Results.Created($"/dishes/{dish.Id}", dish);
});

webApp.Run();

public partial class Program { }