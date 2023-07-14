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
		_databaseContext.Audios.Remove(entity);
	}

	public Task<IEnumerable<Audio>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<Audio> GetByIdAsync(int id)
	{
		return await _databaseContext.Audios.FindAsync(id);
	}

	public async Task SaveChangesAsync()
	{
		await _databaseContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Audio entity)
	{
		_databaseContext.Audios.Update(entity);
		await _databaseContext.SaveChangesAsync();
	}
}