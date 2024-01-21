using System.Reflection;
using FluentValidation;
using Rainfall.API.Middleware;
using Rainfall.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var rainfallServiceConfig = builder.Configuration.GetSection("RainfallDataService");

builder.Services.AddHttpClient("RainfallDataService", client =>
{
    client.BaseAddress = new Uri(rainfallServiceConfig["BaseAddress"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();