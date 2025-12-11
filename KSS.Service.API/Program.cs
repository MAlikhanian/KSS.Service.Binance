using KSS.Service.Application;
using KSS.Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Set ports from appsettings.json
var httpPorts = builder.Configuration["Server:HttpPorts"];
var httpsPorts = builder.Configuration["Server:HttpsPorts"];

if (!string.IsNullOrEmpty(httpPorts))
{
    Environment.SetEnvironmentVariable("ASPNETCORE_HTTP_PORTS", httpPorts);
}

if (!string.IsNullOrEmpty(httpsPorts))
{
    Environment.SetEnvironmentVariable("ASPNETCORE_HTTPS_PORTS", httpsPorts);
}

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "KSS Service Binance API",
        Version = "v1",
        Description = "API for managing futures orders on cryptocurrency exchanges"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KSS Service Binance API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Only use HTTPS redirection if HTTPS port is configured
var httpsPort = builder.Configuration["ASPNETCORE_HTTPS_PORTS"];
if (!string.IsNullOrEmpty(httpsPort))
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
