using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;

namespace AquarioBackend.Repositories
{
    public interface IReplyRepository
    {
        Task<List<Reply>> GetAllAsync();

        Task<ReplyDTO> GetByIdAsync(int id);

        Task<Reply> CreateAsync(Reply reply);

        Task<Reply?> DeleteAsync(int id);
    }
}
