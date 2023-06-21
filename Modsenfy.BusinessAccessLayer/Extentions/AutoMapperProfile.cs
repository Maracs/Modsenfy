using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Modsenfy.BusinessAccessLayer.Extentions;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Image, ImageDto>()
            .ForMember(dest => dest.ImageTypeName, opt => opt.MapFrom(src => src.ImageType.ImageTypeName));

        CreateMap<Genre, GenreDto>();

        CreateMap<Audio, AudioDto>();
        CreateMap<AudioDto, Audio>();

        CreateMap<Album, AlbumDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.AlbumTypeName, opt => opt.MapFrom(src => src.AlbumType.AlbumTypeName));

        CreateMap<Artist, ArtistDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

        CreateMap<ArtistDto, Artist>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

        CreateMap<Track, TrackWithAlbumDto>()
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(ta => ta.Artist)))
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album))
            .ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio))
            .ForMember(dest => dest.TrackGenre, opt => opt.MapFrom(src => src.Genre.GenreName));

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.UserInfo.Image));

        CreateMap<DataAccessLayer.Entities.Stream, InnerStreamWithListenerDto>()
            .ForMember(dest => dest.Listener, opt => opt.MapFrom(src => src.User));

        CreateMap<Track, TrackWithStreamsDto>()
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(ta => ta.Artist)))
            .ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio))
            .ForMember(dest => dest.TrackGenre, opt => opt.MapFrom(src => src.Genre.GenreName))
            .ForMember(dest => dest.StreamsListeners, opt => opt.MapFrom(src => src.Streams));
    }
}
