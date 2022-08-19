using System.Linq.Expressions;

namespace ReenbitChatAppBlazorServer.DB.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class 
{
    Task<TEntity?> Get(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predecate);
    void Add(TEntity item);
    void Remove(TEntity item); 
}