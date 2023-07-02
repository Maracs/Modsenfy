using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IImageRepository:IRepository<Image>
{
    async Task IRepository<Image>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<Image> IRepository<Image>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Image>> IRepository<Image>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Image>.CreateAsync(Image entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<Image>.UpdateAsync(Image entity)
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