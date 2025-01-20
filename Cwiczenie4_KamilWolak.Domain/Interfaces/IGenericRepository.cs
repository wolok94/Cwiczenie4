using System.Linq.Expressions;

namespace Cwiczenie4_KamilWolak.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression);
    Task AddRange(IEnumerable<T> items);
}