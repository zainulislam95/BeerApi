using AutoMapper;
using BeerApi.Models;
using BeerApi.Models.Entities;

namespace BeerApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, BeerApi.Dtos.CustomerDto>().ReverseMap();
            // Map from DTO to entity but ignore identity and CreatedAt to avoid modifying keys on update
            CreateMap<BeerApi.Dtos.CustomerDto, CustomerEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            // If needed map other entities/DTOs here
        }
    }
}
