using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IAudioRepository: IRepository<Audio>
{
    Task<Audio> CreateAndGetAsync(Audio entity);
}