using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;

namespace Modsenfy.DataAccessLayer.Repositories;

public class AlbumRepostiory : IRepository<Album>
{
    private readonly DatabaseContext _databaseContext;
    public AlbumRepostiory(DatabaseContext databaseContext)
	{
        this._databaseContext = databaseContext;
    }
	public Task Create(Album album)
	{
		return _databaseContext.AddAsync(album).AsTask();
	}


	public void DeleteById(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Album>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<Album> GetById(int id)
	{
		throw new NotImplementedException();
	}

	public Task SaveChanges()
	{
		return  _databaseContext.SaveChangesAsync();
	}

	public Task Update(Album entity)
	{
		throw new NotImplementedException();
	}
}