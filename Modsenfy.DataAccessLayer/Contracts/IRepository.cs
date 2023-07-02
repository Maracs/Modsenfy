using Microsoft.EntityFrameworkCore.Storage;
using Modsenfy.DataAccessLayer.Data;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IRepository <T> where T : class
{
    Task SaveChangesAsync();

    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    void DeleteAsync(T entity);
}