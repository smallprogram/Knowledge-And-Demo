using AutoMapper;
using RESTFul.Data.Entities.AppDbEntities;
using RESTFul.Models.Dto;

namespace RESTFul.MappingProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(
                target => target.CompanyName,
                source => source.MapFrom(src => src.Name)
                );

            CreateMap<CompanyAddDto, Company>();
            CreateMap<Company, CompanyFullDto>();
            CreateMap<CompanyAddWithBankruptTimeDto, Company>();
        }
    }
}
