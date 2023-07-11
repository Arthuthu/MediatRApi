using EntityRepository.Models;

namespace EntityRepository.Repository
{
    public interface IUserRepository
    {
        Task<List<User>?> Get();
        Task<User?> Get(Guid id);
        Task<User> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(Guid id);
    }
}