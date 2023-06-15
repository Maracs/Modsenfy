using Modsenfy.BusinessAccessLayer.DTOs;

namespace Modsenfy.PresentationLayer.Controllers
{

	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;

	[Route("[controller]")]
	[ApiController]
	public class AlbumsController : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<AlbumWithTracksDto>> GetAlbum([FromRoute]int id){   
			Console.WriteLine(id);
			return Ok();
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

			return Ok();
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