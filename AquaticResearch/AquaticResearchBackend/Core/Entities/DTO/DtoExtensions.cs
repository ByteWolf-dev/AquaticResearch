using System.Collections;
using System.Text;
using Core.Entities;
using Core.Entities.DTO.DTOs;

namespace AquaticResearch.DTO
{
    public static class DtoExtensions
    {
        public static ResearchProjectDto ToDto(this ResearchProject researchProject)
        {
            var projectDto = new ResearchProjectDto()
            {
                Title = researchProject.Title,
                Species = researchProject.Species,
            };
            return projectDto;
        }
        
        public static ResearchProject ToEntity(this ResearchProjectDto researchProjectDto)
        {
            var project = new ResearchProject()
            {
                Title = researchProjectDto.Title,
                Species = new Species()
                {
                    Name = researchProjectDto.Species.Name,
                    ScientificName = researchProjectDto.Species.ScientificName
                }
            };

            return project;
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
        
        public static Observation ToEntity(this ObservationDto observationDto, IList<ResearchProject> projects, string title)
        {
            var observation = new Observation()
            {
                Notes = observationDto.Notes,
                ObservationDateTime = observationDto.ObservationDateTime,
                ResearchProject = projects.FirstOrDefault(p => p.Title == title),
                Location = new Location()
                {
                    Name = observationDto.Location
                }
            };

            return observation;
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