using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DatabaseContext _databaseContext;

        public TrackRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreateAsync(Track entity)
        {
            await _databaseContext.Tracks.AddAsync(entity);
        }

        public async Task<Track> CreateAndGetAsync(Track entity)
        {
            var trackEntry = await _databaseContext.Tracks.AddAsync(entity);
            await SaveChangesAsync();
            return trackEntry.Entity;
        }

        public void Delete(Track entity)
        {
            _databaseContext.Remove(entity);
            return;
        }

        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            var tracks = await _databaseContext.Tracks
                .Include(t => t.Audio)
                .Include(t => t.Genre)
                .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                .Include(t => t.Album)
                    .ThenInclude(al => al.Image)
                        .ThenInclude(i => i.ImageType)
                .Include(t => t.Album)
                    .ThenInclude(a => a.AlbumType)
                .ToListAsync();
            return tracks;
        }

        public async Task<Track> GetByIdAsync(int id)
        {
            var track = await _databaseContext.Tracks.FindAsync(id);
            return track;
        }

        public async Task<IEnumerable<Track>> GetByListWithJoinsAsync(IEnumerable<int> ids)
        {
            var tracks = await _databaseContext.Tracks
                 .Include(t => t.Audio)
                 .Include(t => t.Genre)
                 .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                 .Include(t => t.Album)
                    .ThenInclude(al => al.Image)
                        .ThenInclude(i => i.ImageType)
                 .Include(t => t.Album)
                    .ThenInclude(a => a.AlbumType)
                 .Where(t => ids.Contains(t.TrackId)).ToListAsync();
            return tracks;
        }
        
        public async Task<Track> GetByIdWithJoinsAsync(int id)
        {
            var track = await _databaseContext.Tracks
                 .Include(t => t.Audio)
                 .Include(t => t.Genre)
                 .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                 .Include(t => t.Album)
                    .ThenInclude(al => al.Image)
                        .ThenInclude(i => i.ImageType)
                 .Include(t => t.Album)
                    .ThenInclude(a => a.AlbumType)
                 .FirstOrDefaultAsync(t => t.TrackId == id);
            return track;
        }

        public async Task<Track> GetByIdWithStreamsAsync(int id)
        {
            var track = await _databaseContext.Tracks
                .Include(t => t.Audio)
                .Include(t => t.Genre)
                .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                .Include(t => t.Streams)
                    .ThenInclude(st => st.User)
                        .ThenInclude(u => u.UserInfo)
                .FirstOrDefaultAsync(t => t.TrackId == id);
            return track;
        }

        public async Task<List<int>> GetFollowersThroughTrackAsync(Track track)
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

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Track entity)
        {
            var track = await _databaseContext.Tracks.FindAsync(entity.TrackId);

            track.TrackArtists = entity.TrackArtists;
            track.TrackDuration = entity.TrackDuration;
            track.Audio = entity.Audio;
            track.Genre = entity.Genre;
            track.TrackName = entity.TrackName;
            track.TrackGenius = entity.TrackGenius;

            await SaveChangesAsync();
        }

        public async Task<bool> IsTrackOwnerAsync(int artistId, int trackId)
        {
            if (await _databaseContext.TrackArtists.SingleOrDefaultAsync
                (x => x.TrackId == trackId && x.ArtistId == artistId) == null)
            { return false; }

            return true;
        }

        public IIncludableQueryable<Track, AlbumType> GetWithJoins()
        {
            IIncludableQueryable<Track, AlbumType> tracks = _databaseContext.Tracks
                .Include(t => t.Audio)
                .Include(t => t.Genre)
                .Include(t => t.TrackArtists)
                    .ThenInclude(ta => ta.Artist)
                        .ThenInclude(a => a.Image)
                            .ThenInclude(i => i.ImageType)
                .Include(t => t.Album)
                    .ThenInclude(al => al.Image)
                        .ThenInclude(i => i.ImageType)
                .Include(t => t.Album)
                    .ThenInclude(a => a.AlbumType);
            return tracks;
        }

        public async Task<IEnumerable<Track>> GetSkippedAsync(int offset)
        {
            var tracks = await GetWithJoins()
                .Skip(offset)
                .ToListAsync();

            return tracks;
        }

        public async Task<IEnumerable<Track>> GetLimitedAsync(int limit, int offset)
        {
            var tracks = await GetWithJoins()
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return tracks;
        }
    }
}
