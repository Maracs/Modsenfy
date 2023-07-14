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
	private readonly AlbumTypeRepository _albumTypeRepository;
	private readonly ImageRepository _imageRepository;
	private readonly ImageTypeRepository _imageTypeRepository;
	private readonly TrackService _trackService;
	private readonly ArtistRepository _artistRepository;
	private readonly IMapper _mapper;
	public AlbumService(
		AlbumRepository albumRepository, 
		TrackRepository trackRepository, 
		AlbumTypeRepository albumTypeRepository,
		ImageRepository imageRepository, 
		ImageTypeRepository imageTypeRepository,
		TrackService trackService,
		ArtistRepository artistRepository,
		IMapper mapper)
	{
		_albumRepository = albumRepository;
		_trackRepository = trackRepository;
		_albumTypeRepository = albumTypeRepository;
		_imageRepository = imageRepository;
		_imageTypeRepository = imageTypeRepository;
		_trackService = trackService;
		_artistRepository = artistRepository;
		_mapper = mapper;
	}
	
	private int CountTotalStreams (Album album)
	{
		var total = 0;
		
		foreach (Track track in album.Tracks)
			total += track.TrackStreams;
		
		return total;
	} 
	public async Task<AlbumWithTracksDto> GetAlbumAsync(int id)
	{
		var album = await _albumRepository.GetByIdWithJoins(id);
		var albumDto = _mapper.Map<AlbumWithTracksDto>(album);
		
		albumDto.AlbumStreams = CountTotalStreams(album);
		
		return albumDto;
	}
	
	public async Task<IEnumerable<TrackDto>?> GetTracksOfAlbum(int id)
	{
		var album = await _albumRepository.GetByIdWithJoins(id);
		
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
				albums = await _albumRepository.GetAllAsync();
			else if (limit == -1)
				albums = await _albumRepository.GetSkipped(offset);
			else
				albums = await _albumRepository.GetLimited(limit, offset);
		}
		else
		{
			var splittedIds = ids.Split(',');
			intIds = splittedIds.Select(id => int.Parse(id));
			
			albums = await _albumRepository.GetByListWithJoinsAsync(intIds);
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
	
	public async Task<IEnumerable<StreamDto>> GetAlbumStreams(int id, int artistId)
	{
		if (!(await _albumRepository.IsAlbumOwnerAsync(artistId, id)))
			return null;

		var albumStreams = await _albumRepository.GetAlbumStreams(id);
		var streamDtos = albumStreams.Select(s => _mapper.Map<StreamDto>(s));
		
		return streamDtos;
	}

	public async Task<int> CreateAlbumAsync(AlbumCreateDto albumDto, int artistOwnerId)
	{		
		//foreach(var trackDto in albumDto.Tracks)
		//{
		//	//foreach(var artistId in trackDto.Artists)
		//	//{
		//	//	//var res = await _artistRepository.Exists(artistId);
		//	//	//if (!res)
		//	//		//throw new Exception("artists not found");
		//	//}
		//}
		
		var albumType = await _albumTypeRepository.GetByNameAsync(albumDto.AlbumTypeName);
		var image = new Image()
		{
			ImageFilename = albumDto.Image.ImageFilename,
			ImageTypeId = (await _imageTypeRepository.GetIfExistsAsync(albumDto.Image.ImageTypeName)).ImageTypeId
		};

		var artistOwner = await _artistRepository.GetByIdAsync(artistOwnerId);

		var album = new Album()
		{
			AlbumName = albumDto.AlbumName,
			AlbumTypeId = albumType.AlbumTypeId,
			CoverId = (await _imageRepository.CreateAndGetAsync(image)).ImageId,
			AlbumOwnerId = artistOwnerId,
			Artist = artistOwner,
			AlbumRelease = DateTime.Now,	
		};

		album = await _albumRepository.CreateAndGet(album);
		
		foreach (var trackDto in albumDto.Tracks)
		{
			await _trackService.CreateTrackAsync(trackDto, album.AlbumId, artistOwnerId);
		}

		return album.AlbumId;
	}

	public async Task<bool> DeleteAlbumAsync(int artistId, int albumId)
	{
		if (!(await _albumRepository.IsAlbumOwnerAsync(artistId, albumId)))
			return false;

        var album = await _albumRepository.GetByIdAsync(albumId);
        _albumRepository.Delete(album);
        await _albumRepository.SaveChangesAsync();

		return true;
    }

	public async Task<string> UpdateAlbum(int id, AlbumUpdateDto albumDto, int artistOwnerId)
	{
		if (!(await _albumRepository.IsAlbumOwnerAsync(artistOwnerId, id)))
			return "Forbid";

		var album = await _albumRepository.GetByIdAsync(id);

		if (album == null)
			return "NotFound";
			
		var albumType = await _albumTypeRepository.GetByNameAsync(albumDto.AlbumTypeName);
		var image = new Image()
		{
			ImageFilename = albumDto.Image.ImageFilename,
			ImageTypeId = (await _imageTypeRepository.GetIfExistsAsync(albumDto.Image.ImageTypeName)).ImageTypeId
		};
		
		var artistOwner = await _artistRepository.GetByIdAsync(artistOwnerId);

		album.AlbumName = albumDto.AlbumName;
		album.CoverId = (await _imageRepository.CreateAndGetAsync(image)).ImageId;

		foreach (var deleteTrackId in albumDto.DeleteTracks)
		{
			var deleteTrack = await _trackRepository.GetByIdAsync(deleteTrackId);
			
			if (deleteTrack == null)
				throw new Exception("n");
			
			_trackRepository.Delete(deleteTrack);
			await _trackRepository.SaveChangesAsync();
		}

		//_imageRepository.Delete(album.Image);

		await _albumRepository.UpdateAsync(album);
		
		foreach (var trackDto in albumDto.AddTracks)
		{
			await _trackService.CreateTrackAsync(trackDto, id, artistOwnerId);
		}
		return "";
	}
}