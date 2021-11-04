using Microsoft.EntityFrameworkCore;
using Property.ApplicationCore.Entities;

namespace Property.Infrastructure.Data.Context
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
