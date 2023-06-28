using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Services;

namespace Modsenfy.PresentationLayer.Controllers;


[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;
    
    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<SearchDto>> Search([FromQuery] string query)
    {
        var searchDto = await _searchService.Search(query);
        return Ok(searchDto);
    }
}