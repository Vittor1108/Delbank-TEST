using AutoMapper;
using Delbank.Application.DTOs.DvdDTOs;
using Delbank.Domain.Entities.NoSQL;
using Delbank.Domain.Entities.SQL;

namespace Delbank.Application.Mappers
{
    public class DvdMapper : Profile
    {
        public DvdMapper()
        {
            CreateMap<DvdRequestDTO, DvdEntitySQL>().ReverseMap();
            CreateMap<DvdResponseDTO, DvdEntitySQL>().ReverseMap();
            CreateMap<DvdNoSqlEntity, DvdEntitySQL>().ReverseMap();
        }
    }
}
