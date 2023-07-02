using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IRequestRepository:IRepository<Request>
{
    async Task IRepository<Request>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<Request> IRepository<Request>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Request>> IRepository<Request>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Request>.CreateAsync(Request entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Request>.UpdateAsync(Request entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<Request>.Delete(Request entity)
    {
        throw new NotImplementedException();
    }
}