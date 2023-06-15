namespace Modsenfy.DataAccessLayer.Contracts;

public interface IRepository <T> where T : class
{
    Task SaveChanges();

    Task<T> GetById(int id);

    Task<IEnumerable<T>> GetAll();

    Task Create(T entity);

    Task Update(T entity);
    
    void DeleteById(int id);
}
