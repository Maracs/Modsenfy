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
	
	public Task Create(AlbumType entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(AlbumType entity)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<AlbumType>> GetAll()
	{
		var types = await _databaseContext.AlbumTypes.ToListAsync();
		return types;
	}

	public async Task<AlbumType> GetById(int id)
	{
		var type = await _databaseContext.AlbumTypes.FindAsync(id);
		return type;
	}

	public async Task<AlbumType> GetByName(string typeName)
	{
		var type = await _databaseContext.AlbumTypes.FirstOrDefaultAsync(at => at.AlbumTypeName.ToLower() == typeName.ToLower());
        return type;
    }

	public Task SaveChanges()
	{
		throw new NotImplementedException();
	}

	public Task Update(AlbumType entity)
	{
		throw new NotImplementedException();
	}
}