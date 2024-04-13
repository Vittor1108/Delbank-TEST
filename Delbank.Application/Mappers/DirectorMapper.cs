using AutoMapper;
using Delbank.Application.DTOs.DirectorDTOs;
using Delbank.Domain.Entities.SQL;

namespace Delbank.Application.Mappers
{
    public class DirectorMapper : Profile
    {
        public DirectorMapper()
        {
            CreateMap<DirectorResponseDTO, DirectorEntitySQL>().ReverseMap();
            CreateMap<DirectorRequestDTO, DirectorEntitySQL>().ReverseMap();
        }
    }
}
