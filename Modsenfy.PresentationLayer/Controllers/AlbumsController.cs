using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.DataAccessLayer.Repositories;
using Newtonsoft.Json;
using Modsenfy.BusinessAccessLayer.Services;

namespace Modsenfy.PresentationLayer.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AlbumsController : ControllerBase
	{
		private readonly AlbumRepository _albumRepository;
		private readonly AlbumService _albumService;

		public AlbumsController(AlbumRepository albumRepository,AlbumService albumService)
		{
			_albumRepository = albumRepository;
			_albumService = albumService;
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<AlbumWithTracksDto>> GetAlbum([FromRoute]int id)
		{
			var albumDto = await _albumService.GetAlbum(id);
			if (albumDto == null)
				return NotFound();
			return Ok(albumDto);
		}

		[HttpPost]
		public async Task<ActionResult<int>> CreateAlbum([FromBody] AlbumCreateDto albumCreateDto)
		{
            var id = await _albumService.CreateAlbum(albumCreateDto);
            return Ok(id);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumUpdateDto albumUpdateDto)
		{
            await _albumService.UpdateAlbum(id, albumUpdateDto);
            return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAlbum([FromRoute] int id)
		{
			var album = await _albumRepository.GetById(id);
			_albumRepository.Delete(album);
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetSeveralAlbums(
			[FromQuery] int limit = -1, [FromQuery] int offset = 0, [FromQuery] string ids = "all")
		{
			var albumDtos = await _albumService.GetSeveralAlbums(ids, limit, offset);
			return Ok(albumDtos);
		}

		[HttpGet("{id}/tracks")]
		public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksOfAlbum([FromRoute] int id)
		{
			var trackDtos = await _albumService.GetTracksOfAlbum(id);
			return Ok(trackDtos);
			
		}

		[HttpGet("new-releases")]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetNewAlbumReleases([FromQuery] int limit = -1, [FromQuery] int offset = 0)
		{
			var albumDtos = await _albumService.GetNewAlbumReleases(limit, offset);
			return Ok(albumDtos);
		}

		[HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams([FromRoute] int id, [FromQuery] int limit = -1, [FromQuery] int offset = 0)
		{
			await _albumService.GetAlbumStreams(id);
			return Ok();
		}
	}
}