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
	public async Task CreateAsync(TrackArtists entity)
	{
		await _databaseContext.TrackArtists.AddAsync(entity);
		await SaveChangesAsync();
	}

	public void DeleteAsync(TrackArtists entity)
	{
		_databaseContext.Remove(entity);
		_databaseContext.SaveChanges();
	}

	public Task<IEnumerable<TrackArtists>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<TrackArtists> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChangesAsync()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(TrackArtists entity)
	{
		_databaseContext.Update(entity);
        await SaveChangesAsync();
    }
}