using Microsoft.EntityFrameworkCore.ChangeTracking;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IAlbumRepository : IRepository<Album>
{
    
}