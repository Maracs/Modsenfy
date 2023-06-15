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
    public class TrackRepository : IRepository<Track>
    {
        private readonly DatabaseContext _databaseContext;

        public TrackRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Create(Track entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public void Delete(Track entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Track>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Track> GetById(int id)
        {
            var track = await _databaseContext.Tracks.FindAsync(id);
            return track;
        }
        
        public async Task<Track> GetByIdWithJoins(int id)
        {
            var track = await _databaseContext.Tracks
                 .Include(t => t.Audio)
                 .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                 .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.UserArtists)
                 .Include(t => t.Album)
                    .ThenInclude(al => al.Image)
                        .ThenInclude(i => i.ImageType)
                 .FirstOrDefaultAsync(t => t.TrackId  == id);
            return track;
        }

        public async Task<List<int>> GetFollowersThroughTrack(Track track)
        {
            var artists = await _databaseContext.TrackArtists
                .Where(ta => ta.TrackId == track.TrackId)
                .Select(ta => ta.Artist).ToListAsync();
            
            List<int> followers = new List<int>(artists.Count);
            for (int i = 0; i < artists.Count; i++)
            {
                followers[i] = artists[i].UserArtists.Count;
            }

            return followers;
        }

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(Track entity)
        {
            var track = await _databaseContext.Tracks.FindAsync(entity.TrackId);

            track.TrackArtists = entity.TrackArtists;
            track.TrackDuration = entity.TrackDuration;
            track.Audio = entity.Audio;
            track.Genre = entity.Genre;
            track.TrackName = entity.TrackName;
            track.TrackGenius = entity.TrackGenius;

        }
    }
}
