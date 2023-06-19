using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using System.Diagnostics;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly TrackRepository _trackRepository;
        private readonly IMapper _mapper;

        public TracksController(IMapper mapper, TrackRepository trackRepository)
        {
            _mapper = mapper;
            _trackRepository = trackRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackWithAlbumDto>> GetTrack(int id)
        {
            var track = await _trackRepository.GetById(id);
            if (track == null)
                return NotFound();

            var trackDto = _mapper.Map<TrackWithAlbumDto>(track);

            var followers = await _trackRepository.GetFollowersThroughTrack(track);

            var artists = trackDto.Artists.ToList();
            for (int i = 0; i < artists.Count; i++)
            {
                artists[i].Followers.Total = followers[i];
            }

            trackDto.Artists = artists;

            return Ok(trackDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrack(TrackDto trackDto)
        {
            var track = _mapper.Map<Track>(trackDto);
            await _trackRepository.Create(track);
            await _trackRepository.SaveChanges();

            return Ok(track.TrackId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrack(TrackDto trackDto)
        {
            var track = _mapper.Map<Track>(trackDto);
            await _trackRepository.Update(track);
            await _trackRepository.SaveChanges();

            return Ok(track.TrackId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var track = await _trackRepository.GetById(id);
            _trackRepository.Delete(track);
            await _trackRepository.SaveChanges();
            return Ok(track.TrackId);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TrackWithAlbumDto>>> GetSeveralTracks(List<int> ids)
        //{
            
        //}

    }
}
