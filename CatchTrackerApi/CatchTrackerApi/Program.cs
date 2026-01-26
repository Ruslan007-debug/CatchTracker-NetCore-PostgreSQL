
using Microsoft.EntityFrameworkCore;
using CatchTrackerApi.Data;
using DotNetEnv;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Repos;
using CatchTrackerApi.Interfaces.ServiceInterfaces;

namespace CatchTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<FishingDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            builder.Services.AddControllers();

            var env = WebApplication.CreateBuilder(args);

            // 1. Завантажуємо змінні з .env файлу
            DotNetEnv.Env.Load();

            // 2. Підставляємо змінні в конфігурацію
            builder.Configuration.AddEnvironmentVariables();

            // Тепер, коли ви будете викликати GetConnectionString, 
            // .NET автоматично шукатиме відповідні значення
            var connectionString = env.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddSingleton<FishingDbContext>();
            builder.Services.AddScoped<IFishTypeRepository, FishTypeRepository>();
            builder.Services.AddScoped<IFishTypeService, IFishTypeService>();

            builder.Services.AddControllers();
            var app = builder.Build();

            
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
