using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace AquaticResearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeciesController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<SpeciesController> _logger;

        public SpeciesController(IUnitOfWork uow, ILogger<SpeciesController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Species>>> GetAllSpeciesAsync()
        {
            try
            {
                var species = await _uow.SpeciesRepository.GetAsync();
                return Ok(species);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting species: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Species>>> AddSpeciesAsync(int i)
        {
            var tmp = new Species()
            {
                Name = "joeh",
                ScientificName = "xx"
            };

            var x = await _uow.SpeciesRepository.AddAsync(tmp);
            int changes = await _uow.SaveChangesAsync();
            return Ok(changes);
        }

    }
}