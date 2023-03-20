global using FileSystemObserver_Api.Services;
global using FileSystemObserver_Api.ViewModels;
global using Microsoft.AspNetCore.Mvc;
global using FileSystemObserver_Api.Handlers;
global using Microsoft.OpenApi.Models;
global using System.Reflection;
global using NLog.Web;
global using NLog;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "File System Obsrver Web API",
            Description = "Symple Web Api for viewing file system in some path.",
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    builder.Configuration.AddJsonFile("config.json");

    builder.Services.AddTransient<IFileSystemService, FileSystemService>();

    builder.Logging.ClearProviders();

    builder.Host.UseNLog();

    builder.Services.AddLogging(logBuilder =>
    {
        logBuilder.AddConsole();
        logBuilder.AddDebug();
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error-development");
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");

    throw;
}
finally
{
    LogManager.Shutdown();
}
