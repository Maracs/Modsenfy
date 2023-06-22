using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly DatabaseContext _databaseContext;
        
        public ArtistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Artist> GetById(int id)
        {
            var artist = await _databaseContext.Artists.FindAsync(id);

            return artist;
        }

        public async Task<Artist> GetByIdWithJoins(int id)
        {
            var artist = await _databaseContext.Artists
                .Include(a => a.Image)
                    .ThenInclude(i => i.ImageType)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Audio)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Genre)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Album)
                            .ThenInclude(al => al.AlbumType)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Album)
                            .ThenInclude(al => al.Image)
                                .ThenInclude(ali => ali.ImageType)
                .Include(a => a.Albums)
                    .ThenInclude(alb => alb.AlbumType)
                .Include(a => a.Albums)
                    .ThenInclude(alb => alb.Image)
                        .ThenInclude(albi => albi.ImageType)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            return artist;
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _databaseContext.Artists.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Create(Artist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task Update(Artist entity)
        {
            var artist = await _databaseContext.Artists.FindAsync(entity.ArtistId);

            artist.ArtistName = entity.ArtistName;
            artist.ArtistBio = entity.ArtistBio;
            artist.Image = entity.Image;
            artist.ImageId = entity.ImageId;
        }

        public void Delete(Artist entity) 
        {
            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();
        }

        public async Task<IEnumerable<Artist>> GetSeveralArtists(List<int> ids)
        {
            List<Artist> artists = new List<Artist>();

            foreach (var id in ids)
            {
                artists.Add(await GetByIdWithJoins(id));
            }

            return artists;
        }

        public async Task<IEnumerable<Album>> GetArtistAlbums(int id)
        {
            var albums = await _databaseContext.Albums
                .Include(a => a.AlbumType)
                .Include(a => a.Image)
                    .ThenInclude(i => i.ImageType)
                .Where(a => a.AlbumOwnerId == id)
                .OrderByDescending(a => a.AlbumRelease)
                .ToListAsync();

            return albums;
        }

        public async Task<IEnumerable<Track>> GetArtistTracks(int id)
        {
            var tracks = await _databaseContext.Tracks
                .Include(t => t.Audio)
                .Include(t => t.Genre)
                .Where(t => t.Album.AlbumOwnerId == id)
                .OrderByDescending(t => t.Streams)
                .ToListAsync();

            return tracks;
        }

        public async Task<IEnumerable<Entities.Stream>> GetArtistStreams(int id)
        {
            var streams = await _databaseContext.Streams
                .Include(s => s.User)
                .Include(s => s.Track)
                .Where(s => s.Track.Album.AlbumOwnerId == id)
                .OrderByDescending(s => s.StreamDate)
                .ToListAsync();

            return streams;
        }
    }
}