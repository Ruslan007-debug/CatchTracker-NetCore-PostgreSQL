using CatchTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CatchTrackerApi.Data
{
    public class FishingDbContext : DbContext
    {
        // Конструктор, який приймає налаштування
        public FishingDbContext(DbContextOptions<FishingDbContext> options)
            : base(options)
        {
        }

        // DbSet - це колекції (таблиці) в базі даних
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<FishType> FishTypes { get; set; }
        public DbSet<FishingLog> FishingLogs { get; set; }

        // Метод для налаштування моделі даних
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування таблиці User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Назва таблиці в БД (малими літерами для PostgreSQL)
                entity.HasKey(e => e.Id); // Первинний ключ

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.Name)
                    .IsUnique();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .HasDefaultValue("User");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP"); // для PostgreSQL

                // Налаштування зв'язку: 1 User -> багато FishingLogs
                entity.HasMany(u => u.FishingLogs)
                    .WithOne(fl => fl.User)
                    .HasForeignKey(fl => fl.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // При видаленні User видаляються всі його логи
            });

            // Налаштування таблиці Place
            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("places");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BiggestTrophy)
                    .HasMaxLength(200);

                entity.Property(e => e.WaterTemp)
                    .HasColumnType("decimal(5,2)"); // Тип даних з точністю

                // Зв'язок: 1 Place -> багато FishingLogs
                entity.HasMany(p => p.FishingLogs)
                    .WithOne(fl => fl.Place)
                    .HasForeignKey(fl => fl.PlaceId)
                    .OnDelete(DeleteBehavior.Restrict); // Заборона видалення якщо є пов'язані логи
            });

            // Налаштування таблиці FishType
            modelBuilder.Entity<FishType>(entity =>
            {
                entity.ToTable("fish_types");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FavBait)
                    .HasMaxLength(100);

                entity.Property(e => e.AvgWeight)
                    .HasColumnType("decimal(8,2)");

                // Зв'язок: 1 FishType -> багато FishingLogs
                entity.HasMany(ft => ft.FishingLogs)
                    .WithOne(fl => fl.FishType)
                    .HasForeignKey(fl => fl.FishTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Налаштування таблиці FishingLog
            modelBuilder.Entity<FishingLog>(entity =>
            {
                entity.ToTable("fishing_logs");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Trophy)
                    .HasMaxLength(200);

                entity.Property(e => e.Bait)
                    .HasMaxLength(100);

                entity.Property(e => e.Distance)
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Time)
                    .IsRequired();

                // Зовнішні ключі вже налаштовані вище через HasMany/WithOne
            });
        }
    }
}