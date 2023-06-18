using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;
using Newtonsoft.Json;

namespace Modsenfy.BusinessAccessLayer.Services;

public class AlbumService
{
	private readonly AlbumRepository _albumRepository;
	private readonly TrackRepository _trackRepository;
	private readonly IMapper _mapper;
	public AlbumService(AlbumRepository albumRepository, TrackRepository trackRepository, IMapper mapper)
	{
		_albumRepository = albumRepository;
		_trackRepository = trackRepository;
		_mapper = mapper;
	}
	
	private int CountTotalStreams (Album album)
	{
		var total = 0;
		foreach (Track track in album.Tracks)
			total += track.TrackStreams;
		return total;
	} 
	public async Task<AlbumWithTracksDto> GetAlbum(int id)
	{
		var album = await _albumRepository.GetByIdWithJoins(id);
		var albumDto = _mapper.Map<AlbumWithTracksDto>(album);
		albumDto.AlbumStreams = CountTotalStreams(album);
		return albumDto;
	}
	
	public async Task<IEnumerable<TrackDto>> GetTracksOfAlbum(int id)
	{
		var album = await _albumRepository.GetByIdWithJoins(id);
		var tracks = album.Tracks;
		IEnumerable<TrackDto> trackDtos = new List<TrackDto>();
		foreach (Track track in tracks)
		{
			trackDtos = trackDtos.Append(_mapper.Map<TrackDto>(track));
		}
        return trackDtos;
	}
	
}