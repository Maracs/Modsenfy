using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Search;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class SearchService
{
    private readonly AlbumRepository _albumRepository;
    private readonly ArtistRepository _artistRepository;
    private readonly TrackRepository _trackRepository;
    private readonly IMapper _mapper;
    public SearchService(
        AlbumRepository albumRepository,
        ArtistRepository artistRepository,
        TrackRepository trackRepository,
        IMapper mapper
        )
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _trackRepository = trackRepository;
        _mapper = mapper;
    }

    public async Task<SearchDto> Search(string query)
    {
        var albums = await _albumRepository.GetAll();
    
        IEnumerable<Searchable> albumSearchables = new List<Searchable>();
        foreach (var album in albums)
        {
            albumSearchables.Append(new Searchable(album, album.GetType().GetProperty(nameof(Album.AlbumName)), query));
        }
        albumSearchables = albumSearchables.OrderByDescending(a => a.Rate);

        var tracks = await _trackRepository.GetAll();

        var searchDto = new SearchDto()
        {
            Albums = albumSearchables.Select(a => _mapper.Map<AlbumDto>((Album)a.SearchObject))
        };

        return searchDto;
    }
}
