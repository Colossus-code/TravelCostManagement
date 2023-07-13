using Microsoft.Extensions.Caching.Memory;
using Serilog;
using TravelCostManagement.Contracts.RepositoryContracts;
using TravelCostManagement.Contracts.ServiceContracts;
using TravelCostManagement.Implementations;
using TravelCostManagement.InfrastructureData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache(memoryCacheOptions =>
{

    memoryCacheOptions.ExpirationScanFrequency = TimeSpan.FromMinutes(3);
    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(6),
        SlidingExpiration = TimeSpan.FromMinutes(1.5)
    };
});

builder.Services.AddScoped<ITravelCostService, TravelCostService>();
builder.Services.AddScoped<IRepositoryCache, RepositoryCache>();
builder.Services.AddScoped<IRepositoryPlanetaryDistances, RepositoryPlanetaryDistances>();
builder.Services.AddScoped<IRepositoryPlanets, RepositoryPlanets>();
builder.Services.AddScoped<IRepositoryRebelPercentByPlanet, RepositoryRebelPercentByPlanet>();
builder.Services.AddScoped<IRepositoyPriceForLunarYears, RepositoyPriceForLunarYears>();

builder.Logging.ClearProviders();

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
