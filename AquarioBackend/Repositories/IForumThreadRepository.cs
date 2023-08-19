using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;

namespace AquarioBackend.Repositories
{
    public interface IForumThreadRepository
    {
        Task<List<ForumThread>> GetAllAsync();

        Task<ForumThreadDTO> GetByIdAsync(int id);

        Task<ForumThread> CreateAsync(ForumThread thread);

        Task<ForumThread?> UpdateAsync(int id, ForumThread thread);

    }
}
