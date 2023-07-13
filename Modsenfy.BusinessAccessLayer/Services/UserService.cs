using System.ComponentModel;
using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.BusinessAccessLayer.DTOs.UserDtos;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Modsenfy.BusinessAccessLayer.Services;

public class UserService
{
    
    private readonly UserRepository _userRepository;
        
    private readonly UserInfoRepository _userInfoRepository;
        
    private readonly ImageRepository _imageRepository;
        
    private readonly ImageTypeRepository _imageTypeRepository;

    private readonly RequestRepository _requestRepository;

    private readonly UserTrackRepository _userTrackRepository;

    private readonly UserAlbumRepository _userAlbumRepository;

    private readonly UserPlaylistRepository _userPlaylistRepository;

    private readonly ArtistRepository _artistRepository;
    
    private readonly IMapper _mapper;
  
   private readonly TokenService _tokenService;

        
    public UserService( UserRepository userRepository, UserInfoRepository userInfoRepository,
        ImageRepository imageRepository, ImageTypeRepository imageTypeRepository,
        RequestRepository requestRepository, UserTrackRepository userTrackRepository,
        UserAlbumRepository userAlbumRepository, UserPlaylistRepository userPlaylistRepository,
        ArtistRepository artistRepository,IMapper mapper,TokenService tokenService)
    {
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _imageRepository = imageRepository;
        _imageTypeRepository = imageTypeRepository;
        _mapper = mapper;
        _requestRepository = requestRepository;
        _userTrackRepository = userTrackRepository;
        _userAlbumRepository = userAlbumRepository;
        _userPlaylistRepository = userPlaylistRepository;
        _artistRepository =   artistRepository;
        _tokenService = tokenService;
    }

    public async Task<UserTokenDto> SignInUserAsync(UserSigningDto userDto)
    {
        var user = await _userRepository.GetByUsername(userDto.UserNickname);
        if (user == null)  return new UserTokenDto() { UserToken = "None"};

        using var hmac = new HMACSHA512(Convert.FromBase64String(user.UserPasshashSalt));
        var computeHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)));
        if (computeHash != user.UserPasshash)
            return new UserTokenDto() { UserToken = "None" };

        return new UserTokenDto()
        {
            UserNickname = user.UserNickname,
            UserToken = await _tokenService.GetTokenAsync(user)
        };
    }

    public async Task<UserTokenDto> RegisterUserAsync(UserWithDetailsAndEmailAndPasshashDto userDto)
    {

        if (await _userRepository.IfNicknameExistsAsync(userDto.Nickname) || await _userRepository.IfEmailExistsAsync(userDto.Email))
            return null;

        var image = new Image()
        {
            ImageFilename = userDto.Image.ImageFilename,
            ImageTypeId = (await _imageTypeRepository.GetIfExists(userDto.Image.ImageTypeName)).ImageTypeId
        };
        
        var imageId = (await _imageRepository.CreateAndGet(image)).ImageId;

        var userInfo = new UserInfo()
        {
            UserInfoAddress = userDto.Details.UserInfoAddress,
            UserInfoPhone = userDto.Details.UserInfoPhone,
            UserInfoFirstName = userDto.Details.UserInfoFirstname,
            UserInfoLastName = userDto.Details.UserInfoLastname,
            UserInfoMiddleName = userDto.Details.UserInfoMiddlename,
            UserInfoRegistrationDate = DateTime.Now,
            ImageId = imageId
        };

        var userInfoId = (await _userInfoRepository.CreateAndGetAsync(userInfo)).UserInfoId;
        
        using var hmac = new HMACSHA512();

        var user = new User()
        {
            UserEmail = userDto.Email,
            UserNickname = userDto.Nickname,
            UserPasshash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Passhash))),
            UserPasshashSalt = Convert.ToBase64String(hmac.Key),
            UserInfoId = userInfoId,
            UserRoleId = 1 
        };

        var userId = (await _userRepository.CreateAndGetAsync(user)).UserId;
        
        user.UserId = userId;
        var userRegDto = new UserTokenDto()
        {
            UserNickname = user.UserNickname,
            UserToken = await _tokenService.GetTokenAsync(user)
        };

        return userRegDto;
    }

    public async Task<List<ArtistDto>> GetUserTopArtistsAsync(int id)
    {
        var topArtistsCount = 5;

        var user = await _userRepository.GetUserTopArtistsAsync(id);

        var streams = user.Streams;

        

        var artistsMap = new Dictionary<Artist, int>();
        
        foreach (var stream in streams)
        {

            foreach (var artist in stream.Track.TrackArtists)
            {
                if (artistsMap.ContainsKey(artist.Artist))
                {
                    artistsMap[artist.Artist]++;
                }
                else
                {
                    artistsMap.Add(artist.Artist,0);      
                }
              
            }
        }

        var topArtists =  artistsMap.OrderByDescending(pair => pair.Value).Take(topArtistsCount);

        var topArtistsDto = new List<ArtistDto>();

        foreach (var topArtist in topArtists)
        {
            var topArtistDto = _mapper.Map<ArtistDto>(topArtist.Key);
            topArtistDto.Followers = new ArtistFollowersDto()
            {
                Total = topArtist.Key.UserArtists.Count,
                Url = topArtist.Key.ArtistId.ToString()
            };
            topArtistsDto.Add(topArtistDto);
        }
        
        return topArtistsDto;
    }

    public async Task<UserWithDetailsAndEmailAndIdAndRoleDto> GetUserProfileForAdminAsync(int id)
    {
        var userWithJoins = await _userRepository.GetByIdWithJoinsAsync(id);
        return  _mapper.Map<UserWithDetailsAndEmailAndIdAndRoleDto>(userWithJoins);
    }

    public async Task<User> GetUserProfileForUserAsync(int id)
    {
        return  await _userRepository.GetByIdWithJoinsAsync(id); 
    }

    public async Task<List<TrackWithAlbumDto>> GetUserTopTracksAsync(int id)
    {
        var topTrackCount = 5;
        var streams = await _userRepository.GetUserTopTracksAsync(id);
        var tracksMap = new Dictionary<Track, int>();
        
        foreach (var stream in streams)
        {
            if (tracksMap.ContainsKey(stream.Track))
            {
                tracksMap[stream.Track]++; }
            else
            {
                tracksMap.Add(stream.Track,0); 
            }
        }

        var topTracks =  tracksMap.OrderByDescending(pair => pair.Value).Take(topTrackCount);
        var topTracksDto = new List<TrackWithAlbumDto>();

        foreach (var topTrack in topTracks)
        {
            var topTrackDto = _mapper.Map<TrackWithAlbumDto>(topTrack.Key);
            
            topTracksDto.Add(topTrackDto);
        }
        
        return topTracksDto;
    }

    public async Task<List<ArtistDto>> GetFollowedArtistsAsync(int id, int limit, int offset)
    {
        var user = await _userRepository.GetFollowedArtistsAsync(id);

        var allArtists = new List<Artist>();

        foreach (var userArtist in user.UserArtists)
        {
            allArtists.Add(userArtist.Artist);
        }

        var artists = allArtists.Skip(offset).Take(limit);
        
        var artistsDto = new List<ArtistDto>();

        foreach (var artist in artists)
        {
            var artistDto = _mapper.Map<ArtistDto>(artist);
            artistDto.Followers = new ArtistFollowersDto()
            {
                Total = artist.UserArtists.Count,
                Url = artist.ArtistId.ToString()
            };
            artistsDto.Add(artistDto);
        }

        return artistsDto;
    }

    public async Task FollowArtistsAsync(int id, string ids)
    {
       
            var splittedIds = ids.Split(',');

            IEnumerable<int> intIds;

            intIds = splittedIds.Select(id => int.Parse(id));

            foreach (var intId in intIds)
            {
                var userArtist = new UserArtists()
                {
                    ArtistId = intId,
                    UserId = id,
                };
                await _userRepository.FollowArtistAsync(userArtist);
            }

            await _userRepository.SaveChangesAsync();
    }
    
    public async Task UnfollowArtistsAsync(int id, string ids)
    {
       
        var splittedIds = ids.Split(',');

        IEnumerable<int> intIds;

        intIds = splittedIds.Select(id => int.Parse(id));

        foreach (var intId in intIds)
        {
            var userArtist = new UserArtists()
            {
                ArtistId = intId,
                UserId = id,
            };
            await _userRepository.UnfollowArtistAsync(userArtist);
        }
        await _userRepository.SaveChangesAsync();
    }


    public async Task<IEnumerable<bool>> CheckUserFollowsArtistsAsync(int id,string ids)
    {
        var splittedIds = ids.Split(',');

        
        
        var followings =new List<bool>();

        foreach (var artistId in splittedIds)
        {
            followings.Add(await _userRepository.IfUserFollowArtistAsync(id,int.Parse(artistId)));
        }
        
        return followings;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user =  await _userRepository.GetByIdAsync(id);

        var userInfo = await _userInfoRepository.GetByIdAsync(user.UserInfoId);

        var image = await _imageRepository.GetByIdAsync(userInfo.ImageId);
         
        _userRepository.DeleteAsync(user);

        await _userRepository.SaveChangesAsync();
         
        _userInfoRepository.DeleteAsync(userInfo);

        await _userInfoRepository.SaveChangesAsync();
         
        _imageRepository.DeleteAsync(image);

        await _imageRepository.SaveChangesAsync();
    }

    public async Task<bool> UpdateUserAsync(int id, UserWithDetailsAndEmailDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        user.UserNickname = userDto.Nickname;
        user.UserEmail = userDto.Email;
        
        var userInfo = await _userInfoRepository.GetByIdAsync(user.UserInfoId);
        userInfo.UserInfoAddress = userDto.Details.UserInfoAddress;
        userInfo.UserInfoPhone = userDto.Details.UserInfoPhone;
        userInfo.UserInfoFirstName = userDto.Details.UserInfoFirstname;
        userInfo.UserInfoLastName = userDto.Details.UserInfoLastname;
        userInfo.UserInfoMiddleName = userDto.Details.UserInfoMiddlename;
        userInfo.UserInfoRegistrationDate =  DateTime.Parse(userDto.Details.UserInfoRegistrationDate );

        var image = await _imageRepository.GetByIdAsync(userInfo.ImageId);
        image.ImageFilename = userDto.Image.ImageFilename;

        var imageType = await _imageTypeRepository.GetIfExists(userDto.Image.ImageTypeName);
        if(imageType==null)
            return false;
        image.ImageTypeId = imageType.ImageTypeId;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        await _userInfoRepository.UpdateAsync(userInfo);
        await _userInfoRepository.SaveChangesAsync();

        await _imageRepository.UpdateAsync(image);
        await _imageRepository.SaveChangesAsync();


        return true;
    }

    public async Task<List<PlaylistDto>> GetUserPlaylistsAsync(int id,int limit,int offset)
    {
        var user = await _userRepository.GetUserWithPlaylistsAsync(id);
        if (user == null) { return null; }
       var limitedPlaylists= user.Playlists
            .OrderBy(playlist => playlist.PlaylistId)
            .Skip(offset)
            .Take(limit);

       var playlistDtoList = new List<PlaylistDto>();
       
        foreach (var playlist in limitedPlaylists)
        {
            var owner = new UserDto()
            {
                UserId = user.UserId,
                UserNickname = user.UserNickname,
                Image = _mapper.Map<ImageDto>(user.UserInfo.Image),
            };

            var followers = new PlaylistFollowersDto()
            {
                Total = playlist.UserPlaylists.Count,
                Url = playlist.PlaylistId.ToString(),
            };
            var playlistDto = new PlaylistDto()
            {
                Id = playlist.PlaylistId,
                Name = playlist.PlaylistName,
                Release = playlist.PlaylistRelease.ToString(),
                Owner = owner,
                Followers = followers,
                Tracks = new List<TrackDto>(),
                Image = _mapper.Map<ImageDto>(playlist.Image)
            };

            foreach (var track in playlist.PlaylistTracks)
            {
                
                var audio  = new AudioDto()
                {
                    AudioFilename = track.Track.Audio.AudioFilename
                };
                var trackDto = new TrackDto()
                {
                    TrackId = track.Track.TrackId,
                    TrackName = track.Track.TrackName,
                    TrackStreams = track.Track.TrackStreams,
                    GenreName = track.Track.Genre.GenreName,
                    TrackDuration = track.Track.TrackDuration,
                    TrackGenius = track.Track.TrackGenius,
                    Audio = audio,
                    Artists = new List<ArtistDto>()

                };

                foreach (var artist in track.Track.TrackArtists)
                {
                    var artistDto = new ArtistDto()
                    {
                        ArtistId  = artist.Artist.ArtistId,
                        ArtistName = artist.Artist.ArtistName,
                        ArtistBio = artist.Artist.ArtistBio,
                        Image = _mapper.Map<ImageDto>(artist.Artist.Image),
                        Followers = new ArtistFollowersDto()
                        {
                            Total = artist.Artist.UserArtists.Count,
                            Url = artist.Artist.ArtistId.ToString(),
                        }
                    };
                    trackDto.Artists =  trackDto.Artists.Append(artistDto);
                }

                playlistDto.Tracks =  playlistDto.Tracks.Append(trackDto);
            }
            
            playlistDtoList.Add(playlistDto);
        }

        return playlistDtoList;
    }

     public async Task<List<PlaylistDto>> GetUserSavedPlaylistsAsync(int id,int limit,int offset)
    {
        var userPlaylists = await _userRepository.GetUserWithSavedPlaylistsAsync(id);
        
       var limitedPlaylists= userPlaylists
            .OrderBy(playlist => playlist.PlaylistId)
            .Skip(offset)
            .Take(limit);

       var playlistDtoList = new List<PlaylistDto>();
       
        foreach (var playlist in limitedPlaylists)
        {
            var owner = new UserDto()
            {
                UserId = playlist.UserId,
                UserNickname = playlist.User.UserNickname,
                Image = _mapper.Map<ImageDto>( playlist.User.UserInfo.Image),
            };

            var followers = new PlaylistFollowersDto()
            {
                Total = playlist.Playlist.UserPlaylists.Count,
                Url = playlist.PlaylistId.ToString(),
            };
            var playlistDto = new PlaylistDto()
            {
                Id = playlist.PlaylistId,
                Name =  playlist.Playlist.PlaylistName,
                Release =  playlist.Playlist.PlaylistRelease.ToString(),
                Owner = owner,
                Followers = followers,
                Tracks = new List<TrackDto>(),
                Image = _mapper.Map<ImageDto>( playlist.Playlist.Image)
            };

            foreach (var track in  playlist.Playlist.PlaylistTracks)
            {
                
                var audio  = new AudioDto()
                {
                    AudioFilename = track.Track.Audio.AudioFilename
                };
                var trackDto = new TrackDto()
                {
                    TrackId = track.Track.TrackId,
                    TrackName = track.Track.TrackName,
                    TrackStreams = track.Track.TrackStreams,
                    GenreName = track.Track.Genre.GenreName,
                    TrackDuration = track.Track.TrackDuration,
                    TrackGenius = track.Track.TrackGenius,
                    Audio = audio,
                    Artists = new List<ArtistDto>()

                };

                foreach (var artist in track.Track.TrackArtists)
                {
                    var artistDto = new ArtistDto()
                    {
                        ArtistId  = artist.Artist.ArtistId,
                        ArtistName = artist.Artist.ArtistName,
                        ArtistBio = artist.Artist.ArtistBio,
                        Image = _mapper.Map<ImageDto>(artist.Artist.Image),
                        Followers = new ArtistFollowersDto()
                        {
                            Total = artist.Artist.UserArtists.Count,
                            Url = artist.Artist.ArtistId.ToString(),
                        }
                    };
                    trackDto.Artists =  trackDto.Artists.Append(artistDto);
                }

                playlistDto.Tracks =  playlistDto.Tracks.Append(trackDto);
            }
            
            playlistDtoList.Add(playlistDto);
        }

        return playlistDtoList;
    }
    
    public async Task<RequestDto> GetRequestAsync(int id)
    {
        var request = await _requestRepository.GetWithJoins(id);

        var requestDto = new RequestDto()
        {
            Id = request.RequestId,
            Name = request.RequestArtistName,
            Bio = request.RequestArtistBio,
            Time = request.RequestTime.ToString(),
            Image = _mapper.Map<ImageDto>(request.Image),
            User = _mapper.Map<UserWithIdAndDetailsAndEmailDto>(request.User)
        };
        return requestDto;
    }

    public async Task<List<RequestDto>> GetSeveralRequestsAsync(int limit, int offset, string status)
    {
        var requests = await _requestRepository.GetAllWithJoins();

        var limitedRequests = requests
            .OrderBy(request => request.RequestId)
            .Where(request => request.RequestStatus.RequestStatusName == status)
            .Skip(offset)
            .Take(limit);

        var requestDtoList = new List<RequestDto>();

        foreach (var request in limitedRequests)
        {
            var requestDto = new RequestDto()
            {
                Id = request.RequestId,
                Name = request.RequestArtistName,
                Bio = request.RequestArtistBio,
                Time = request.RequestTime.ToString(),
                Image = _mapper.Map<ImageDto>(request.Image),
                User = _mapper.Map<UserWithIdAndDetailsAndEmailDto>(request.User)
            };
            requestDtoList.Add(requestDto);
        }

        return requestDtoList;
    }

    public async Task AnswerRequestAsync(int id, string status)
    {
       
       
       var request = await _requestRepository.GetByIdAsync(id);
        
       var requestStatus = await _requestRepository.GetRequestStatusIdByName(status);

       request.RequestStatusId = requestStatus;

       await _requestRepository.UpdateAsync(request);
       await _requestRepository.SaveChangesAsync();

       if (status == "accepted")
       {
           var user = await _userRepository.GetByIdAsync(request.UserId);

           user.UserRoleId = 2;

           await _userRepository.UpdateAsync(user);
           await _userRepository.SaveChangesAsync();
         
           
           var artist = new Artist()
           {
               ArtistId = request.UserId,
               ArtistBio = request.RequestArtistBio,
               ArtistName = request.RequestArtistName,
               ImageId = request.ImageId,
           };
           await _artistRepository.CreateWithId(artist);
           await _artistRepository.SaveChangesAsync();
       }
       
    }

    public async Task<List<RequestDto>> GetSeveralUserRequestsAsync(int id, int limit, int offset, string status)
    {

        var user = await _userRepository.GetUserRequestsAsync(id);

        var requests = user.Requests;

        var limitedRequests = requests
            .OrderBy(request => request.RequestId)
            .Where(request => request.RequestStatus.RequestStatusName == status)
            .Skip(offset)
            .Take(limit);
        
        var requestDtoList = new List<RequestDto>();

        foreach (var request in limitedRequests)
        {
            var requestDto = new RequestDto()
            {
                Id = request.RequestId,
                Name = request.RequestArtistName,
                Bio = request.RequestArtistBio,
                Time = request.RequestTime.ToString(),
                Image = _mapper.Map<ImageDto>(request.Image),
                User = _mapper.Map<UserWithIdAndDetailsAndEmailDto>(request.User)
            };
            requestDtoList.Add(requestDto);
        }

        return requestDtoList;
    }

    public async Task<RequestDto> GetUserRequestAsync(int userId, int requestId)
    {
        var user = await _userRepository.GetUserRequestsAsync(userId);

        var requests = user.Requests;

       var request = requests.Single(request => request.RequestId == requestId);

       var requestDto = new RequestDto()
       {
           Id = request.RequestId,
           Name = request.RequestArtistName,
           Bio = request.RequestArtistBio,
           Time = request.RequestTime.ToString(),
           Image = _mapper.Map<ImageDto>(request.Image),
           User = _mapper.Map<UserWithIdAndDetailsAndEmailDto>(request.User)
       };
       
       return requestDto;
    }


    public async Task SaveTracksForUserAsync(int id, string ids)
    {
        var trackIds = ids.Split(",");
        
        foreach (var trackId in trackIds)
        {
            var entity = new UserTracks() { TrackId = int.Parse(trackId), UserId = id, UserTrackAdded = DateTime.Now };
            await _userTrackRepository.CreateAsync(entity);
        }
        await _userTrackRepository.SaveChangesAsync();
    }

    public async Task DeleteUserSavedTracksAsync(int id, string ids)
    {
        var trackIds = ids.Split(",");
        
        foreach (var trackId in trackIds)
        {
             _userTrackRepository.DeleteAsync(new UserTracks() { TrackId =int.Parse(trackId), UserId = id });
            
        }
        await _userTrackRepository.SaveChangesAsync();
    }

    public async Task<List<bool>> CheckUserSavedTracksAsync(int id, string ids)
    {
        var trackIds = ids.Split(",");

        var userFollowTracks = new List<bool>();
        
        foreach (var trackId in trackIds)
        {
           var userFollowTrack = await  _userTrackRepository.IfUserFollowTrackAsync( id,int.Parse(trackId) );
           userFollowTracks.Add(userFollowTrack);
        }

        return userFollowTracks;
    }

    public async Task<List<bool>> CheckUserSavedAlbumsAsync(int id, string ids)
    {
        var albumIds = ids.Split(",");

        var userFollowAlbums = new List<bool>();
        
        foreach (var albumId in albumIds)
        {
            var userFollowAlbum = await  _userAlbumRepository.IfUserFollowAlbumAsync( id,int.Parse(albumId) );
            userFollowAlbums.Add(userFollowAlbum);
        }

        return userFollowAlbums;
    }
    
    
    public async Task SaveAlbumsForUserAsync(int id, string ids)
    {
        var albumIds = ids.Split(",");

        foreach (var albumId in albumIds)
        {
            await _userAlbumRepository.CreateAsync(new UserAlbums() {UserId = id,AlbumId = int.Parse(albumId),UserAlbumsAdded =DateTime.Now});
        }

        await _userAlbumRepository.SaveChangesAsync();
    }

    public async Task DeleteUserSavedAlbumsAsync(int id, string ids)
    {
        var albumIds = ids.Split(",");

        foreach (var albumId in albumIds)
        {
            var entity = await _userAlbumRepository.GetByIdAsync(id, int.Parse(albumId));
            
            _userAlbumRepository.DeleteAsync(entity);
        }

        await _userAlbumRepository.SaveChangesAsync();
    }

    public async Task CreateRequestAsync(int id, RequestWithoutIdAndTimeDto requestDto)
    {
        var image = new Image()
        {
            ImageFilename = requestDto.Image.ImageFilename,
            ImageTypeId = (await _imageTypeRepository.GetIfExists(requestDto.Image.ImageTypeName)).ImageTypeId
        };
        
        var imageWithId = await _imageRepository.CreateAndGet(image);
        var request = new Request()
        {
            RequestStatusId = 1,
            RequestTime = DateTime.Now,
            RequestArtistBio = requestDto.Bio,
            RequestArtistName = requestDto.Name,
            ImageId = imageWithId.ImageId,
            UserId = id
        };

        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveChangesAsync();
    }

    public async  Task<List<AlbumWithTracksDto>> GetUserSavedAlbumsAsync(int id, int limit, int offset)
    {
        var userAlbums = await _userAlbumRepository.GetUserSavedAlbums(id);

        var limitedUserAlbums = userAlbums
            .OrderBy(albums => albums.AlbumId)
            .Skip(offset)
            .Take(limit);

        var albumDtos = new List<AlbumWithTracksDto>();
        
        foreach (var UserAlbum in limitedUserAlbums)
        {
            albumDtos.Add(_mapper.Map<AlbumWithTracksDto>(UserAlbum.Album));
        }

        return albumDtos;
    }

    public async Task<List<TrackWithAlbumDto>> GetUserTracksAsync(int id, int limit, int offset)
    {
        var userTracks = await _userTrackRepository.GetUserTracksAsync(id);

        var limitedUserTracks = userTracks
            .OrderBy(tracks =>tracks.TrackId )
            .Skip(offset)
            .Take(limit);

        var trackDtos = new List<TrackWithAlbumDto>();
        foreach (var userTrack in limitedUserTracks)
        {
            trackDtos.Add(_mapper.Map<TrackWithAlbumDto>(userTrack.Track));
        }

        return trackDtos;
    }


    public async Task<UserStreamDto> GetUserStreamHistoryAsync(int id, int limit, int offset)
    {
        var streams = await _userRepository.GetUserStreamHistoryAsync(id);

        var limitedStreams = streams.Skip(offset).Take(limit);

        var streamDto = new UserStreamDto()
        {
            Listener = _mapper.Map<UserDto>(limitedStreams.First().User),
            Streams = new List<InnerStreamWithTrackDto>()
        };

        foreach (var stream in limitedStreams)
        {

            streamDto.Streams = streamDto.Streams.Append(new InnerStreamWithTrackDto()
            {
                Date = stream.StreamDate.ToString(),
                Track = _mapper.Map<TrackDto>(stream.Track)
            });

        }
        
        
        return streamDto;
    }

    public async Task FollowPlaylistAsync(int id, int playlistId)
    {
        await _userPlaylistRepository.FollowPlaylistAsync(id, playlistId);
        await _userPlaylistRepository.SaveChangesAsync();
    }

    public async Task UnfollowPlaylistAsync(int id, int playlistId)
    {
        await _userPlaylistRepository.UnfollowPlaylistAsync(id, playlistId);
        await _userPlaylistRepository.SaveChangesAsync();
    }

    public async Task<List<bool>> CheckIfUsersFollowPlaylistAsync(int playlistId, string ids)
    {
        var userIds = ids.Split(",");
        
        var userFollowPlaylists = new List<bool>();
        
        foreach (var userId in userIds)
        {
            var userFollowPlaylist = await  _userPlaylistRepository.IfUserFollowPlaylistAsync( playlistId,int.Parse(userId) );
            userFollowPlaylists.Add(userFollowPlaylist);
        }

        return userFollowPlaylists;
    }

    public async Task<int> CreateUserPlaylistAsync(int id, PlaylistWithNameAndImage playlistDto)
    {
       

        var imageType = await _imageTypeRepository.GetIfExists(playlistDto.Image.ImageTypeName);
        if (imageType == null)
            return -1;
        var image = new Image()
        {
            ImageFilename = playlistDto.Image.ImageFilename,
            ImageTypeId = imageType.ImageTypeId
        };
        var savedImage = await _imageRepository.CreateAndGet(image);

        var playlist = new Playlist()
        {
            PlaylistOwnerId = id,
            PlaylistName = playlistDto.Name,
            PlaylistRelease = DateTime.Now,
            CoverId = savedImage.ImageId
        };
        var savedPlaylist =await _userRepository.CreateAndGetPlaylistAsync(playlist);

        return savedPlaylist.PlaylistId;
    }
}