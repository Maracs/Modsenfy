using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class UserService
{
    
    private readonly UserRepository _userRepository;
        
    private readonly UserInfoRepository _userInfoRepository;
        
    private readonly ImageRepository _imageRepository;
        
    private readonly ImageTypeRepository _imageTypeRepository;

    private readonly RequestRepository _requestRepository;
    
    private readonly IMapper _mapper;
        
    public UserService( UserRepository userRepository,
        UserInfoRepository userInfoRepository,
        ImageRepository imageRepository,
        ImageTypeRepository imageTypeRepository,RequestRepository requestRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        
        _userInfoRepository = userInfoRepository;
        
        _imageRepository = imageRepository;

        _imageTypeRepository = imageTypeRepository;

        _mapper = mapper;

        _requestRepository = requestRepository;
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
            .SkipWhile((playlist, i) => i < offset)
            .Take(limit);

       var playlistDtoList = new List<PlaylistDto>();
       
        foreach (var playlist in limitedPlaylists)
        {
            
            
            var playlistDto = new PlaylistDto()
            {
                Id = playlist.PlaylistId,
                Name = playlist.PlaylistName,
                Release = playlist.PlaylistRelease.ToString(),
                Owner = new UserDto()
                {
                    UserId = user.UserId,
                    UserNickname = user.UserNickname,
                    Image = _mapper.Map<ImageDto>(user.UserInfo.Image),
                },
                Followers = new PlaylistFollowersDto()
                {
                    Total = playlist.UserPlaylists.Count,
                    Url = playlist.PlaylistId.ToString(),
                },
                Tracks = new List<TrackDto>(),
                Image = _mapper.Map<ImageDto>(playlist.Image)
            };

            foreach (var track in playlist.PlaylistTracks)
            {
                var trackDto = new TrackDto()
                {
                    TrackId = track.Track.TrackId,
                    TrackName = track.Track.TrackName,
                    TrackStreams = track.Track.Streams.Count,
                    GenreName = track.Track.Genre.GenreName,
                    TrackDuration = track.Track.TrackDuration,
                    TrackGenius = track.Track.TrackGenius,
                    Audio = new AudioDto()
                    {
                        AudioFilename = track.Track.Audio.AudioFilename
                    },
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
                    trackDto.Artists.Append(artistDto);
                }

                playlistDto.Tracks.Append(trackDto);
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
            .SkipWhile((request, i) => i < offset)
            .Take(limit);

        var requestDtoList = new List<RequestDto>();

        foreach (var request in limitedRequests)
        {
            var requestDto = new RequestDto()
            {
                Id = request.RequestId,
                Name = request.RequestArtistName,
                Bio = request.RequestArtistBio,
                Image = _mapper.Map<ImageDto>(request.Image),
                User = _mapper.Map<UserWithIdAndDetailsAndEmailDto>(request.User)
            };
            requestDtoList.Add(requestDto);
        }

        return requestDtoList;
    }

    public async Task AnswerRequest(int id, string status)
    {
       await _requestRepository.UpdateStatus(id, status);

       //Добавить логику обновления роли
       
       await _requestRepository.SaveChanges();
    }

}