
using Microsoft.EntityFrameworkCore;
using CatchTrackerApi.Data;
using DotNetEnv;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Repos;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Services;

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

            // 4. Реєструємо репозиторії та сервіси
            builder.Services.AddScoped<IFishTypeRepository, FishTypeRepository>();
            builder.Services.AddScoped<IFishTypeService, FishTypeServise>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}