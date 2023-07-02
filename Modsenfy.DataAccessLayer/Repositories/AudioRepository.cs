using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class AudioRepository : IAudioRepository
{
	private readonly DatabaseContext _databaseContext;

	public AudioRepository(DatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}
	
	public async Task Create(Audio entity)
	{
		await _databaseContext.AddAsync(entity);
	}
	
	public async Task<Audio> CreateAndGet(Audio entity)
	{
		var audioEntry =  await _databaseContext.AddAsync(entity);
		await SaveChanges();
        return audioEntry.Entity;
    }

	public void Delete(Audio entity)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Audio>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<Audio> GetById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChanges()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public Task Update(Audio entity)
	{
		throw new NotImplementedException();
	}
}