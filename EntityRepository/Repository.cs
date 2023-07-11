using EntityRepository.Context;
using EntityRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityRepository;

public class RepositoryTest : IRepositoryTest
{
    private readonly ApplicationDbContext _context;

    public RepositoryTest(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>?> Get<T>(T item) where T : class
    {
        List<T> items = await _context.Set<T>().ToListAsync();

        return items is not null ? items : null;
    }

    public async Task<bool> Create<T>(T item) where T : class
    {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();

        return true;
    }
}
