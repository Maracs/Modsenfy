using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IImageTypeRepository:IRepository<ImageType>
{
    async Task IRepository<ImageType>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<ImageType> IRepository<ImageType>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<ImageType>> IRepository<ImageType>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<ImageType>.Create(ImageType entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<ImageType>.Update(ImageType entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<ImageType>.Delete(ImageType entity)
    {
        throw new NotImplementedException();
    }
}