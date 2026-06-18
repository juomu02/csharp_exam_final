
using Microsoft.EntityFrameworkCore;
using App.Entities;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(o =>
            {
                o.HasKey(e => e.Id);
                o.HasIndex(e => e.UserName).IsUnique();
                o.HasIndex(e => e.Email).IsUnique();
                o.Property(e => e.PasswordHash).IsRequired();
                o.Property(e => e.Role).IsRequired();
            });

            modelBuilder.Entity<UserTask>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.Title).IsRequired();
                o.Property(e => e.Description).IsRequired();
                o.Property(e => e.IsCompleted).HasDefaultValue(false);
                o.Property(e => e.Importance).IsRequired();
                o.Property(e => e.StartDate);
                o.Property(e => e.EndDate);
                o.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                o.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                o.HasOne<User>().WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}