using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Search;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class SearchService
{
    private readonly AlbumRepository _albumRepository;
    private readonly ArtistRepository _artistRepository;
    private readonly TrackRepository _trackRepository;
    private readonly IMapper _mapper;
    public SearchService(
        AlbumRepository albumRepository,
        ArtistRepository artistRepository,
        TrackRepository trackRepository,
        IMapper mapper
        )
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _trackRepository = trackRepository;
        _mapper = mapper;
    }

    public async Task<SearchDto> Search(string query)
    {
        var albums = await _albumRepository.GetAllAsync();
    
        IEnumerable<Searchable> albumSearchables = new List<Searchable>();
        foreach (var album in albums)
        {
            albumSearchables.Append(new Searchable(album, album.GetType().GetProperty(nameof(Album.AlbumName)), query));
        }
        albumSearchables = albumSearchables.OrderByDescending(a => a.Rate);

        var tracks = await _trackRepository.GetAllAsync();

		IEnumerable<Searchable> trackSearchables = new List<Searchable>();
		
		foreach (var track in tracks)
		{
			trackSearchables.Append(new Searchable(track, track.GetType().GetProperty(nameof(Track.TrackName)), query));
		}
		trackSearchables = trackSearchables.OrderByDescending(t => t.Rate);


		var artists = await _artistRepository.GetAll();

		IEnumerable<Searchable> artistSearchables = new List<Searchable>();
		foreach (var artist in artists)
		{
			artistSearchables.Append(new Searchable(artist, artist.GetType().GetProperty(nameof(Artist.ArtistName)), query));
		}
        artistSearchables = artistSearchables.OrderByDescending(a => a.Rate);

		var searchDto = new SearchDto()
		{
			Albums = albumSearchables.Select(a => _mapper.Map<AlbumDto>((Album)a.SearchObject)),
			Tracks = trackSearchables.Select(t => _mapper.Map<TrackDto>((Track)t.SearchObject)),
			Artists = artistSearchables.Select(a => _mapper.Map<ArtistDto>((Artist)a.SearchObject))
		};
		
		

		return searchDto;
	}
}
