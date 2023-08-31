using AquarioBackend.Data;
using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AquarioBackend.Repositories
{
    public class SQLReplyRepository : IReplyRepository
    {
        private readonly AquarioBackendDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public SQLReplyRepository(AquarioBackendDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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

        public async Task<ReplyDTO> GetByIdAsync(int id)
        {

            var replies = await dbContext.Set<Reply>()
                .Where(x => x.Id == id)
                    .Select(x => new ReplyDTO()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        ForumThreadId = x.ForumThreadId,
                        UserId = x.UserId,
                        UserName = x.UserName
                    }).FirstOrDefaultAsync();

            return replies;
        }

        public async Task<Reply?> DeleteAsync(int id)
        {
            var existingReply = await dbContext.Replies.FirstOrDefaultAsync(x => x.Id == id);

            if (existingReply == null)
            {
                return null;
            }

            dbContext.Replies.Remove(existingReply);
            await dbContext.SaveChangesAsync();

            return existingReply;

        }
    }
}
