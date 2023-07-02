using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;

namespace Modsenfy.BusinessAccessLayer.Services;

public class ArtistService
{
    private readonly ArtistRepository _artistRepository;
    
    private readonly IMapper _mapper;
    public ArtistService(ArtistRepository artistRepository, IMapper mapper)
    {
        _artistRepository = artistRepository;
		_mapper = mapper;
    }
}