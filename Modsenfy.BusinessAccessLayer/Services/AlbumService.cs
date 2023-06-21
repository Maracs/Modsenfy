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
	
	public async Task<IEnumerable<TrackDto>?> GetTracksOfAlbum(int id)
	{
		var album = await _albumRepository.GetByIdWithJoins(id);
		Console.WriteLine(JsonConvert.SerializeObject(album));
		if (album == null)
			return null;
		var tracks = album.Tracks;
		IEnumerable<TrackDto> trackDtos = new List<TrackDto>();
		foreach (Track track in tracks)
		{
			trackDtos = trackDtos.Append(_mapper.Map<TrackDto>(track));
		}
		return trackDtos;
	}
	
	public async Task<IEnumerable<AlbumWithTracksDto>> GetNewAlbumReleases(int limit, int offset)
	{
		IEnumerable<Album> albums;
		if (limit == -1 && offset == 0)
			albums = await _albumRepository.GetOrderedByRelease();
		else if (limit == -1)
			albums = await _albumRepository.GetOrderedByReleaseAndSkipped(offset);
		else 
			albums = await _albumRepository.GetOrderedByReleaseAndLimited(limit, offset);
		
		
		IEnumerable<AlbumWithTracksDto> albumDtos = new List<AlbumWithTracksDto>();
		foreach (Album album in albums)
		{
			var albumDto = _mapper.Map<AlbumWithTracksDto>(album);
			albumDto.AlbumStreams = CountTotalStreams(album);
			albumDtos = albumDtos.Append(albumDto);
		}
		return albumDtos;
	}
	
	public async Task<IEnumerable<AlbumWithTracksDto>> GetSeveralAlbums(string ids, int limit, int offset)
	{
		IEnumerable<Album> albums;
		IEnumerable<int> intIds;
		if (ids.Equals("all"))
		{
			if (limit == -1 && offset == 0)
				albums = await _albumRepository.GetAll();
			else if (limit == -1)
				albums = await _albumRepository.GetSkipped(offset);
			else
				albums = await _albumRepository.GetLimited(limit, offset);
		}
		else
		{
			var splittedIds = ids.Split(',');
			intIds = splittedIds.Select(id => int.Parse(id));
			albums = new List<Album>();
			foreach (var id in intIds)			
			{
				var album = await _albumRepository.GetByIdWithJoins(id);
				albums = albums.Append(album);
			}
		}


		IEnumerable<AlbumWithTracksDto> albumDtos = new List<AlbumWithTracksDto>();
		foreach (Album album in albums)
		{
			var albumDto = _mapper.Map<AlbumWithTracksDto>(album);
			albumDto.AlbumStreams = CountTotalStreams(album);
			albumDtos = albumDtos.Append(albumDto);
		}
		return albumDtos;
	}
	
	public async Task GetAlbumStreams(int id)
	{
		var albumStreams = await _albumRepository.GetAlbumStreams(id);
		var streamDtos = albumStreams.Select(s => _mapper.Map<StreamDto>(s));
        Console.WriteLine(JsonConvert.SerializeObject(streamDtos, Formatting.Indented));
    }
	
}