using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}