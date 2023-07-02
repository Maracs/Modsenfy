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
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfileAsyncAsync()
    {

         var id = 1; // Заглушка для id
        
        var user = await _userRepository.GetByIdWithJoinsAsync(id);
        var userDto = _mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(user);
        
        return Ok(userDto);
    }

    [HttpGet("top/artists")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetUserTopArtistsAsyncAsync()
    {
        var id = 1; // Заглушка для id

        var artists = await _userService.GetUserTopArtistsAsync(id);
        
        return Ok(artists);
    }

    [HttpGet("top/tracks")]
    public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTopTracksAsync()
    {
        var id = 1;

        var tracks = await _userService.GetUserTopTracksAsync(id);
        
        return Ok(tracks);
    }

    [HttpGet("albums")]
    public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetUserSavedAlbumsAsync([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;
        var albums = await _userService.GetUserSavedAlbumsAsync(id, limit, offset);
        
        return Ok(albums);
    }

    [HttpGet("albums/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedAlbumsAsync([FromQuery] string ids)
    {
        var id = 1;
        var userFollowAlbums  = await _userService.CheckUserSavedAlbumsAsync(id, ids);
        
        return Ok(userFollowAlbums);
    }

    [HttpPut("albums")]
    public async Task<ActionResult> SaveAlbumsForUserAsync([FromQuery] string ids)
    {
        var id = 1;

       await _userService.SaveAlbumsForUserAsync(id, ids);
        
        return Ok();
    }

    [HttpDelete("albums")]
    public async Task<ActionResult> DeleteUserSavedAlbumsAsync([FromQuery] string ids)
    {
        var id = 1;

       await _userService.DeleteUserSavedAlbumsAsync(id, ids);
        
        return Ok();
    }

    [HttpGet("playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylistsAsync([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;
        
        if (limit < -1 )
            return BadRequest("Invalid limit value");
        
        if (offset<0)
            return BadRequest("Invalid offset value");
        
        var playlists = await _userService.GetUserSavedPlaylistsAsync(id, limit, offset);
        
        return Ok(playlists);
    }

    [HttpGet("tracks")]
    public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTracksAsync([FromQuery] int limit, [FromQuery] int offset)
    {
        var id = 1;
        var tracks =await _userService.GetUserTracksAsync(id, limit, offset);
        
        return Ok(tracks);
    }

    [HttpPut("tracks")]
    public async Task<ActionResult> SaveTracksForUserAsync([FromQuery] string ids)
    {

        var id = 1;

        await _userService.SaveTracksForUserAsync(id,ids);
        
        return Ok();
    }

    [HttpDelete("tracks")]
    public async Task<ActionResult> DeleteUserSavedTracksAsync([FromQuery] string ids)
    {
        var id = 1;

        await _userService.DeleteUserSavedTracksAsync(id, ids);
        
        return Ok();
    }

    [HttpGet("tracks/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedTracksAsync([FromQuery] string ids)
    {

        var id = 1;
        
        var userFollowTracks  = await _userService.CheckUserSavedTracksAsync(id, ids);
        
        return Ok(userFollowTracks);
    }

    [HttpGet("following")]
    public async Task<ActionResult<IEnumerable<ArtistDto>>> GetFollowedArtistsAsync([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1; // Заглушка для id

        var artists = await _userService.GetFollowedArtistsAsync(id, limit, offset);
        
        
        return Ok(artists);
    }

    [HttpPut("following")]
    public async Task<ActionResult> FollowArtistsAsync([FromQuery] string ids)
    {
        var id = 1; // Заглушка для id
        await _userService.FollowArtistsAsync( id,ids);
        return Ok();
    }

    [HttpDelete("following")]
    public async Task<ActionResult> UnfollowArtistsAsync([FromQuery] string ids)
    {
        var id = 1; // Заглушка для id
        await _userService.UnfollowArtistsAsync( id,ids);
        return Ok();
    }

    [HttpGet("following/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckUserFollowsArtistsAsync([FromQuery] string ids)
    {

        var id = 1; // Заглушка для id
        
        var followings = await _userService.CheckUserFollowsArtistsAsync( id,  ids);
        
        return Ok(followings);
    }

    [HttpPost("become-artist")]
    public async Task<ActionResult> CreateRequestAsync([FromBody] RequestWithoutIdAndTimeDto requestDto)
    {
        var id = 1;
        
        await _userService.CreateRequestAsync(id,requestDto);
        return Ok();
    }

    [HttpGet("become-artist/{id}")]
    public async Task<ActionResult> GetRequestAsync(int id)
    {
        var userId = 1; // Заглушка для id

        var request = await _userService.GetUserRequestAsync(userId, id);
        
        return Ok(request);
    }

    [HttpGet("become-artist")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequestsAsync([FromQuery] int limit,
        [FromQuery] int offset,[FromQuery] string status)
    {

        var id = 1; // Заглушка для id
        
        var requestDtos = await _userService.GetSeveralUserRequestsAsync(id, limit,offset , status);
        
        return Ok(requestDtos);
    }

    [HttpGet("streams")]
    public async Task<ActionResult<UserStreamDto>> GetUserStreamHistoryAsync([FromQuery] int limit,
        [FromQuery] int offset)
    {
        var id = 1;

        var streams = await _userService.GetUserStreamHistoryAsync(id, limit, offset);
        
        return Ok(streams);
    }

    [HttpPut("playlists/{playlistId}/followers")]
    public async Task FollowPlaylistAsync(int playlistId)
    {
        var id = 1;

       await _userService.FollowPlaylistAsync(id, playlistId);

    }

    [HttpDelete("playlists/{playlistId}/followers")]
    public async Task UnfollowPlaylistAsync(int playlistId)
    {
        var id = 1;

        await _userService.UnfollowPlaylistAsync(id, playlistId);
    }

   

}