using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Extentions;
using Modsenfy.BusinessAccessLayer.Services;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using System.Diagnostics;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Artist")]
    public class TracksController : ControllerBase
    {
        private readonly TrackRepository _trackRepository;
        private readonly IMapper _mapper;
        private readonly TrackService _trackService;

        public TracksController(IMapper mapper, TrackRepository trackRepository, TrackService trackService)
        {
            _mapper = mapper;
            _trackRepository = trackRepository;
            _trackService = trackService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackWithAlbumDto>> GetTrack([FromRoute]int id)
        {
            var track = await _trackRepository.GetByIdWithJoinsAsync(id);
            if (track == null)
                return NotFound();

            var trackDto = _mapper.Map<TrackWithAlbumDto>(track);

            return Ok(trackDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTrack([FromBody]TrackDto trackDto)
        {
            if (!(await _trackRepository.IsTrackOwnerAsync(User.GetUserId(), trackDto.TrackId)))
                return Forbid();

            var track = _mapper.Map<Track>(trackDto);
            await _trackRepository.UpdateAsync(track);

            return Ok(track.TrackId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack([FromQuery]int id)
        {
            if (!(await _trackRepository.IsTrackOwnerAsync(User.GetUserId(), id)))
                return Forbid();

            var track = await _trackRepository.GetByIdAsync(id);
            _trackRepository.Delete(track);
            await _trackRepository.SaveChangesAsync();
            return Ok(track.TrackId);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetSeveralTracks([FromQuery] int limit = -1,
            [FromQuery] int offset = 0, [FromQuery] string ids = "all")
        {
            var tracks = await _trackService.GetSeverlTracksAsync(limit, offset, ids);
            return Ok(tracks);
        }
        
        [HttpGet("{id}/streams")]
        public async Task<ActionResult<TrackWithStreamsDto>> GetTrackStreams(int id)
        {
            if (!(await _trackRepository.IsTrackOwnerAsync(User.GetUserId(), id)))
                return Forbid();

            var track = await _trackRepository.GetByIdWithStreamsAsync(id);
            var trackDto = _mapper.Map<TrackWithStreamsDto>(track);
            return Ok(trackDto);
        }
    }
}
