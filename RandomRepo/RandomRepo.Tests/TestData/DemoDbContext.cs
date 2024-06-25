using Microsoft.EntityFrameworkCore;

namespace RandomRepo.Tests.TestData
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(nameof(User.Id));
                builder.Property(u => u.Username).HasMaxLength(100);
                builder.HasIndex(u => u.Username).IsUnique();
            });
        }
    }
}
