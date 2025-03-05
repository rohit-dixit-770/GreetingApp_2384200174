using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Application is starting...");

    var builder = WebApplication.CreateBuilder(args);

    // Retrieve the database connection string
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

    // Configure the application's DbContext to use SQL Server
    builder.Services.AddDbContext<GreetingContext>(options =>
        options.UseSqlServer(connectionString));

    // Add services to the container.
    builder.Services.AddControllers();

    // Register Layer Dependencies
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    // Setup NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Register Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Greeting Application",
            Version = "v1",
            Description = "API for greeting users",
            Contact = new OpenApiContact
            {
                Name = "Rohit Dixit",
                Email = "rohitdixit570@gmail.com",
            }
        });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    logger.Info("Application started successfully");

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application failed to start");
    throw; 
}
finally
{
    logger.Info("Application is shutting down...");
    LogManager.Shutdown(); 
}
