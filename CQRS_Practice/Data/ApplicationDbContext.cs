using CQRS_Practice.Model;
using Microsoft.EntityFrameworkCore;

namespace CQRS_Practice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
