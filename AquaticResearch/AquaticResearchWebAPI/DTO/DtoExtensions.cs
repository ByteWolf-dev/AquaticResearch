using System.Collections;
using AquaticResearch.DTO.DTOs;
using Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaticResearch.DTOExtensions
{
    public static class DtoExtensions
    {
        public static ResearchProjectDto ToDto(this ResearchProject researchProject)
        {
            var projectDto = new ResearchProjectDto()
            {
                Title = researchProject.Title,
                Species = researchProject.Species.Name,
                ObservationDtos = researchProject.Observations.Select(o => o.ToDto()).ToList()
            };


            return projectDto;
        }

        public static ObservationDto ToDto(this Observation observation)
        {
            
            var observationDto = new ObservationDto()
            {
                Notes = observation.Notes,
                ObservationDateTime = observation.ObservationDateTime,
                Equipment = GetListAsString(observation.Equipment),
                Location = observation.Location.Name,
                Researchers = GetListAsString(observation.Researchers)
            };
            return observationDto;
        }

        public static string GetListAsString(IList list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var entity in list)
            {
                sb.Append(entity.ToString());
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}