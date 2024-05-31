using AquaticResearch.DTO.DTOs;
using AquaticResearch.DTOExtensions;
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
    public async Task<ActionResult<IEnumerable<ResearchProjectDto>>> GetAllAsync()
    {
        var projects = await _uow.ResearchProjectRepository.GetAll();
        var projectDtos = projects.Select(p => p.ToDto()).ToList();
        return this.Ok(projectDtos);
    }

}