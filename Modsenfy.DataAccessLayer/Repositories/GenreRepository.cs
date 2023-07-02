using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Modsenfy.DataAccessLayer.Repositories;

public class GenreRepository : IGenreRepository
{
	private readonly DatabaseContext _databaseContext;
	public GenreRepository(DatabaseContext databaseContex)
	{
		_databaseContext = databaseContex;
	}
	
	public Task CreateAsync(Genre entity)
	{
		throw new NotImplementedException();
	}

	public void DeleteAsync(Genre entity)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Genre>> GetAllAsync()
	{
		var genres = await _databaseContext.Genres.ToListAsync();
		return genres;
	}

	public async Task<Genre> GetByIdAsync(int id)
	{
		var genre = await _databaseContext.Genres.FindAsync(id);
		return genre;
	}

	public async Task<Genre> GetByName(string genreName)
	{
		var genre = await _databaseContext.Genres.FirstOrDefaultAsync(g => g.GenreName.ToLower() == genreName.ToLower());
        return genre;
    }

	public Task SaveChangesAsync()
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(Genre entity)
	{
		throw new NotImplementedException();
	}
}