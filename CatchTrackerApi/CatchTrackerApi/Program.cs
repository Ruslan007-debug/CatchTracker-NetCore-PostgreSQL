using System.Text;
using CatchTrackerApi.Data;
using CatchTrackerApi.Repos;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Services;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CatchTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Завантажуємо змінні з .env
            DotNetEnv.Env.Load();

            // 2. ВРУЧНУ формуємо рядок підключення, витягуючи дані з Environment
            var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                                   $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                   $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                                   $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                                   $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";


            // 3. Реєструємо базу даних
            builder.Services.AddDbContext<FishingDbContext>(options =>
                options.UseNpgsql(connectionString));

            // 4. Налаштовуємо JWT автентифікацію
            builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero, // Без затримки на термін дії

                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)
                )
            };

            // Додаткова логіка (опціонально)
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine($"Token validated for: {context.Principal?.Identity?.Name}");
                    return Task.CompletedTask;
                }
            };
        });

            // 5. Реєструємо репозиторії та сервіси
            builder.Services.AddScoped<IFishTypeRepository, FishTypeRepository>();
            builder.Services.AddScoped<IFishTypeService, FishTypeServise>();
            builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
            builder.Services.AddScoped<IPlaceService, PlaceService>();
            builder.Services.AddScoped<IFishingLogRepository, FishingLogRepository>();
            builder.Services.AddScoped<IFishingLogService, FishingLogService>();
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // 6. CORS (для frontend)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}