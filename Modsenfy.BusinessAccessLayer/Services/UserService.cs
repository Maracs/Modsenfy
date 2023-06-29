using System.ComponentModel;
using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Newtonsoft.Json;

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
    
    private readonly IMapper _mapper;
        
    public UserService( UserRepository userRepository,
        UserInfoRepository userInfoRepository,
        ImageRepository imageRepository,
        ImageTypeRepository imageTypeRepository,RequestRepository requestRepository,UserTrackRepository userTrackRepository,UserAlbumRepository userAlbumRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        
        _userInfoRepository = userInfoRepository;
        
        _imageRepository = imageRepository;

        _imageTypeRepository = imageTypeRepository;

        _mapper = mapper;

        _requestRepository = requestRepository;

        _userTrackRepository = userTrackRepository;

        _userAlbumRepository = userAlbumRepository;
    }

    public async Task<int> RegisterUser(UserWithDetailsAndEmailAndPasshashDto userDto)
    {

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
            UserInfoRegistrationDate = DateTime.Parse(userDto.Details.UserInfoRegistrationDate),
            ImageId = imageId
        };

        var userInfoId = (await _userInfoRepository.CreateAndGet(userInfo)).UserInfoId;
        
        var user = new User()
        {
            UserEmail = userDto.Email,
            UserNickname = userDto.Nickname,
            UserPasshash = userDto.Passhash,
            UserInfoId = userInfoId,
            UserRoleId = 1 // Доработать эту логику
        };

        var userId = (await _userRepository.CreateAndGet(user)).UserId;
        
        return userId;
    }

    public async Task<List<ArtistDto>> GetUserTopArtists(int id)
    {
        var topArtistsCount = 5;

        var user = await _userRepository.GetUserTopArtists(id);

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

    public async Task<List<TrackWithAlbumDto>> GetUserTopTracks(int id)
    {
        var topTrackCount = 5;
        var streams = await _userRepository.GetUserTopTracks(id);
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

    public async Task<List<ArtistDto>> GetFollowedArtists(int id, int limit, int offset)
    {
        var user = await _userRepository.GetFollowedArtists(id);

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

    public async Task FollowArtists(int id, string ids)
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
                await _userRepository.FollowArtist(userArtist);
            }

            await _userRepository.SaveChanges();
    }
    
    public async Task UnfollowArtists(int id, string ids)
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
            await _userRepository.UnfollowArtist(userArtist);
        }
        await _userRepository.SaveChanges();
    }


    public async Task<IEnumerable<bool>> CheckUserFollowsArtists(int id,string ids)
    {
        var splittedIds = ids.Split(',');

        
        
        var followings =new List<bool>();

        foreach (var artistId in splittedIds)
        {
            followings.Add(await _userRepository.IfUserFollowArtist(id,int.Parse(artistId)));
        }
        
        return followings;
    }

    public async Task DeleteUser(int id)
    {
        var user =  await _userRepository.GetById(id);

        var userInfo = await _userInfoRepository.GetById(user.UserInfoId);

        var image = await _imageRepository.GetById(userInfo.ImageId);
         
        _userRepository.Delete(user);

        await _userRepository.SaveChanges();
         
        _userInfoRepository.Delete(userInfo);

        await _userInfoRepository.SaveChanges();
         
        _imageRepository.Delete(image);

        await _imageRepository.SaveChanges();
    }

    public async Task<bool> UpdateUser(int id, UserWithDetailsAndEmailDto userDto)
    {
        var user = await _userRepository.GetById(id);
        user.UserNickname = userDto.Nickname;
        user.UserEmail = userDto.Email;
        
        var userInfo = await _userInfoRepository.GetById(user.UserInfoId);
        userInfo.UserInfoAddress = userDto.Details.UserInfoAddress;
        userInfo.UserInfoPhone = userDto.Details.UserInfoPhone;
        userInfo.UserInfoFirstName = userDto.Details.UserInfoFirstname;
        userInfo.UserInfoLastName = userDto.Details.UserInfoLastname;
        userInfo.UserInfoMiddleName = userDto.Details.UserInfoMiddlename;
        userInfo.UserInfoRegistrationDate =  DateTime.Parse(userDto.Details.UserInfoRegistrationDate );

        var image = await _imageRepository.GetById(userInfo.ImageId);
        image.ImageFilename = userDto.Image.ImageFilename;

        var imageType = await _imageTypeRepository.GetIfExists(userDto.Image.ImageTypeName);
        if(imageType==null)
            return false;
        image.ImageTypeId = imageType.ImageTypeId;

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        await _userInfoRepository.Update(userInfo);
        await _userInfoRepository.SaveChanges();

        await _imageRepository.Update(image);
        await _imageRepository.SaveChanges();


        return true;
    }

    public async Task<List<PlaylistDto>> GetUserPlaylists(int id,int limit,int offset)
    {
        var user = await _userRepository.GetUserWithPlaylists(id);
        
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

     public async Task<List<PlaylistDto>> GetUserSavedPlaylists(int id,int limit,int offset)
    {
        var userPlaylists = await _userRepository.GetUserWithSavedPlaylists(id);
        
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
    
    public async Task<RequestDto> GetRequest(int id)
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

    public async Task<List<RequestDto>> GetSeveralRequests(int limit, int offset, string status)
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

    public async Task AnswerRequest(int id, string status)
    {
       
       
       var request = await _requestRepository.GetById(id);
        
       var requestStatus = await _requestRepository.GetRequestStatusIdByName(status);

       request.RequestStatusId = requestStatus;

       await _requestRepository.Update(request);
       await _requestRepository.SaveChanges();

       //Добавить логику обновления роли
       
       await _requestRepository.SaveChanges();
    }

    public async Task<List<RequestDto>> GetSeveralUserRequests(int id, int limit, int offset, string status)
    {

        var user = await _userRepository.GetUserRequests(id);

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

    public async Task<RequestDto> GetUserRequest(int userId, int requestId)
    {
        var user = await _userRepository.GetUserRequests(userId);

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


    public async Task SaveTracksForUser(int id, string ids)
    {
        var trackIds = ids.Split(",");
        
        foreach (var trackId in trackIds)
        {
            var entity = new UserTracks() { TrackId = int.Parse(trackId), UserId = id, UserTrackAdded = DateTime.Now };
            await _userTrackRepository.Create(entity);
        }
        await _userTrackRepository.SaveChanges();
    }

    public async Task DeleteUserSavedTracks(int id, string ids)
    {
        var trackIds = ids.Split(",");
        
        foreach (var trackId in trackIds)
        {
             _userTrackRepository.Delete(new UserTracks() { TrackId =int.Parse(trackId), UserId = id });
            
        }
        await _userTrackRepository.SaveChanges();
    }

    public async Task<List<bool>> CheckUserSavedTracks(int id, string ids)
    {
        var trackIds = ids.Split(",");

        var userFollowTracks = new List<bool>();
        
        foreach (var trackId in trackIds)
        {
           var userFollowTrack = await  _userTrackRepository.IfUserFollowTrack( id,int.Parse(trackId) );
           userFollowTracks.Add(userFollowTrack);
        }

        return userFollowTracks;
    }

    public async Task<List<bool>> CheckUserSavedAlbums(int id, string ids)
    {
        var albumIds = ids.Split(",");

        var userFollowAlbums = new List<bool>();
        
        foreach (var albumId in albumIds)
        {
            var userFollowAlbum = await  _userAlbumRepository.IfUserFollowAlbum( id,int.Parse(albumId) );
            userFollowAlbums.Add(userFollowAlbum);
        }

        return userFollowAlbums;
    }
    
    
    public async Task SaveAlbumsForUser(int id, string ids)
    {
        var albumIds = ids.Split(",");

        foreach (var albumId in albumIds)
        {
            await _userAlbumRepository.Create(new UserAlbums() {UserId = id,AlbumId = int.Parse(albumId),UserAlbumsAdded =DateTime.Now});
        }

        await _userAlbumRepository.SaveChanges();
    }

    public async Task DeleteUserSavedAlbums(int id, string ids)
    {
        var albumIds = ids.Split(",");

        foreach (var albumId in albumIds)
        {
            var entity = await _userAlbumRepository.GetById(id, int.Parse(albumId));
            
            _userAlbumRepository.Delete(entity);
        }

        await _userAlbumRepository.SaveChanges();
    }

    public async Task CreateRequest(int id, RequestWithoutIdAndTimeDto requestDto)
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

        await _requestRepository.Create(request);
        await _requestRepository.SaveChanges();
    }

    public async  Task<List<AlbumWithTracksDto>> GetUserSavedAlbums(int id, int limit, int offset)
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

    public async Task<List<TrackWithAlbumDto>> GetUserTracks(int id, int limit, int offset)
    {
        var userTracks = await _userTrackRepository.GetUserTracks(id);

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

    public async Task<List<UserStreamDto>> GetUserStreamHistory(int id, int limit, int offset)
    {
        var streams = await _userRepository.GetUserStreamHistory(id);

        var limitedStreams = streams.Skip(offset).Take(limit);

        var streamDtos = new List<UserStreamDto>();

        
        
        
        return streamDtos;
    }

}