using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Modsenfy.DataAccessLayer.Repositories;

public class AlbumRepository : IAlbumRepository
{
	private readonly DatabaseContext _databaseContext;


	public AlbumRepository(DatabaseContext databaseContext)
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

	public async Task<Album> GetByIdWithJoins(int id)
	{
		var album = await _databaseContext.Albums
			.Include(a => a.AlbumType)
			.Include(a => a.Artist)
				.ThenInclude(ar => ar.Image)
					.ThenInclude(i => i.ImageType)
			.Include(a => a.Image)
				.ThenInclude(i => i.ImageType)
			.Include(a => a.Tracks)
				.ThenInclude(t => t.Audio)
			.Include(a => a.Tracks)
				.ThenInclude(t => t.Genre)	
			.Include(a => a.Tracks)
				.ThenInclude(t => t.TrackArtists)
					.ThenInclude(ta => ta.Artist)
			.FirstOrDefaultAsync(a => a.AlbumId == id);
			

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