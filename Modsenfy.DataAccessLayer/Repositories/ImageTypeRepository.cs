using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class ImageTypeRepository:IImageTypeRepository
{

    private readonly DatabaseContext _databaseContext;

    public ImageTypeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task SaveChanges()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<ImageType> GetById(int id)
    {
        var imageType = await _databaseContext.ImageTypes.FindAsync(id);
        
        return imageType;
    }

    public async Task<IEnumerable<ImageType>> GetAll()
    {
        var imageTypeList = await _databaseContext.ImageTypes.ToListAsync();
        
        return imageTypeList;;
    }

    public async Task Create(ImageType entity)
    {
        await _databaseContext.ImageTypes.AddAsync(entity);
    }

    public async Task Update(ImageType entity)
    {
         _databaseContext.ImageTypes.Update(entity);
    }

    public void Delete(ImageType entity)
    {
        _databaseContext.ImageTypes.Remove(entity);
    }
    
    public async Task<ImageType> GetIfExists(string type)
    {
         if(await _databaseContext.ImageTypes.AnyAsync(x=>x.ImageTypeName == type)==false)
             return null;

         return await _databaseContext.ImageTypes.SingleOrDefaultAsync(x => x.ImageTypeName == type);
    }
    
}