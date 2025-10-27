using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantHotelAds.Application.Mappings;
using RestaurantHotelAds.Application.Services.HotelsServices;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// ============================================================
// DATABASE CONFIGURATION
// ============================================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("RestaurantHotelAds.Infrastructure");
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null
            );
        }
    );

    // Enable detailed errors in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Register Unit of Work, Services, Mappings and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(RestaurantHotelAds.Infrastructure.Repositories.BaseRepository<>));
builder.Services.AddScoped<IRoomRepository, RestaurantHotelAds.Infrastructure.Repositories.RoomRepository>();
builder.Services.AddScoped<IHotelRepository, RestaurantHotelAds.Infrastructure.Repositories.HotelRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantHotelAds.Infrastructure.Repositories.RestaurantRepository>();
builder.Services.AddScoped<IAdvertisementRepository, RestaurantHotelAds.Infrastructure.Repositories.AdvertisementRepository>();
builder.Services.AddScoped<IRoomAdvertisementRepository, RestaurantHotelAds.Infrastructure.Repositories.RoomAdvertisementRepository>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",    // hotel-portal
                "http://localhost:4201",    // restaurant-portal
                "http://localhost:4202"     // room-display
            )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
}
);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// DATABASE INITIALIZATION & SEEDING
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Apply migrations automatically
        logger.LogInformation("Applying database migrations...");
        context.Database.Migrate();

        // Seed data
        logger.LogInformation("Seeding database...");
        await DatabaseSeeder.SeedAsync(context, logger);

        logger.LogInformation("Database initialization completed successfully!");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database.");
        // Don't throw in production - let app start even if seeding fails
        if (app.Environment.IsDevelopment())
        {
            throw;
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "../frontend/hotel-restaurant-portal";
//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
//    }
//});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine($"Connected to: {context.Database.GetDbConnection().Database}");
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
