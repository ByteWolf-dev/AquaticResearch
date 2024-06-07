using AquaticResearch.DTO;
using Base.Web.Controller;
using Core.Contracts;
using Core.Entities;
using Core.Entities.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AquaticResearch.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [Authorize]
    public async Task<ActionResult<IEnumerable<ResearchProjectDto>>> GetAllAsync()
    {
        var projects = await _uow.ResearchProjectRepository.GetAllResearchProjectDTOs();
        return this.Ok(projects);
    }
    
    [HttpPost]
    [Authorize (Roles = "Scientist")]
    public async Task<ActionResult<ResearchProjectDto>> CreateAsync([FromBody] ResearchProjectDto project)
    {
        var newProject = await _uow.ResearchProjectRepository.AddAsync(project.ToEntity());
        await _uow.SaveChangesAsync();
        return this.Created();
    }

}