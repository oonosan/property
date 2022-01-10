using Microsoft.EntityFrameworkCore;
using Property.ApplicationCore.Entities;

namespace Property.Infrastructure.Data.Context
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext()
        {

        }

        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=Property;");
            }
        }

        public DbSet<User> Users { get; set; }
    }
}
