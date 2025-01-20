using System.Linq.Expressions;
using Cwiczenie4_KamilWolak.Domain.Interfaces;
using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie4_KamilWolak.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class 
{
    private readonly CurrencyDbContext _dbContext;

    public GenericRepository(CurrencyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> expression)
    {
        var items = await _dbContext.Set<T>().Where(expression).ToListAsync();
        return items;
    }

    public async Task AddRange(IEnumerable<T> items)
    {
        await _dbContext.Set<T>().AddRangeAsync(items);
        await _dbContext.SaveChangesAsync();
    }
    
}