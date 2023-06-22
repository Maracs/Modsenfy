using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;

namespace Modsenfy.BusinessAccessLayer.Services;

public class PlaylistService
{
    private readonly PlaylistRepository _playlistRepository;
    
    private readonly IMapper _mapper;
    public PlaylistService(PlaylistRepository playlistRepository, IMapper mapper)
    {
        _playlistRepository = playlistRepository;
		_mapper = mapper;
    }
}