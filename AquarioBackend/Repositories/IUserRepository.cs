using AquarioBackend.Models;

namespace AquarioBackend.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task<User> CreateAsync(User user);

    }
}
