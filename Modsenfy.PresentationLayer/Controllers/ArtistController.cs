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
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistController(IMapper mapper, ArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            await _artistRepository.Create(artist);
            await _artistRepository.SaveChanges();

            return Ok(artist.ArtistId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            await _artistRepository.Update(artist);
            await _artistRepository.SaveChanges();

            return Ok(artist.ArtistId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _artistRepository.GetById(id);
            _artistRepository.Delete(artist);
            await _artistRepository.SaveChanges();

            return Ok(artist.ArtistId);
        }
    }
}