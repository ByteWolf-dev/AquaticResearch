using Base.Web.Controller;
using Core.Contracts;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AquaticResearch.Controllers;

[ApiController]
[Route("[controller]")]
public class ResearchProjectController : Controller
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<ResearchProjectController> _logger;

    public ResearchProjectController(IUnitOfWork uow, ILogger<ResearchProjectController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResearchProject>>> GetAllAsync()
    {
        var projects = await _uow.ResearchProjectRepository.GetAsync();
        return await this.NotFoundOrOk(projects);
    }
    
    [HttpPost]
    public async Task<ActionResult<Location>> AddLocationAsync(Location locationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var location = new Location()
        {
            Name = locationDto.Name,
            Latitude = locationDto.Latitude,
            Longitude = locationDto.Longitude
        };

        await _uow.LocationRepository.AddAsync(location);
        int changes = await _uow.SaveChangesAsync();

        return Ok(changes);
    }

}