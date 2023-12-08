using Microsoft.EntityFrameworkCore;
using TheApp.Model;

namespace TheApp.SystemDbContext
{
    public class SystemDbContextClass : DbContext
    {
        public DbSet<UserDto> Users { get; init; }

        public SystemDbContextClass(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserDto>();
        }



    }
}
