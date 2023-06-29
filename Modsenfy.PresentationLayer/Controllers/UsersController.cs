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

    private readonly TokenService _tokenService;

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
        var userToken = await _userService.SignInUser(userDto);
      
        if (userToken.UserToken == "None") { return Unauthorized(); }
        
        return Ok(userToken);
    }

    [HttpPost]
    public async Task<ActionResult<UserTokenDto>> RegisterUser([FromBody] UserWithDetailsAndEmailAndPasshashDto userDto)
    {
        if (await _userRepository.IfNicknameExists(userDto.Nickname) || await _userRepository.IfEmailExists(userDto.Email))
            return BadRequest("This nickname or email is already taken");

        var userRegDto = await _userService.RegisterUser(userDto);
        return Ok(userRegDto);
    }

    [Authorize(Roles = "User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfile([FromRoute]int id)
    {
        var user = await _userRepository.GetByIdWithJoins(id);

        var userDto = _mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(user);
        
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id,[FromBody] UserWithDetailsAndEmailDto userDto)
    {

        if (!await _userService.UpdateUser(id, userDto))
            return BadRequest("Incorrect image type");
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        if (await _userRepository.GetById(id) == null)
            return BadRequest("User with this Id does not exists");
        
        await _userService.DeleteUser(id);
        
        return Ok();
    }

    [HttpGet("{id}/playlists")]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetUserPlaylists([FromRoute] int id, [FromQuery] int limit,
        [FromQuery] int offset)
    {
        if (await _userRepository.GetById(id) == null)
            return BadRequest("User with this Id does not exists");

        if (limit < 0 )
            return BadRequest("Invalid limit value");
        
        if (offset<0)
            return BadRequest("Invalid offset value");
        
        var playlists = await _userService.GetUserPlaylists(id, limit, offset);
        
        return Ok(playlists);
    }

    [HttpPost("{id}/playlists")]
    public async Task<ActionResult> CreateUserPlaylist([FromRoute] int id, [FromBody] PlaylistDto playlistDto)
    {
        
        return Ok();
    }

    [HttpGet("requests")]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetSeveralRequests([FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] string status)
    {
        var requests = await _userService.GetSeveralRequests(limit, offset, status);
       
        return Ok(requests);
    }

    [HttpGet("requests/{id}")]
    public async Task<ActionResult<RequestDto>> GetRequest([FromRoute] int id)
    {

        var request = await _userService.GetRequest(id);
        
        return Ok(request);
    }

    [HttpPost("requests/{id}/managing")]
    public async Task<ActionResult> AnswerRequest([FromRoute] int id,[FromBody] string answer)
    {
        await _userService.AnswerRequest(id, answer);
        return Ok();
    }
}
