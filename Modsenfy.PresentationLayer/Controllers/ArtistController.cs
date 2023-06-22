using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artist = await _artistRepository.GetByIdWithJoins(id);
            var artistDto = _mapper.Map<ArtistDto>(artist);

            return Ok(artistDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateArtist(ArtistDto artistDto)
        {
            var artist = new Artist();

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetSeveralArtists(List<int> ids)
        {
            var artists = await _artistRepository.GetSeveralArtists(ids);
            List<ArtistDto> artistDtos = new List<ArtistDto>();

            foreach (var artist in artists)
            {
                artistDtos.Add(_mapper.Map<ArtistDto>(artist));
            }
            
            return Ok(artistDtos);
        }

        [HttpGet("{id}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetArtistAlbums(int id)
        {
            var albums = await _artistRepository.GetArtistAlbums(id);
            List<AlbumDto> albumDtos = new List<AlbumDto>();

            foreach (var album in albums)
            {
                albumDtos.Add(_mapper.Map<AlbumDto>(album));
            }

            return Ok(albumDtos);
        }

        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetArtistTracks(int id)
        {
            var tracks = await _artistRepository.GetArtistTracks(id);
            List<TrackDto> trackDtos = new List<TrackDto>();

            foreach (var track in tracks)
            {
                trackDtos.Add(_mapper.Map<TrackDto>(track));
            }

            return Ok(trackDtos);
        }

        [HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams(int id)
		{
            var streams = await _artistRepository.GetArtistStreams(id);
            List<StreamDto> streamDtos = new List<StreamDto>();

            foreach (var stream in streams)
            {
                streamDtos.Add(_mapper.Map<StreamDto>(stream));
            }

            return Ok(streamDtos);
		}
    }
}