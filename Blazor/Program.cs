using Foodly.Core;
using Foodly.Infrastructure;
using Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

FoodlyInfrastructure.AddDbContext(builder.Configuration, builder.Services);
builder.Services.AddScoped<IFoodOrderRepository, EFFoodOrderRepository>();
builder.Services.AddScoped<IDishRepository, EFDishRepository>();
builder.Services.AddScoped<IFoodOrderService, FoodOrderService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
