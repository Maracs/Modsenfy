using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly DatabaseContext _databaseContext;
        
        public ArtistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Create(Artist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public Task<IEnumerable<Artist>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Artist> GetById(int id)
        {
            var artist = await _databaseContext.Artists.FindAsync(id);
            return artist;
        }

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Artist entity)
        {
            var artist = await _databaseContext.Artists.FindAsync(entity.ArtistId);

            artist.ArtistName = entity.ArtistName;
            artist.ArtistBio = entity.ArtistBio;
            artist.Image = entity.Image;
            artist.ImageId = entity.ImageId;
        }

        public async void Delete(Artist entity) 
        {
            throw new NotImplementedException(); //
        }
    }
}