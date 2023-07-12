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
        private readonly IMapper _mapper;
        private readonly TrackService _trackService;

        public TracksController(IMapper mapper, TrackService trackService)
        {
            _mapper = mapper;
            _trackService = trackService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackWithAlbumDto>> GetTrack([FromRoute]int id)
        {
            var track = await _trackService.GetTrack(id);
            if (track == null)
                return NotFound();

            var trackDto = _mapper.Map<TrackWithAlbumDto>(track);

            return Ok(trackDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTrack([FromBody]TrackDto trackDto)
        {
            var track = await _trackService.UpdateTrackAsync(User.GetUserId(), trackDto);
            if (track == null)
                return Forbid();
            return Ok(track.TrackId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack([FromQuery]int id)
        {
            var track = await _trackService.DeleteTrackAsync(User.GetUserId(), id);
            if (track == null)
                return Forbid();
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
            var trackDto = await _trackService.GetTrackStreamsAsync(User.GetUserId(), id);
            if (trackDto == null)
                return Forbid();

            return Ok(trackDto);
        }
    }
}
