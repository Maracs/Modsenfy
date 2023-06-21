using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IRequestRepository:IRepository<Request>
{
    async Task IRepository<Request>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<Request> IRepository<Request>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Request>> IRepository<Request>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Request>.Create(Request entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Request>.Update(Request entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<Request>.Delete(Request entity)
    {
        throw new NotImplementedException();
    }
}