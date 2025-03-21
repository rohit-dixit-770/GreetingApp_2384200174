using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Middleware.Email;
using Middleware.ExceptionHandler;
using Middleware.JWT;
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

    // Add services to the container
    builder.Services.AddControllers();

    // Register Layer Dependencies
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    builder.Services.AddScoped<IUserBL, UserBL>();
    builder.Services.AddScoped<IUserRL, UserRL>();

    // Configure JWT and Email Service
    builder.Services.AddSingleton<JwtHelper>();
    builder.Services.AddScoped<EmailService>();

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

    //  Register CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
    });

    var app = builder.Build();

    app.UseMiddleware<GlobalExceptionMiddleware>(); 

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Enable CORS Before Authorization
    app.UseCors("AllowAll");

    // Configure the HTTP request pipeline
    app.UseRouting();
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
