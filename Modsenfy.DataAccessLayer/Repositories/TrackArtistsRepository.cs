using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;


public class TrackArtistsRepository : ITrackArtistsRepository
{
	private readonly DatabaseContext _databaseContext;
	public TrackArtistsRepository(DatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}
	public async Task Create(TrackArtists entity)
	{
		await _databaseContext.TrackArtists.AddAsync(entity);
		await SaveChanges();
	}

	public void Delete(TrackArtists entity)
	{
		_databaseContext.Remove(entity);
		_databaseContext.SaveChanges();
	}

	public Task<IEnumerable<TrackArtists>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<TrackArtists> GetById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChanges()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public async Task Update(TrackArtists entity)
	{
		_databaseContext.Update(entity);
        await SaveChanges();
    }
}