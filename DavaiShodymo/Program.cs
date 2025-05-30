
using DavaiShodymo.Data;
using DavaiShodymo.EndpointHelper;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Middleware;
using DavaiShodymo.PasswordHasherHelper;
using DavaiShodymo.Users;
using DavaiShodymo.Users.GetProfile;
using DavaiShodymo.Users.Login;
using DavaiShodymo.Users.Register;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DavaiShodymo.EventImagePlacements;
using DavaiShodymo.EventImages;
using DavaiShodymo.EventReviews;
using DavaiShodymo.Events;
using DavaiShodymo.Events.CreateEvent;
using DavaiShodymo.Events.GetEventById;
using DavaiShodymo.Events.GetEventsLibrary;
using DavaiShodymo.Events.UpdateEvent;
using DavaiShodymo.Users.UpdateUser;
using static DavaiShodymo.Users.Register.RegisterHandler;

namespace DavaiShodymo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                            Console.WriteLine($"Full Authorization header: '{authHeader}'");

                            var token = authHeader?.Split(" ").Last();
                            Console.WriteLine($"Extracted token: '{token}'");
                            Console.WriteLine($"Token length: {token?.Length}");

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            Console.WriteLine($"Exception type: {context.Exception.GetType().Name}");
                            if (context.Exception.InnerException != null)
                            {
                                Console.WriteLine($"Inner exception: {context.Exception.InnerException.Message}");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]
                                                   ?? throw new InvalidOperationException()))
                    };
                });

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IEventImagePlacementRepository, EventImagePlacementRepository>();
            builder.Services.AddScoped<IEventImageRepository, EventImageRepository>();
            builder.Services.AddScoped<IEventReviewRepository, EventReviewRepository>();
            builder.Services.AddScoped<IEventService, EventService>();

            builder.Services.AddScoped<RegisterHandler>();
            builder.Services.AddScoped<LoginHandler>();
            builder.Services.AddScoped<GetProfileHandler>();
            builder.Services.AddScoped<UpdateUserHandler>();
            builder.Services.AddScoped<CreateEventHandler>();
            builder.Services.AddScoped<UpdateEventHandler>();
            builder.Services.AddScoped<GetEventByIdHandler>();
            builder.Services.AddScoped<GetEventsLibraryHandler>();

            builder.Services.AddEndpoints();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DockerConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.MapOpenApi();
            }

            app.MapEndpoints();

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            CreateDbIfNotExists(app);

            app.Run();

            void CreateDbIfNotExists(IHost host)
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    DbInitialize.InitializeAsync(context).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
