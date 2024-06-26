using DriveEase.API.Middleware;
using DriveEase.Application;
using DriveEase.Infrastructure;
using DriveEase.Persistance;
using DriveEase.Persistance.EFCustomizations;
using DriveEase.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var config = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true)
    .Build();

// serilog
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

// register services for each layer
builder.Services.RegisterPersistenceServices(config);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices(config);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.
    AddFastEndpoints()
    .SwaggerDocument(x => x.AutoTagPathSegmentIndex = 2);

// options pattern
builder.Services.Configure<ApplicationConfig>(
    builder.Configuration.GetSection(nameof(ApplicationConfig)));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(nameof(JwtSettings)));

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("HealthCheck API", "/healthcheck");
        options.SetEvaluationTimeInSeconds(600);
    }).AddInMemoryStorage();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<DriveEaseDbContext>()
    .AddSqlServer(config?.GetConnectionString(Connectionstring.DriveEaseDbConnectionKey));

builder.Services.Add(ServiceDescriptor.Singleton(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler();

//if (app.Environment.IsDevelopment())
//{
//    app.UseOpenApi();
//    app.UseSwaggerUi();
//}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider
        .GetRequiredService<DriveEaseDbContext>();
    dbContext.Database.Migrate();
}

app.UseFastEndpoints()
    .UseSwaggerGen();
app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors("DefaultAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("healthcheck", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");
app.Run();