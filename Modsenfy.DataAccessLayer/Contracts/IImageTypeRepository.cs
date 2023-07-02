using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IImageTypeRepository:IRepository<ImageType>
{
    async Task IRepository<ImageType>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<ImageType> IRepository<ImageType>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<ImageType>> IRepository<ImageType>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<ImageType>.CreateAsync(ImageType entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<ImageType>.UpdateAsync(ImageType entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<ImageType>.Delete(ImageType entity)
    {
        throw new NotImplementedException();
    }
}