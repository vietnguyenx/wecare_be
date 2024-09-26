using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wecare.Repositories.Data;
using Wecare.Repositories.Data.Entities;

namespace WeCare.Repositories.Data
{
    public class WeCareDbContext : BaseDbContext
    {
        public WeCareDbContext(DbContextOptions<WeCareDbContext> options) : base(options)
        {
        }

        #region Config 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config.GetConnectionString("WeCare");

            return strConn;
        }
        #endregion

        #region DbSet
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<HealthMetric> HealthMetrics { get; set; } = null!;
        public virtual DbSet<Dietitian> Dietitians { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<DietPlan> DietPlans { get; set; } = null!;
        public virtual DbSet<MenuDish> MenuDishes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<MenuDietPlan> MenuDietPlans { get; set; } = null!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.Username);
                e.Property(x => x.Password);
                e.Property(x => x.FullName).IsRequired();
                e.Property(x => x.ImageUrl);
                e.Property(x => x.Email).IsRequired();
                e.Property(x => x.DOB);
                e.Property(x => x.Address);
                e.Property(x => x.Gender).HasConversion<string>();
                e.Property(x => x.Phone);
                e.Property(x => x.DiseaseType).HasConversion<string>();
                e.Property(x => x.UserType).HasConversion<string>();

                // 1-1
                e.HasOne(x => x.HealthMetric)
                    .WithOne(x => x.User)
                    .HasForeignKey<HealthMetric>(x => x.UserId); 

                // 1-n
                e.HasMany(x => x.DietPlans)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId);

                e.HasMany(x => x.Notifications)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);
            });

            modelBuilder.Entity<HealthMetric>(e =>
            {
                e.ToTable("HealthMetric");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.DateRecorded);
                e.Property(x => x.BloodSugar);
                e.Property(x => x.UricAcid);
                e.Property(x => x.Weight);
                e.Property(x => x.BloodPressure);
                e.Property(x => x.Note);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);
            });

            modelBuilder.Entity<Dietitian>(e =>
            {
                e.ToTable("Dietitian");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.Name);
                e.Property(x => x.Email);
                e.Property(x => x.Phone);
                e.Property(x => x.Specialization);
                e.Property(x => x.ImageUrl);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);

                e.HasMany(x => x.Menus)
                    .WithOne(x => x.Dietitian)
                    .HasForeignKey(x => x.DietitianId);
            });

            modelBuilder.Entity<Menu>(e =>
            {
                e.ToTable("Menu");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.MenuName);
                e.Property(x => x.Description);
                e.Property(x => x.SuitableFor).HasConversion<string>();
                e.Property(x => x.ImageUrl);
                e.Property(x => x.Status).HasConversion<string>();
                e.Property(x => x.IsActive);

                e.Property(x => x.TotalCalories);
                e.Property(x => x.TotalCarbohydrates);
                e.Property(x => x.TotalProtein);
                e.Property(x => x.TotalFat);
                e.Property(x => x.TotalFiber);
                e.Property(x => x.TotalSugar);
                e.Property(x => x.TotalPurine);
                e.Property(x => x.TotalCholesterol);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);

                e.HasMany(x => x.MenuDishes)
                    .WithOne(x => x.Menu)
                    .HasForeignKey(x => x.MenuId);

                e.HasMany(x => x.MenuDietPlans)
                    .WithOne(x => x.Menu)
                    .HasForeignKey(x => x.MenuId);
            });

            modelBuilder.Entity<Dish>(e =>
            {
                e.ToTable("Dish");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.DishName);
                e.Property(x => x.Description);
                e.Property(x => x.Calories);
                e.Property(x => x.Carbohydrates);
                e.Property(x => x.Protein);
                e.Property(x => x.Fat);
                e.Property(x => x.Fiber);
                e.Property(x => x.Sugar);
                e.Property(x => x.Purine);
                e.Property(x => x.Cholesterol);
                e.Property(x => x.ImageUrl);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);

                e.HasMany(x => x.MenuDishes)
                    .WithOne(x => x.Dish)
                    .HasForeignKey(x => x.DishId);
            });

            modelBuilder.Entity<MenuDish>(e =>
            {
                e.ToTable("MenuDish");
                e.HasKey(md => md.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWId()");

                e.HasOne(x => x.Menu)
                    .WithMany(x => x.MenuDishes)
                    .HasForeignKey(x => x.MenuId)
                    .OnDelete(DeleteBehavior.Restrict); // NO ACTION on delete;

                e.HasOne(x => x.Dish)
                    .WithMany(x => x.MenuDishes)
                    .HasForeignKey(x => x.DishId)
                    .OnDelete(DeleteBehavior.Restrict); // NO ACTION on delete;

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);
            });

            modelBuilder.Entity<DietPlan>(e =>
            {
                e.ToTable("DietPlan");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.DateAssigned);
                e.Property(x => x.Period);
                e.Property(x => x.Status).HasConversion<string>();

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);

                e.HasMany(x => x.MenuDietPlans)
                    .WithOne(x => x.DietPlan)
                    .HasForeignKey(x => x.DietPlanId);
            });

            modelBuilder.Entity<MenuDietPlan>(e =>
            {
                e.HasKey(md => md.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWId()");

                e.HasOne(x => x.Menu)
                    .WithMany(x => x.MenuDietPlans)
                    .HasForeignKey(x => x.MenuId)
                    .OnDelete(DeleteBehavior.Restrict); // NO ACTION on delete;

                e.HasOne(x => x.DietPlan)
                    .WithMany(x => x.MenuDietPlans)
                    .HasForeignKey(x => x.DietPlanId)
                    .OnDelete(DeleteBehavior.Restrict); // NO ACTION on delete;

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);
            });

            modelBuilder.Entity<Notification>(e =>
            {
                e.ToTable("Notification");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
                e.Property(x => x.Title);
                e.Property(x => x.Message);
                e.Property(x => x.IsRead);

                e.Property(x => x.CreatedBy);
                e.Property(x => x.CreatedDate);
                e.Property(x => x.LastUpdatedBy);
                e.Property(x => x.LastUpdatedDate);
                e.Property(x => x.IsDeleted);

                e.HasOne(x => x.User)
                    .WithMany(x => x.Notifications)
                    .HasForeignKey(x => x.UserId);
            });
        }
    }
}

