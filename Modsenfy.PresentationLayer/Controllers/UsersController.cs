using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;

namespace Modsenfy.PresentationLayer.Controllers;


[Route("[controller]")]
[ApiController]
public class UsersController:ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserProfile(int id)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id,[FromBody] UserDto userDto)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        return Ok();
    }

    [HttpGet("{id}/playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylists(int id, [FromQuery] int limit,
        [FromQuery] int offset)
    {
        return Ok();
    }

    [HttpPost("{id}/playlists")]
    public async Task<ActionResult> CreateUserPlaylist(int id, [FromBody] PlaylistDto playlistDto)
    {
        return Ok();
    }

    [HttpGet("requests")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequests([FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] string status)
    {
        return Ok();
    }

    [HttpGet("requests/{id}")]
    public async Task<ActionResult> GetRequest(int id)
    {
        return Ok();
    }

    [HttpPost("requests/{id}/managing")]
    public async Task<ActionResult> AnswerRequest([FromBody] string answer)
    {
        return Ok();
    }
    
    
}
