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
	
	public Task Create(Genre entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(Genre entity)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Genre>> GetAll()
	{
		var genres = await _databaseContext.Genres.ToListAsync();
		return genres;
	}

	public async Task<Genre> GetById(int id)
	{
		var genre = await _databaseContext.Genres.FindAsync(id);
		return genre;
	}

	public async Task<Genre> GetByName(string genreName)
	{
		var genre = await _databaseContext.Genres.FirstOrDefaultAsync(g => g.GenreName.ToLower() == genreName.ToLower());
        return genre;
    }

	public Task SaveChanges()
	{
		throw new NotImplementedException();
	}

	public Task Update(Genre entity)
	{
		throw new NotImplementedException();
	}
}