using Microsoft.EntityFrameworkCore.Storage;
using Modsenfy.DataAccessLayer.Data;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IRepository <T> where T : class
{
    Task SaveChanges();

    Task<T> GetById(int id);

    Task<IEnumerable<T>> GetAll();

    Task Create(T entity);

    Task Update(T entity);

    void Delete(T entity);
}