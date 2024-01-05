using DriveEase.SharedKernel;

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

//options pattern
builder.Services.Configure<ApplicationConfig>(
    builder.Configuration.GetSection(nameof(ApplicationConfig)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

//builder.Register();

var app = builder.Build();

//add global exception middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "DriveEase API");
        //o.OAuthClientId(configuration["SwaggerClientAd:ClientId"]);
    });
}

app.UseHttpsRedirection();

app.UseCors("DefaultAnyOriginPolicy");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();


app.Run();