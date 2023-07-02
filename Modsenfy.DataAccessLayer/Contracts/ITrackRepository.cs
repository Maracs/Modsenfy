using Modsenfy.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsenfy.DataAccessLayer.Contracts
{
	internal interface ITrackRepository : IRepository<Track>
	{
	
		Task<IEnumerable<Track>> GetSeverlTracks(List<int> ids);

		Task<Track> GetByIdWithJoins(int id);

		Task<Track> GetByIdWithStreams(int id);

        Task<Track> CreateAndGet(Track entity);
    }
}
