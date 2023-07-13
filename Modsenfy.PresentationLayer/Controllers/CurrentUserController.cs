using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.BusinessAccessLayer.Extentions;
using Modsenfy.BusinessAccessLayer.Services;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize(Roles = "User,Artist,Admin")]
    public class CurrentUserController:ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public CurrentUserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfileAsync()
        {
            return Ok(_mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(
                await _userService.GetUserProfileForUserAsync(User.GetUserId())));
        }//ready //����� �� ��������, �� ���� ��������� � ����������� ������� � ��

        [HttpGet("top/artists")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetUserTopArtistsAsyncAsync()
        {
            var artists = await _userService.GetUserTopArtistsAsync(User.GetUserId());
            return Ok(artists);
        }//ready

        [HttpGet("top/tracks")]
        public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTopTracksAsync()
        {
            var tracks = await _userService.GetUserTopTracksAsync(User.GetUserId());
            return Ok(tracks);
        }//ready

        [HttpGet("albums")]
        public async Task<ActionResult<IEnumerable<AlbumWithTracksDto>>> GetUserSavedAlbumsAsync([FromQuery] int limit,
            [FromQuery] int offset)
        {
            var albums = await _userService.GetUserSavedAlbumsAsync(User.GetUserId(), limit, offset);
            return Ok(albums);
        }//ready

        [HttpGet("albums/contains")]
        public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedAlbumsAsync([FromQuery] string ids)
        {
            var userFollowAlbums  = await _userService.CheckUserSavedAlbumsAsync(User.GetUserId(), ids);
            return Ok(userFollowAlbums);
        }//ready

        [HttpPut("albums")]
        public async Task<ActionResult> SaveAlbumsForUserAsync([FromQuery] string ids)
        {
            await _userService.SaveAlbumsForUserAsync(User.GetUserId(), ids);
            return Ok();
        }//ready

        [HttpDelete("albums")]
        public async Task<ActionResult> DeleteUserSavedAlbumsAsync([FromQuery] string ids)
        {
            await _userService.DeleteUserSavedAlbumsAsync(User.GetUserId(), ids);
            return Ok();
        }//ready

        [HttpGet("playlists")]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylistsAsync([FromQuery] int limit,
            [FromQuery] int offset)
        {
            if (limit < -1 )
                return BadRequest("Invalid limit value");
            
            if (offset<0)
                return BadRequest("Invalid offset value");
            
            var playlists = await _userService.GetUserSavedPlaylistsAsync(User.GetUserId(), limit, offset);
            return Ok(playlists);
        }//ready

        [HttpGet("tracks")]
        public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetUserTracksAsync([FromQuery] int limit, [FromQuery] int offset)
        {
            var tracks =await _userService.GetUserTracksAsync(User.GetUserId(), limit, offset);
            return Ok(tracks);
        }//ready

        [HttpPut("tracks")]
        public async Task<ActionResult> SaveTracksForUserAsync([FromQuery] string ids)
        {
            await _userService.SaveTracksForUserAsync(User.GetUserId(),ids);
            return Ok();
        }

        [HttpDelete("tracks")]
        public async Task<ActionResult> DeleteUserSavedTracksAsync([FromQuery] string ids)
        {
            await _userService.DeleteUserSavedTracksAsync(User.GetUserId(), ids);
            return Ok();
        }//ready

        [HttpGet("tracks/contains")]
        public async Task<ActionResult<IEnumerable<bool>>> CheckUserSavedTracksAsync([FromQuery] string ids)
        {
            var userFollowTracks  = await _userService.CheckUserSavedTracksAsync(User.GetUserId(), ids);
            return Ok(userFollowTracks);
        }//ready

        [HttpGet("following")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetFollowedArtistsAsync([FromQuery] int limit,
            [FromQuery] int offset)
        {
            var artists = await _userService.GetFollowedArtistsAsync(User.GetUserId(), limit, offset);
            return Ok(artists);
        }//ready

        [HttpPut("following")]
        public async Task<ActionResult> FollowArtistsAsync([FromQuery] string ids)
        {
            await _userService.FollowArtistsAsync(User.GetUserId(),ids);
            return Ok();
        }//ready

        [HttpDelete("following")]
        public async Task<ActionResult> UnfollowArtistsAsync([FromQuery] string ids)
        {
            await _userService.UnfollowArtistsAsync(User.GetUserId(),ids);
            return Ok();
        }//ready

        [HttpGet("following/contains")]
        public async Task<ActionResult<IEnumerable<bool>>> CheckUserFollowsArtistsAsync([FromQuery] string ids)
        {
            var followings = await _userService.CheckUserFollowsArtistsAsync(User.GetUserId(), ids);
            return Ok(followings);
        }//ready

        [Authorize(Roles = "User")]
        [HttpPost("become-artist")]
        public async Task<ActionResult> CreateRequestAsync([FromBody] RequestWithoutIdAndTimeDto requestDto)
        {
            await _userService.CreateRequestAsync(User.GetUserId(),requestDto);
            return Ok();
        }//ready

        [Authorize(Roles = "User")]
        [HttpGet("become-artist/{id}")]
        public async Task<ActionResult> GetRequestAsync(int id)
        {
            var request = await _userService.GetUserRequestAsync(User.GetUserId(), id);
            return Ok(request);
        }//ready

        [Authorize(Roles = "User")]
        [HttpGet("become-artist")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequestsAsync([FromQuery] int limit,
            [FromQuery] int offset,[FromQuery] string status)
        {
            var requestDtos = await _userService.GetSeveralUserRequestsAsync(User.GetUserId(), limit,offset , status);
            
            return Ok(requestDtos);
        }//ready

        [HttpGet("streams")]
        public async Task<ActionResult<UserStreamDto>> GetUserStreamHistoryAsync([FromQuery] int limit,
            [FromQuery] int offset)
        {
            var streams = await _userService.GetUserStreamHistoryAsync(User.GetUserId(), limit, offset);
            return Ok(streams);
        }//ready

        [HttpPut("playlists/{playlistId}/followers")]
        public async Task FollowPlaylistAsync(int playlistId)
        {
            await _userService.FollowPlaylistAsync(User.GetUserId(), playlistId);
        }//ready

        [HttpDelete("playlists/{playlistId}/followers")]
        public async Task UnfollowPlaylistAsync(int playlistId)
        {
            await _userService.UnfollowPlaylistAsync(User.GetUserId(), playlistId);
        }//ready
    }
}