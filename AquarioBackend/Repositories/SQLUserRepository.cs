using AquarioBackend.Data;
using AquarioBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AquarioBackend.Repositories
{
    public class SQLUserRepository : IUserRepository
    {

        private readonly AquarioBackendDbContext dbContext;
        public SQLUserRepository(AquarioBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;   
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await dbContext.Users.ToListAsync();

            return users;

        }

        public async Task<User> GetByIdAsync(int id)
        {
            var users = await dbContext.Users.Include(x => x.ForumThread).FirstOrDefaultAsync(x => x.UserId == id);

            return users;
           
        }
    }
}
