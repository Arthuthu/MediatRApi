namespace EntityRepository
{
    public interface IRepositoryTest
    {
        Task<List<T>?> Get<T>(T item) where T : class;
        Task<bool> Create<T>(T item) where T : class;
    }
}