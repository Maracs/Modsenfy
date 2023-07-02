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
	
	public async Task CreateAsync(Audio entity)
	{
		await _databaseContext.AddAsync(entity);
	}
	
	public async Task<Audio> CreateAndGetAsync(Audio entity)
	{
		var audioEntry =  await _databaseContext.AddAsync(entity);
		await SaveChangesAsync();
        return audioEntry.Entity;
    }

	public void Delete(Audio entity)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Audio>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Audio> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task SaveChangesAsync()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public Task UpdateAsync(Audio entity)
	{
		throw new NotImplementedException();
	}
}