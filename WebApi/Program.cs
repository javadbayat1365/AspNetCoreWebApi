using Data;
using Data.Contracts;
using Entities.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var SqlProvider = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDBContext>(option => {
    option.UseSqlServer(SqlProvider,
        a => { 
            a.CommandTimeout(60);
            a.MigrationsAssembly(typeof(User).Assembly.ToString());
        });
});

builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(IGenericRepo<>));



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}