using AquarioBackend.Data;
using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AquarioBackend.Repositories
{


    public class SQLForumThreadRepository : IForumThreadRepository
    {
        private readonly AquarioBackendDbContext dbContext;
        

        public SQLForumThreadRepository(AquarioBackendDbContext dbContext) {
          
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            
        }

        public async Task<ForumThread> CreateAsync(ForumThread thread)
        {
            thread.TimeCreated = DateTime.Now;

            await dbContext.Threads.AddAsync(thread);
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
                       Replies = x.Reply.Select(x => new ReplyDTO()
                       {
                           Id = x.Id,
                           UserId = x.UserId,
                           Content = x.Content,
                           ForumThreadId = x.ForumThreadId

                       }).ToList(),
                   }
                   ).FirstAsync();

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
    }
}
