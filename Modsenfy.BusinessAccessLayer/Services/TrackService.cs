using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class TrackService
{
	private readonly TrackRepository _trackRepository;
	private readonly GenreRepository _genreRepository;
	private readonly ArtistRepository _artistRepository;
	private readonly AudioRepository _audioRepository;
	private readonly TrackArtistsRepository _trackArtistsRepository;
	private readonly IMapper _mapper;

	public TrackService(
		TrackRepository trackRepository, 
		GenreRepository genreRepository,
		ArtistRepository artistRepository, 
		TrackArtistsRepository trackArtistsRepository,
		AudioRepository audioRepository,
		IMapper mapper)
	{
		_trackRepository = trackRepository;
		_genreRepository = genreRepository;
		_artistRepository = artistRepository;
		_trackArtistsRepository = trackArtistsRepository;
        _audioRepository = audioRepository;
        _mapper = mapper;
	}

	public async Task CreateTrack(TrackCreateDto trackDto, int albumId, int artistOwnerId)
	{
		var genre = await _genreRepository.GetByName(trackDto.GenreName);
		var audio = new Audio()
		{
			AudioFilename = trackDto.Audio.AudioFilename
		};

        var addedAudio = await _audioRepository.CreateAndGetAsync(audio);

        var track = new Track()
		{
			TrackName = trackDto.TrackName,
			TrackStreams = 0,
			TrackDuration = DateTime.Parse("0:" + trackDto.TrackDuration),
			TrackGenius = trackDto.TrackGenius,
			AudioId = addedAudio.AudioId,
			GenreId = genre.GenreId,
			AlbumId = albumId
		};


		var addedTrack = await _trackRepository.CreateAndGet(track);
		trackDto.Artists.Append(artistOwnerId);
		foreach (var artistId in trackDto.Artists)
		{
			var artist = await _artistRepository.GetByIdAsync(artistId);
			if (artist == null)
				throw new Exception("artist not found");

			var trackArtists = new TrackArtists
			{
				TrackId = addedTrack.TrackId,
				ArtistId = artistId
			};

      await _trackArtistsRepository.CreateAsync(trackArtists);
		}
	}
}