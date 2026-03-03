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
            CreateMap<BeerApi.Dtos.CustomerCreateDto, CustomerEntity>();
            CreateMap<BeerApi.Dtos.CustomerUpdateDto, CustomerEntity>();
            // If needed map other entities/DTOs here
        }
    }
}
