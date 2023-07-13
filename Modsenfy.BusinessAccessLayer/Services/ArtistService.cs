using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;

namespace Modsenfy.BusinessAccessLayer.Services
{
    public class ArtistService
    {
        private readonly ArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(ArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<ArtistDto> GetArtist(int id)
        {
            var artist = await _artistRepository.GetByIdWithJoinsAsync(id);
            var artistDto = _mapper.Map<ArtistDto>(artist);

            return artistDto;
        }

        public async Task<ArtistDto> CreateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            await _artistRepository.CreateAsync(artist);
            await _artistRepository.SaveChangesAsync();

            return artistDto;
        }

        public async Task<ArtistDto> UpdateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            if (artist is null)
            {
                return null;
            }

            await _artistRepository.UpdateAsync(artist);
            await _artistRepository.SaveChangesAsync();

            return artistDto;
        }

        public async Task<ArtistDto> DeleteArtist(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            var artistDto = _mapper.Map<ArtistDto>(artist);

            if (artist is null)
            {
                return null;
            }

            _artistRepository.Delete(artist);
            await _artistRepository.SaveChangesAsync();

            return artistDto;
        }

        public async Task<IEnumerable<ArtistDto>> GetSeveralArtists(List<int> ids)
        {
            var artists = await _artistRepository.GetSeveralArtistsAsync(ids);

            IEnumerable<ArtistDto> artistDtos = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            
            return artistDtos;
        }

        public async Task<IEnumerable<AlbumDto>> GetArtistAlbums(int id)
        {
            var albums = await _artistRepository.GetArtistAlbumsAsync(id);

            IEnumerable<AlbumDto> albumDtos = _mapper.Map<IEnumerable<AlbumDto>>(albums);

            return albumDtos;
        }

        public async Task<IEnumerable<TrackDto>> GetArtistTracks(int id)
        {
            var tracks = await _artistRepository.GetArtistTracksAsync(id);

            IEnumerable<TrackDto> trackDtos = _mapper.Map<IEnumerable<TrackDto>>(tracks);

            return trackDtos;
        }

        public async Task<IEnumerable<StreamDto>> GetArtistStreams(int id)
        {
            var streams = await _artistRepository.GetArtistStreamsAsync(id);

            IEnumerable<StreamDto> streamDtos = _mapper.Map<IEnumerable<StreamDto>>(streams);

            return streamDtos;
        }
    }
}