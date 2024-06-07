using AquaticResearch.DTO;
using Base.Web.Controller;
using Core.Contracts;
using Core.Entities.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticResearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObservationController : Controller
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<ResearchProjectController> _logger;

    public ObservationController(IUnitOfWork uow, ILogger<ResearchProjectController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ObservationDto>>> GetObservationByTitle(string title)
    {
        var observations = await _uow.ObservationRepository.GetObservationsByResearchProjectTitle(title);
        return this.Ok(observations);
    }

    [HttpPost]
    [Authorize(Roles = "Scientist")]
    public async Task<ActionResult<ObservationDto>> CreateAsync([FromBody] ObservationWithTitleDto observation)
    {
        var newObservation = await _uow.ObservationRepository
            .AddAsync(observation.Observation.ToEntity(
                await _uow.ResearchProjectRepository.GetAsync(),
                observation.Title
            ));
        await _uow.SaveChangesAsync();
        return this.Created();
    }
}