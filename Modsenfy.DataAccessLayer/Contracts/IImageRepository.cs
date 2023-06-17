using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IImageRepository:IRepository<Image>
{
    async Task IRepository<Image>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<Image> IRepository<Image>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Image>> IRepository<Image>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Image>.Create(Image entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Image>.Update(Image entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<Image>.Delete(Image entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Image> CreateAndGet(Image entity)
    {
        throw new NotImplementedException();
    }
}