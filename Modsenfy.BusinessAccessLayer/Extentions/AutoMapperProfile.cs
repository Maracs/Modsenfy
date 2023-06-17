using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Modsenfy.BusinessAccessLayer.Extentions;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Image, ImageDto>()
            .ForMember(dest => dest.ImageType, opt => opt.MapFrom(src => src.ImageType));
        
        CreateMap<Audio, AudioDto>();
        CreateMap<Album, AlbumDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        CreateMap<Artist, ArtistDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

        CreateMap<Track, TrackWithAlbumDto>()
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(ta => ta.Artist)))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album))
            .ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio));

        CreateMap<TrackDto, Track>();
    }
}
