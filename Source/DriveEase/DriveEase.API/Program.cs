using DriveEase.API.Middleware;
using DriveEase.Application;
using DriveEase.Infrastructure;
using DriveEase.Persistance;
using DriveEase.Persistance.EFCustomizations;
using DriveEase.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var config = builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    //.AddAzureAppConfiguration(x =>
    //{
    //    x.Connect("asd");
    //    x.ConfigureKeyVault();
    //})
    .AddUserSecrets<Program>(optional: true).Build();

// register services for each layer
builder.Services.RegisterPersistenceServices(config);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.
    AddFastEndpoints()
    .SwaggerDocument();


// options pattern
builder.Services.Configure<ApplicationConfig>(
    builder.Configuration.GetSection(nameof(ApplicationConfig)));

builder.Services
    .AddHealthChecksUI(options
    => options
        .AddHealthCheckEndpoint("HealthCheck API", "/healthcheck"))
    .AddInMemoryStorage();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<DriveEaseDbContext>()
    .AddSqlServer(config?.GetConnectionString(Connectionstring.DriveEaseDbConnectionKey));

builder.Services.Add(ServiceDescriptor.Singleton(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI(o =>
    //{
    //    o.SwaggerEndpoint("/swagger/v1/swagger.json", "DriveEase API");
    //o.OAuthClientId(configuration["SwaggerClientAd:ClientId"]);
    //});
}

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

app.UseCors("DefaultAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("healthcheck", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");
app.Run();