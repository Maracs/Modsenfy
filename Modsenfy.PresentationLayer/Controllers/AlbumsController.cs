using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.DataAccessLayer.Repositories;
using Newtonsoft.Json;
using Modsenfy.BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Modsenfy.BusinessAccessLayer.Extentions;
using System.Diagnostics.Eventing.Reader;

namespace Modsenfy.PresentationLayer.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AlbumsController : ControllerBase
	{
		private readonly AlbumService _albumService;

		public AlbumsController(AlbumService albumService)
		{
			_albumService = albumService;
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<ActionResult<AlbumWithTracksDto>> GetAlbum([FromRoute]int id)
		{
			var albumDto = await _albumService.GetAlbumAsync(id);
			if (albumDto == null)
				return NotFound();
			return Ok(albumDto);
		}//ready

		[Authorize(Roles = "Artist")]
		[HttpPost]
		public async Task<ActionResult<int>> CreateAlbum([FromBody] AlbumCreateDto albumCreateDto)
		{
            var id = await _albumService.CreateAlbumAsync(albumCreateDto, User.GetUserId());
            return Ok(id);
		}//ready

        [Authorize(Roles = "Artist")]
        [HttpPut("{id}")]
		public async Task<ActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumUpdateDto albumUpdateDto)
		{
			var res = await _albumService.UpdateAlbum(id, albumUpdateDto, User.GetUserId());
			if (res == "Forbid")
				return Forbid();
			else if (res == "NotFound")
				return NotFound();

            return Ok();
		}// надо переделать логику 

        [Authorize(Roles = "Artist")]
        [HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAlbum([FromRoute] int id)
		{
			if (!(await _albumService.DeleteAlbumAsync(User.GetUserId(), id)))
				return Forbid();
			return Ok();
		}//ready

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetSeveralAlbums(
			[FromQuery] int limit = -1, [FromQuery] int offset = 0, [FromQuery] string ids = "all")
		{
			var albumDtos = await _albumService.GetSeveralAlbums(ids, limit, offset);
			return Ok(albumDtos);
		}//ready

		[AllowAnonymous]
		[HttpGet("{id}/tracks")]
		public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksOfAlbum([FromRoute] int id)
		{
			var trackDtos = await _albumService.GetTracksOfAlbum(id);
			return Ok(trackDtos);
		}//ready

		[AllowAnonymous]
		[HttpGet("new-releases")]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetNewAlbumReleases([FromQuery] int limit = -1, [FromQuery] int offset = 0)
		{
			var albumDtos = await _albumService.GetNewAlbumReleases(limit, offset);
			return Ok(albumDtos);
		}// надо посмотреть логику

		[Authorize(Roles = "Artist")]
		[HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams([FromRoute] int id)
		{
			var streams = await _albumService.GetAlbumStreams(id, User.GetUserId());
			if (streams == null) return Forbid();
			return Ok(streams);
		}//ready
	}
}