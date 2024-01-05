using Microsoft.Extensions.Options;

namespace DriveEase.API
{
    public static class Startup
    {
        public static void Register(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            var services = builder.Services;
            var configuration = builder.Configuration;

            //services.Configure<ApplicationConfig>(configuration);

            //services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthMiddleware>();

            builder.Host.ConfigureAppConfiguration(options =>
            {
                options.AddJsonFile("appsettings.json", false);
                //options.AddUserSecrets<UserController>(true);
            });

            services.AddControllers();


            services.Add(ServiceDescriptor.Singleton(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));
            services.AddDistributedMemoryCache();
            //services.AddDbContext<IAgiDbContext, AgiDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringNames.AgiDbContext));
            //});


            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).
            //AddJwtBearer(o =>
            //{
            //    o.SaveToken = true;
            //    o.RequireHttpsMetadata = false;
            //    o.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = configuration["JWT:ValidAudience"],
            //        ValidIssuer = configuration["JWT:ValidIssuer"],
            //        ClockSkew = TimeSpan.Zero,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            //    };
            //});



            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            //services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            services.AddSwaggerGen();
        }

        public static void Build(this WebApplication app)
        {

            var services = app.Services;
            var configuration = app.Configuration;

            //using (var scope = services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<AgiDbContext>();
            //    dbContext.Database.Migrate();

            //    if (app.Environment.IsDevelopment())
            //    {
            //        //dbContext.Initializ();
            //    }
            //}


            app.Use(async (context, next) =>
            {
                //context.Response.Headers.Add("X-Content-Type-Options", this.Configuration.GetValue<string>(Core.Constants.XContentTypeOptions));
                //context.Response.Headers.Add("X-Frame-Options", this.Configuration.GetValue<string>(Core.Constants.XFrameOptions));
                //context.Response.Headers.Add("X-Xss-Protection", this.Configuration.GetValue<string>(Core.Constants.XXssProtection));
                //context.Response.Headers.Add("Content-Security-Policy", this.Configuration.GetValue<string>(Core.Constants.ContentSecurityPolicy));
                //if (this.Configuration.GetValue<bool>(Core.Constants.UseStrictTransportSecurity))
                //{
                //    context.Response.Headers.Add("Strict-Transport-Security", this.Configuration.GetValue<string>(Core.Constants.StrictTransportSecurity));
                //}

                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("Server");
                await next().ConfigureAwait(false);
            });

            app.UseHttpsRedirection();
            //app.UseMiddleware<GlobalExceptionHandler>();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("DefaultAnyOriginPolicy");

            app.MapGet("/", () => "Hello from DriveEase API");

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "DriveEase API");
                //o.OAuthClientId(configuration["SwaggerClientAd:ClientId"]);
            });
        }
    }

}
