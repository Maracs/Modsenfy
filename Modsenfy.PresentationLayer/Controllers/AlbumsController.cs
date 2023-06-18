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
		private readonly IMapper _mapper;
		private readonly AlbumService _albumService;

		public AlbumsController(AlbumRepository albumRepository, IMapper mapper, AlbumService albumService)
		{
			_albumRepository = albumRepository;
			_mapper = mapper;
			_albumService = albumService;
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<AlbumWithTracksDto>> GetAlbum([FromRoute]int id){
			var albumDto = await _albumService.GetAlbum(id);
			return Ok(albumDto);
		}

		[HttpPost]
		public async Task<ActionResult<int>> CreateAlbum([FromBody] AlbumCreateDto albumCreateDto){
			Console.WriteLine(JsonConvert.SerializeObject(albumCreateDto));

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumCreateDto albumCreateDto){
			Console.WriteLine(id);
			Console.WriteLine(JsonConvert.SerializeObject(albumCreateDto));
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAlbum([FromRoute] int id){

			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetSeveralAlbums([FromQuery] string ids){
			
			return Ok();
		}

		[HttpGet("{id}/tracks")]
		public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksOfAlbum([FromRoute] int id){
			var trackDtos = await _albumService.GetTracksOfAlbum(id);
			return Ok(trackDtos);
		}

		[HttpGet("{id}/new-releases")]
		public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetNewAlbumReleases([FromRoute] int id, [FromQuery] int limit, [FromQuery] int offset){

			return Ok();
		}

		[HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams([FromRoute] int id, [FromQuery] int limit, [FromQuery] int offset){
			Console.WriteLine(limit);
			Console.WriteLine(offset);
			return Ok();
		}
	}
}