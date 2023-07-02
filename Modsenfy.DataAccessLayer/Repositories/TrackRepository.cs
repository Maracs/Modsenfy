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

		public async Task<Track> CreateAndGet(Track entity)
		{
			var trackEntry = await _databaseContext.Tracks.AddAsync(entity);
            await SaveChangesAsync();
            return trackEntry.Entity;
		}

		public void DeleteAsync(Track entity)
		{
			_databaseContext.Remove(entity);
			return;
		}

		public async Task<IEnumerable<Track>> GetAllAsync()
		{
			return await _databaseContext.Tracks.ToListAsync();
		}

		public async Task<Track> GetByIdAsync(int id)
		{
			var track = await _databaseContext.Tracks.FindAsync(id);
			return track;
		}
		
		public async Task<Track> GetByIdWithJoins(int id)
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
				 .FirstOrDefaultAsync(t => t.TrackId  == id);
			return track;
		}

		public async Task<Track> GetByIdWithStreams(int id)
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

		public async Task<IEnumerable<Track>> GetSeverlTracks(List<int> ids)
		{
			List<Track> tracks = new List<Track>();
			foreach (var id in ids)
			{
				tracks.Add(await GetByIdWithJoins(id));
			}
			return tracks;
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
		}
	}
}
