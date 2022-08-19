using System.Linq.Expressions;
using ReenbitChatAppBlazorServer.DB.Interfaces;

namespace ReenbitChatAppBlazorServer.DB.Repositories;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: class
{
    protected readonly ApplicationContext _context;
    public GenericRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public void Add(TEntity item)
    {
        _context.Set<TEntity>().Add(item);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predecate)
    {
        return _context.Set<TEntity>().Where(predecate);
    }

    public async Task<TEntity?> Get(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public void Remove(TEntity item)
    {
        _context.Set<TEntity>().Remove(item);
    }
}