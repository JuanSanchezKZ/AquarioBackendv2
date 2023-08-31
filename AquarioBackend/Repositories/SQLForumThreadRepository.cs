using AquarioBackend.Data;
using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AquarioBackend.Repositories
{


    public class SQLForumThreadRepository : IForumThreadRepository
    {
        private readonly AquarioBackendDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SQLForumThreadRepository(AquarioBackendDbContext dbContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor) {
          
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [Authorize]
        public async Task<ForumThread> CreateAsync(ForumThread thread)
        {

            thread.TimeCreated = DateTime.Now;

            await dbContext.AddAsync(thread);
            await dbContext.SaveChangesAsync();
            return thread;
                       
        }






        public async Task<List<ForumThread>> GetAllAsync()
        {
            return await dbContext.Threads.Include(x => x.Reply).ToListAsync();
        }
        public async Task<ForumThreadDTO> GetByIdAsync(int id)
        {
            var threads = await dbContext.Set<ForumThread>()
                .Where(x => x.ThreadId == id)
                   .Select(x => new ForumThreadDTO()
                   {
                       ThreadId = x.ThreadId,
                       UserId = x.UserId,
                       Title = x.Title,
                       Content = x.Content,
                       Tag = x.Tag,
                       Replies = x.Reply.Select(x => new ReplyDTO()
                       {
                           Id = x.Id,
                           UserId = x.UserId,
                           Content = x.Content,
                           ForumThreadId = x.ForumThreadId,
                           UserName = x.UserName

                       }).ToList(),
                   }
                   ).FirstOrDefaultAsync();

            return threads;

        }

        public async Task<ForumThread?> UpdateAsync(int id, ForumThread thread)
        {
            var existingThread = await dbContext.Threads.FirstOrDefaultAsync(x => x.ThreadId == id);

            if (existingThread == null) 
            {
                return null;
            
            }

            dbContext.Update(existingThread);

            await dbContext.SaveChangesAsync();

            return existingThread;
        }

        public async Task<ForumThread?> DeleteAsync(int id)
        {
            var existingThread = await dbContext.Threads.FirstOrDefaultAsync(x => x.ThreadId == id);

            if (existingThread == null)
            {
                return null;
            }

            dbContext.Threads.Remove(existingThread);
            await dbContext.SaveChangesAsync();

            return existingThread;

        }
    }


}
