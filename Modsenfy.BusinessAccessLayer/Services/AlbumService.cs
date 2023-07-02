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
	
	public async Task<IEnumerable<StreamDto>> GetAlbumStreams(int id)
	{
		var albumStreams = await _albumRepository.GetAlbumStreams(id);
		var streamDtos = albumStreams.Select(s => _mapper.Map<StreamDto>(s));
		return streamDtos;
	}

	public async Task<int> CreateAlbum(AlbumCreateDto albumDto)
	{
		//artistOwnerId из клейма !TODO;
		int artistOwnerId = 1;
		
		foreach(var trackDto in albumDto.Tracks)
		{
			foreach(var artistId in trackDto.Artists)
			{
				var res = await _artistRepository.Exists(artistId);
				if (!res)
					throw new Exception("artists not found");
			}
		}
		
		var albumType = await _albumTypeRepository.GetByName(albumDto.AlbumTypeName);
		var image = new Image()
		{
			ImageFilename = albumDto.Image.ImageFilename,
			ImageTypeId = (await _imageTypeRepository.GetIfExists(albumDto.Image.ImageTypeName)).ImageTypeId
		};
		

		//artistOwnerId из клейма !TODO;
		int artistOwnerId = 1;
		var artistOwner = await _artistRepository.GetByIdAsync(artistOwnerId);
	

		var album = new Album()
		{
			AlbumName = albumDto.AlbumName,
			AlbumTypeId = albumType.AlbumTypeId,
			CoverId = (await _imageRepository.CreateAndGet(image)).ImageId,
			AlbumOwnerId = artistOwnerId,
			Artist = artistOwner,
			AlbumRelease = DateTime.Now,	
		};
		
		

		album = await _albumRepository.CreateAndGet(album);
		
		foreach (var trackDto in albumDto.Tracks)
		{
			await _trackService.CreateTrack(trackDto, album.AlbumId, artistOwnerId);
		}
		

		return album.AlbumId;
	}

	public async Task UpdateAlbum(int id, AlbumUpdateDto albumDto)
	{
		//artistOwnerId из клейма !TODO;
		int artistOwnerId = 1;

		var album = await _albumRepository.GetById(id);
		if (album == null)
			throw new Exception("n");
		if (album.AlbumOwnerId != artistOwnerId)
			throw new Exception("n");
			
		foreach(var trackDto in albumDto.AddTracks)
		{
			foreach(var artistId in trackDto.Artists)
			{
				var res = await _artistRepository.Exists(artistId);
				if (!res)
					throw new Exception("artists not found");
			}
		}
			
		var albumType = await _albumTypeRepository.GetByName(albumDto.AlbumTypeName);
		var image = new Image()
		{
			ImageFilename = albumDto.Image.ImageFilename,
			ImageTypeId = (await _imageTypeRepository.GetIfExists(albumDto.Image.ImageTypeName)).ImageTypeId
		};
		
		var artistOwner = await _artistRepository.GetById(artistOwnerId);
		
		
		album.AlbumName = albumDto.AlbumName;
		album.CoverId = (await _imageRepository.CreateAndGet(image)).ImageId;

		foreach (var deleteTrackId in albumDto.DeleteTracks)
		{
			var deleteTrack = await _trackRepository.GetById(deleteTrackId);
			if (deleteTrack == null)
				throw new Exception("n");
			_trackRepository.Delete(deleteTrack);
			await _trackRepository.SaveChanges();
		}

		//_imageRepository.Delete(album.Image);

		await _albumRepository.Update(album);
		
		foreach (var trackDto in albumDto.AddTracks)
		{
			await _trackService.CreateTrack(trackDto, id, artistOwnerId);
		}

	}
}