using EntityRepository.Models;

namespace EntityRepository.Repository
{
    public interface IUserRepository
    {
        Task<List<User>?> Get(CancellationToken cancellationToken);
        Task<User?> Get(Guid id, CancellationToken cancellationToken);
        Task<User> Create(User user);
        Task<User?> Update(User user);
        Task<User?> Delete(Guid id);
    }
}