using AquarioBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AquarioBackend.Data
{
    public class AquarioBackendDbContext : DbContext
    {

        public AquarioBackendDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<ForumThread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; } 
        public DbSet<User> Users { get; set; }
    }
}
