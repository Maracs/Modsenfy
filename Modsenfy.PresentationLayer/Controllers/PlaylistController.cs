using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;

        public PlaylistController(IMapper mapper, PlaylistRepository playlistRepository)
        {
            _mapper = mapper;
            _playlistRepository = playlistRepository;
        }
    }
}