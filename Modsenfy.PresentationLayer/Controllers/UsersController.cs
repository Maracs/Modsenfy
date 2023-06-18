using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers;


[Route("[controller]")]
[ApiController]
public class UsersController:ControllerBase
{

    private readonly UserRepository _userRepository;

    private readonly UserInfoRepository _userInfoRepository;

    private readonly ImageRepository _imageRepository;

    private readonly ImageTypeRepository _imageTypeRepository;

    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, UserRepository userRepository,
        UserInfoRepository userInfoRepository,ImageRepository imageRepository,ImageTypeRepository imageTypeRepository)
    {
        _userRepository = userRepository;
        
        _userInfoRepository = userInfoRepository;
        
        _imageRepository = imageRepository;

        _imageTypeRepository = imageTypeRepository;

        _mapper = mapper;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<int>> RegisterUser([FromBody] UserWithDetailsAndEmailAndPasshashDto userDto)
    {
        if (await _userRepository.IfNicknameExists(userDto.Nickname) || await _userRepository.IfEmailExists(userDto.Email))
            return BadRequest("This nickname or email is already taken");
        
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithDetailsAndEmailAndIdAndRoleDto>> GetUserProfile([FromRoute]int id)
    {
        var user = await _userRepository.GetByIdWithJoins(id);

        var userDto = _mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(user);
        
        return Ok(userDto);
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
