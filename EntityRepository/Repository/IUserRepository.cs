using EntityRepository.Models;

namespace EntityRepository.Repository
{
    public interface IUserRepository
    {
        Task<List<User>?> Get();
        Task<User?> Get(Guid id);
        Task<User> Create(User user);
        Task<User?> Update(User user);
        Task<User?> Delete(Guid id);
    }
}