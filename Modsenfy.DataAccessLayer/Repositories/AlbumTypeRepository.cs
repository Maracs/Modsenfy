using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class AlbumTypeRepository : IAlbumTypeRepository
{

	private readonly DatabaseContext _databaseContext;
	public AlbumTypeRepository(DatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}
	
	public Task CreateAsync(AlbumType entity)
	{
		throw new NotImplementedException();
	}

	public void DeleteAsync(AlbumType entity)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<AlbumType>> GetAllAsync()
	{
		var types = await _databaseContext.AlbumTypes.ToListAsync();
		return types;
	}

	public async Task<AlbumType> GetByIdAsync(int id)
	{
		var type = await _databaseContext.AlbumTypes.FindAsync(id);
		return type;
	}

	public async Task<AlbumType> GetByName(string typeName)
	{
		var type = await _databaseContext.AlbumTypes.FirstOrDefaultAsync(at => at.AlbumTypeName.ToLower() == typeName.ToLower());
        return type;
    }

	public Task SaveChangesAsync()
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(AlbumType entity)
	{
		throw new NotImplementedException();
	}
}