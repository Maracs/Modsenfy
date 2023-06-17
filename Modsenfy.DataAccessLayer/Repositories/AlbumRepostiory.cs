using System.Runtime.CompilerServices;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Modsenfy.DataAccessLayer.Repositories;

public class AlbumRepostiory : IAlbumRepository
{
	private readonly DatabaseContext _databaseContext;
	
	public AlbumRepostiory(DatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}

	public async Task Create(Album album)
	{
		await _databaseContext.AddAsync(album);
	}

	public void Delete(Album album)
	{
		_databaseContext.Remove(album);
	}

	public async Task<IEnumerable<Album>> GetAll()
	{
		return await _databaseContext.Albums.ToListAsync();
	}

	public async Task<Album> GetById(int id)
	{
		var album = await _databaseContext.Albums.FindAsync(id);
        return album;
    }

	public async Task SaveChanges()
	{
        await _databaseContext.SaveChangesAsync();
    }

	public Task Update(Album album)
	{
		throw new NotImplementedException();
	}
}