using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IGenreRepository : IRepository<Genre>
{
	Task<Genre> GetByName(string genreName);
	
	
}