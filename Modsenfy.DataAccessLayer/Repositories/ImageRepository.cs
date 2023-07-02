using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class ImageRepository:IImageRepository
{
    private readonly DatabaseContext _databaseContext;

    public ImageRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Image> CreateAndGet(Image entity)
    {
        var imageEntry = await _databaseContext.Images.AddAsync(entity);

        await _databaseContext.SaveChangesAsync();
        return imageEntry.Entity;
    }

    public async Task SaveChangesAsync()
    {
       await _databaseContext.SaveChangesAsync();
    }

    public async Task<Image> GetByIdAsync(int id)
    {
        var image = await _databaseContext.Images.FindAsync(id);

        return image;
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        var images = await _databaseContext.Images.ToListAsync();

        return images;
    }

    public async Task CreateAsync(Image entity)
    {
        await _databaseContext.Images.AddAsync(entity);
    }

    public async Task UpdateAsync(Image entity)
    {
        _databaseContext.Images.Update(entity);
    }

    public void DeleteAsync(Image entity)
    {
        _databaseContext.Images.Remove(entity);
    }

   
}