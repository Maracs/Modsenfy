using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;

namespace Modsenfy.PresentationLayer.Controllers;



[Route("me")]
[ApiController]
public class CurrentUserController:ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetUserProfile()
    {
        return Ok();
    }

    [HttpGet("top/artists")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetUserTopArtists()
    {
        return Ok();
    }

    [HttpGet("top/tracks")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetUserTopTracks()
    {
        return Ok();
    }

    [HttpGet("albums")]
    public async Task<ActionResult<IEnumerable<AlbumDto>>> GetUserSavedAlbums([FromQuery] int limit,
        [FromQuery] int offset)
    {
        return Ok();
    }

    [HttpGet("albums/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedAlbums([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpPut("albums")]
    public async Task<ActionResult> SaveAlbumsForUser([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpDelete("albums")]
    public async Task<ActionResult> DeleteUserSavedAlbums([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpGet("playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylists([FromQuery] int limit,
        [FromQuery] int offset)
    {
        return Ok();
    }

    [HttpGet("tracks")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetUserTracks([FromQuery] int limit, [FromQuery] int offset)
    {
        return Ok();
    }

    [HttpPut("tracks")]
    public async Task<ActionResult> SaveTracksForUser([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpDelete("tracks")]
    public async Task<ActionResult> DeleteUserSavedTracks([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpGet("tracks/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedTracks([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpGet("following")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetFollowedArtists([FromQuery] int limit,
        [FromQuery] int offset)
    {
        return Ok();
    }

    [HttpPut("following")]
    public async Task<ActionResult> FollowArtists([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpDelete("following")]
    public async Task<ActionResult> UnfollowArtists([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpGet("following/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserFollowsArtists([FromQuery] string ids)
    {
        return Ok();
    }

    [HttpPost("become-artist")]
    public async Task<ActionResult> CreateRequest([FromBody] RequestDto requestDto)
    {
        return Ok();
    }

    [HttpGet("become-artist/{id}")]
    public async Task<ActionResult> GetRequest(int id)
    {
        return Ok();
    }

    [HttpGet("become-artist")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequests([FromQuery] int limit,
        [FromQuery] int offset,[FromQuery] string status)
    {
        return Ok();
    }

    [HttpGet("streams")]
    public async Task<ActionResult<IEnumerable<StreamDto>>> GetUserStreamHistory([FromQuery] int limit,
        [FromQuery] int offset)
    {
        return Ok();
    }
}