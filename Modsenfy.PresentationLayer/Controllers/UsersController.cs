using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.BusinessAccessLayer.DTOs.UserDtos;
using Modsenfy.BusinessAccessLayer.Extentions;
using Modsenfy.BusinessAccessLayer.Services;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modsenfy.PresentationLayer.Controllers;


[Route("[controller]")]

[ApiController]
public class UsersController:ControllerBase
{
    private readonly IMapper _mapper;

    private readonly UserService _userService;
    
    public UsersController(IMapper mapper, UserService userService)
    {
        _userService = userService;

        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult<UserTokenDto>> SignInUser(UserSigningDto userDto)
    {
        var userToken = await _userService.SignInUserAsync(userDto);
      
        if (userToken.UserToken == "None") { return Unauthorized(); }
        
        return Ok(userToken);
    } //ready //working

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<int>> RegisterUserAsync([FromBody] UserWithDetailsAndEmailAndPasshashDto userDto)
    {
        var userRegDto = await _userService.RegisterUserAsync(userDto);
        if (userRegDto == null)
            return BadRequest("This nickname or email is already taken");

        return Ok(userRegDto);
    } //ready //working

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfileAsync([FromRoute]int id)
    {
        if (User.HasClaim(ClaimTypes.Role, "Admin"))   
            return Ok(await _userService.GetUserProfileForAdminAsync(id));
        return Ok(await _userService.GetUserProfileForUserAsync(id));
    }
    //ready

    [Authorize(Roles = "User,Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync([FromRoute] int id,[FromBody] UserWithDetailsAndEmailDto userDto)
    {
        if (!(User.HasClaim(ClaimTypes.Role, "Admin") || User.GetUserId() == id))
            return Forbid("Not enough rights");

        if (!await _userService.UpdateUserAsync(id, userDto))
            return BadRequest("Incorrect image type");
        
        return Ok();
    }//ready

    [Authorize(Roles = "User,Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
    {
        if (!(User.HasClaim(ClaimTypes.Role, "Admin") || User.GetUserId() == id))
            return Forbid("Not enough rights");
        
        await _userService.DeleteUserAsync(id);
        
        return Ok();
    }//ready

    [AllowAnonymous]
    [HttpGet("{id}/playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylistsAsync([FromRoute] int id, [FromQuery] int limit,
        [FromQuery] int offset)
    {
        if (limit < -1 )
            return BadRequest("Invalid limit value");
        
        if (offset < 0)
            return BadRequest("Invalid offset value");
        
        var playlists = await _userService.GetUserPlaylistsAsync(id, limit, offset);
        if (playlists == null) return NotFound("User not found");

        return Ok(playlists);
    } //ready //working

    [Authorize(Roles = "User,Admin")]
    [HttpPost("{id}/playlists")]
    public async Task<ActionResult<int>> CreateUserPlaylistAsync([FromRoute] int id, [FromBody] PlaylistWithNameAndImage playlistDto)
    {
        if (!(User.HasClaim(ClaimTypes.Role, "Admin") || User.GetUserId() == id))
            return Forbid();
        
        var playlistId = await _userService.CreateUserPlaylistAsync(id, playlistDto);
        
        return Ok(playlistId);
    }//ready //working

    [Authorize(Roles = "User,Admin")]
    [HttpGet("playlists/{playlistId}/followers/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckIfUsersFollowUserPlaylistAsync(int playlistId,[FromQuery] string ids)
    {
        var followings =  await _userService.CheckIfUsersFollowPlaylistAsync(playlistId,ids);

        return Ok(followings);
    } // не понял прикола этого эндпоинта
    
    [Authorize(Roles = "Admin")]
    [HttpGet("requests")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequestsAsync([FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] string status)
    {
        var requests = await _userService.GetSeveralRequestsAsync(limit, offset, status);
        return Ok(requests);
    }//ready //working

    [Authorize(Roles = "User,Admin")]
    [HttpGet("requests/{id}")]
    public async Task<ActionResult<RequestDto>> GetRequestAsync([FromRoute] int id)
    {
        if (!(User.HasClaim(ClaimTypes.Role, "Admin") || User.GetUserId() == id))
            return Forbid();
       
        var request = await _userService.GetRequestAsync(id);
        
        return Ok(request);
    }//ready //working

    [Authorize(Roles = "Admin")]
    [HttpPost("requests/{id}/managing")]
    public async Task<ActionResult> AnswerRequestAsync([FromRoute] int id,[FromBody] string answer)
    {
        await _userService.AnswerRequestAsync(id, answer);
        return Ok();
    }//ready
}
