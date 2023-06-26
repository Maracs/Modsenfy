using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.BusinessAccessLayer.Services;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers;



[Route("me")]
[ApiController]
public class CurrentUserController:ControllerBase
{

    private readonly UserService _userService;

    private readonly IMapper _mapper;

    private readonly UserRepository _userRepository;

    public CurrentUserController(UserService userService, IMapper mapper,UserRepository userRepository)
    {
        _userService = userService;

        _mapper = mapper;

        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfile()
    {

         var id = 1; // Заглушка для id
        
        var user = _userRepository.GetByIdWithJoins(id);
        
        return Ok(user);
    }

    [HttpGet("top/artists")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetUserTopArtists()
    {
        var id = 1; // Заглушка для id

        var artists = await _userService.GetUserTopArtists(id);
        
        return Ok(artists);
    }

    [HttpGet("top/tracks")]
    public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTopTracks()
    {
        var id = 1;

        var tracks = await _userService.GetUserTopTracks(id);
        
        return Ok(tracks);
    }

    [HttpGet("albums")]
    public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetUserSavedAlbums([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;
        var albums = await _userService.GetUserSavedAlbums(id, limit, offset);
        
        return Ok(albums);
    }

    [HttpGet("albums/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedAlbums([FromQuery] string ids)
    {
        
        
        var id = 1;
        
        var userFollowAlbums  = await _userService.CheckUserSavedAlbums(id, ids);
        
        return Ok(userFollowAlbums);
        
        
    }

    [HttpPut("albums")]
    public async Task<ActionResult> SaveAlbumsForUser([FromQuery] string ids)
    {
        var id = 1;

       await _userService.SaveAlbumsForUser(id, ids);
        
        return Ok();
    }

    [HttpDelete("albums")]
    public async Task<ActionResult> DeleteUserSavedAlbums([FromQuery] string ids)
    {
        var id = 1;

       await _userService.DeleteUserSavedAlbums(id, ids);
        
        return Ok();
    }

    [HttpGet("playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylists([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;
        
        if (limit < -1 )
            return BadRequest("Invalid limit value");
        
        if (offset<0)
            return BadRequest("Invalid offset value");
        
        var playlists = await _userService.GetUserPlaylists(id, limit, offset);
        
        return Ok(playlists);
    }

    [HttpGet("tracks")]
    public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTracks([FromQuery] int limit, [FromQuery] int offset)
    {
        var id = 1;
        var tracks =await _userService.GetUserTracks(id, limit, offset);
        
        return Ok(tracks);
    }

    [HttpPut("tracks")]
    public async Task<ActionResult> SaveTracksForUser([FromQuery] string ids)
    {

        var id = 1;

        await _userService.SaveTracksForUser(id,ids);
        
        return Ok();
    }

    [HttpDelete("tracks")]
    public async Task<ActionResult> DeleteUserSavedTracks([FromQuery] string ids)
    {
        var id = 1;

        await _userService.DeleteUserSavedTracks(id, ids);
        
        return Ok();
    }

    [HttpGet("tracks/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedTracks([FromQuery] string ids)
    {

        var id = 1;
        
        var userFollowTracks  = await _userService.CheckUserSavedTracks(id, ids);
        
        return Ok(userFollowTracks);
    }

    [HttpGet("following")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetFollowedArtists([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1; // Заглушка для id

        var artists = await _userService.GetFollowedArtists(id, limit, offset);
        
        
        return Ok(artists);
    }

    [HttpPut("following")]
    public async Task<ActionResult> FollowArtists([FromQuery] string ids)
    {
        var id = 1; // Заглушка для id
        await _userService.FollowArtists( id,ids);
        return Ok();
    }

    [HttpDelete("following")]
    public async Task<ActionResult> UnfollowArtists([FromQuery] string ids)
    {
        var id = 1; // Заглушка для id
        await _userService.UnfollowArtists( id,ids);
        return Ok();
    }

    [HttpGet("following/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserFollowsArtists([FromQuery] string ids)
    {

        var id = 1; // Заглушка для id
        
        var followings = await _userService.CheckUserFollowsArtists( id,  ids);
        
        return Ok(followings);
    }

    [HttpPost("become-artist")]
    public async Task<ActionResult> CreateRequest([FromBody] RequestWithoutIdAndTimeDto requestDto)
    {
        var id = 1;
        
        await _userService.CreateRequest(id,requestDto);
        return Ok();
    }

    [HttpGet("become-artist/{id}")]
    public async Task<ActionResult> GetRequest(int id)
    {
        var userId = 1; // Заглушка для id

        var request = _userService.GetUserRequest(userId, id);
        
        return Ok(request);
    }

    [HttpGet("become-artist")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequests([FromQuery] int limit,
        [FromQuery] int offset,[FromQuery] string status)
    {

        var id = 1; // Заглушка для id
        
        var requestDtos = await _userService.GetSeveralUserRequests(id, offset, limit, status);
        
        return Ok(requestDtos);
    }

    [HttpGet("streams")]
    public async Task<ActionResult<IEnumerable<UserStreamDto>>> GetUserStreamHistory([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;

        var streams = await _userService.GetUserStreamHistory(id, limit, offset);
        
        return Ok(streams);
    }
}