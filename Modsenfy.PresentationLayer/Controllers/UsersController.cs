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
using System.Security.Cryptography;
using System.Text;

namespace Modsenfy.PresentationLayer.Controllers;


[Route("[controller]")]

[ApiController]
public class UsersController:ControllerBase
{
    private readonly UserRepository _userRepository;

    private readonly IMapper _mapper;

    private readonly UserService _userService;
    
    public UsersController(IMapper mapper, UserRepository userRepository, UserService userService)
    {
        _userRepository = userRepository;

        _userService = userService;

        _mapper = mapper;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<UserTokenDto>> SignInUser(UserSigningDto userDto)
    {
        var userToken = await _userService.SignInUserAsync(userDto);
      
        if (userToken.UserToken == "None") { return Unauthorized(); }
        
        return Ok(userToken);
    }

    [HttpPost]
    public async Task<ActionResult<int>> RegisterUserAsync([FromBody] UserWithDetailsAndEmailAndPasshashDto userDto)
    {
        if (await _userRepository.IfNicknameExistsAsync(userDto.Nickname) || await _userRepository.IfEmailExistsAsync(userDto.Email))
            return BadRequest("This nickname or email is already taken");

        var userRegDto = await _userService.RegisterUserAsync(userDto);
        return Ok(userRegDto);
    }

    [Authorize(Roles = "User,Admin,Artist")]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfileAsync([FromRoute]int id)
    {
        var user = await _userRepository.GetByIdWithJoinsAsync(id);

        var userDto = _mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(user);
        
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync([FromRoute] int id,[FromBody] UserWithDetailsAndEmailDto userDto)
    {

        if (!await _userService.UpdateUserAsync(id, userDto))
            return BadRequest("Incorrect image type");
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
    {
        if (await _userRepository.GetByIdAsync(id) == null)
            return BadRequest("User with this Id does not exists");
        
        await _userService.DeleteUserAsync(id);
        
        return Ok();
    }

    [HttpGet("{id}/playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylistsAsync([FromRoute] int id, [FromQuery] int limit,
        [FromQuery] int offset)
    {
        if (await _userRepository.GetByIdAsync(id) == null)
            return BadRequest("User with this Id does not exists");

        if (limit < -1 )
            return BadRequest("Invalid limit value");
        
        if (offset<0)
            return BadRequest("Invalid offset value");
        
        var playlists = await _userService.GetUserPlaylistsAsync(id, limit, offset);
        
        return Ok(playlists);
    }

    [HttpPost("{id}/playlists")]
    public async Task<ActionResult<int>> CreateUserPlaylistAsync([FromRoute] int id, [FromBody] PlaylistWithNameAndImage playlistDto)
    {
        var playlistId = await _userService.CreateUserPlaylistAsync(id, playlistDto);
        
        return Ok(playlistId);
    }

    [HttpGet("playlists/{playlistId}/followers/contains")]
    public async Task<ActionResult<IEnumerable<bool>>> CheckIfUsersFollowUserPlaylistAsync(int playlistId,[FromQuery] string ids)
    {
        var followings =  await _userService.CheckIfUsersFollowPlaylistAsync(playlistId,ids);

        return Ok(followings);
    }
    
    [HttpGet("requests")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequestsAsync([FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] string status)
    {
        var requests = await _userService.GetSeveralRequestsAsync(limit, offset, status);
       
        return Ok(requests);
    }

    [HttpGet("requests/{id}")]
    public async Task<ActionResult<RequestDto>> GetRequestAsync([FromRoute] int id)
    {
        
        var request = await _userService.GetRequestAsync(id);
        
        return Ok(request);
    }

    [HttpPost("requests/{id}/managing")]
    public async Task<ActionResult> AnswerRequestAsync([FromRoute] int id,[FromBody] string answer)
    {
        await _userService.AnswerRequestAsync(id, answer);
        return Ok();
    }
}
