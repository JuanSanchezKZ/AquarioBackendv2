using AquarioBackend.Data;
using AquarioBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AquarioBackend.Repositories
{
    public class SQLReplyRepository : IReplyRepository
    {
        private readonly AquarioBackendDbContext dbContext;
        public SQLReplyRepository(AquarioBackendDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Reply> CreateAsync(Reply reply)
        {

            reply.TimeCreated = DateTime.Now;

            await dbContext.Replies.AddAsync(reply);

            await dbContext.SaveChangesAsync();

            return reply;
            


        }

        public async Task<List<Reply>> GetAllAsync()
        {
            return await dbContext.Replies.Include(x => x.ForumThread).ToListAsync();
        }

        public async Task<Reply> GetByIdAsync(int id)
        {

            return await dbContext.Replies.Include(x => x.ForumThread).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
