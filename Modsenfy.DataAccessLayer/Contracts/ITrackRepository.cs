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
		Task<Track> GetByIdWithJoinsAsync(int id);
		Task<Track> GetByIdWithStreamsAsync(int id);
        Task<Track> CreateAndGetAsync(Track entity);
    }
}
