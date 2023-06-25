using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
	
	public async Task<Album> CreateAndGet(Album entity)
	{
		var albumEntry = await _databaseContext.Albums.AddAsync(entity);

		await _databaseContext.SaveChangesAsync();
		return albumEntry.Entity;
	}

	public void Delete(Album album)
	{
		_databaseContext.Remove(album);
		_databaseContext.SaveChanges();
	}

	public async Task<IEnumerable<Album>> GetAll()
	{
		var albums = await GetWithJoins()
			.ToListAsync();

		return albums;
	}

	public async Task<Album> GetById(int id)
	{
		var album = await _databaseContext.Albums.FindAsync(id);
		return album;
	}

	public IIncludableQueryable<Album, ImageType> GetWithJoins()
	{
        IIncludableQueryable<Album, ImageType> albums = _databaseContext.Albums
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
                        .ThenInclude(ar => ar.Image)
                            .ThenInclude(ar => ar.ImageType);
        return albums;
	}

	public async Task<Album> GetByIdWithJoins(int id)
	{
		var album = await GetWithJoins()
			.FirstOrDefaultAsync(a => a.AlbumId == id);
			

		return album;
	}

	public async Task<IEnumerable<Album>> GetLimited(int limit, int offset)
	{
		var albums = await GetWithJoins()
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return albums;
	}

	public async Task<IEnumerable<Album>> GetOrderedByRelease()
	{
		var albums = await GetWithJoins()
			.OrderByDescending(a => a.AlbumRelease)
			.ToListAsync();
		return albums;
	}
	
	public async Task<IEnumerable<Album>> GetOrderedByReleaseAndSkipped(int offset)
	{
		var albums = await GetWithJoins()
			.OrderByDescending(a => a.AlbumRelease)
			.Skip(offset)
			.ToListAsync();
		return albums;
	}
	public async Task<IEnumerable<Album>> GetOrderedByReleaseAndLimited(int limit, int offset)
	{
		var albums = await GetWithJoins()
			.OrderByDescending(a => a.AlbumRelease)
			.Skip(offset)
			.Take(limit).ToListAsync();
		return albums;
	}

	public async Task<IEnumerable<Album>> GetSkipped(int offset)
	{
		var albums = await GetWithJoins()
			.Skip(offset)
			.ToListAsync();

		return albums;
	}

	public async Task SaveChanges()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public Task Update(Album album)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Entities.Stream>> GetAlbumStreams(int id)
	{
		var streams = await _databaseContext.Streams
			.Include(s => s.User)
			.Include(s => s.Track)
			.Where(s => s.Track.AlbumId == id)
			.OrderByDescending(s => s.StreamDate)
			.ToListAsync();
		return streams;

	}

	Task<Image> IAlbumRepository.CreateAndGet(Album entity)
	{
		throw new NotImplementedException();
	}
}