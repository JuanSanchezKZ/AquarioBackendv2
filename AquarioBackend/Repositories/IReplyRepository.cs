using AquarioBackend.Models;

namespace AquarioBackend.Repositories
{
    public interface IReplyRepository
    {
        Task<List<Reply>> GetAllAsync();

        Task<Reply> GetByIdAsync(int id);

        Task<Reply> CreateAsync(Reply reply);
    }
}
