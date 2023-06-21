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

		CreateMap<Audio, AudioDto>()
			.ForMember(dest => dest.AudioFilename, opt => opt.MapFrom(src => src.AudioFilename));

		CreateMap<Genre, GenreDto>();

		CreateMap<Artist, ArtistDto>()
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
      .ReverseMap();
			
		CreateMap<Track, TrackDto>()
			.ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(ta => ta.Artist)))
			.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
			.ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio)).ReverseMap();
			

		CreateMap<Album, AlbumWithTracksDto>()
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
			.ForMember(dest => dest.AlbumRelease, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.AlbumRelease)))
			.ForMember(dest => dest.AlbumType, opt => opt.MapFrom(src => src.AlbumType.AlbumTypeName))
			.ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
			.ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks));
			
		

		CreateMap<Album, AlbumDto>()
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
			.ForMember(dest => dest.AlbumRelease, opt => opt.MapFrom(src =>
			DateOnly.FromDateTime(src.AlbumRelease)))
			.ForMember(dest => dest.AlbumTypeName, opt => opt.MapFrom(src => src.AlbumType.AlbumTypeName))
			.ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist));

		CreateMap<Track, TrackWithAlbumDto>()
      .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.TrackArtists.Select(ta => ta.Artist)))
      .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album))
      .ForMember(dest => dest.Audio, opt => opt.MapFrom(src => src.Audio))
      .ForMember(dest => dest.TrackGenre, opt => opt.MapFrom(src => src.Genre.GenreName));
		
    CreateMap<TrackDto, Track>();

		CreateMap<DataAccessLayer.Entities.Stream, StreamDto>()
			.ForMember(dest => dest.Listener, opt => opt.MapFrom(src => src.User))
			.ForMember(dest => dest.Track, opt => opt.MapFrom(src => src.Track));
		
        RecognizePrefixes("UserInfo");

        CreateMap<UserInfo, UserDetailsDto>()
            .ForMember(dest => dest.UserInfoRegistrationDate,
                opt => opt.MapFrom(src => src.UserInfoRegistrationDate.ToString()));
        
        RecognizePrefixes("User");

        CreateMap<User, UserWithDetailsAndEmailAndIdAndRoleDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest=>dest.Image,opt=>opt.MapFrom(src=>src.UserInfo.Image))
            .ForMember(dest=>dest.Details,opt=>opt.MapFrom(src=>src.UserInfo));
        
        CreateMap<User, UserWithIdAndDetailsAndEmailDto>()
            .ForMember(dest=>dest.Image,opt=>opt.MapFrom(src=>src.UserInfo.Image))
            .ForMember(dest=>dest.Details,opt=>opt.MapFrom(src=>src.UserInfo));
            
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
