using DriveEase.API.Middleware;
using DriveEase.Application;
using DriveEase.Infrastructure;
using DriveEase.Persistance;
using DriveEase.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
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

app.UseFastEndpoints()
    .UseSwaggerGen();
app.UseHttpsRedirection();

app.UseCors("DefaultAnyOriginPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();