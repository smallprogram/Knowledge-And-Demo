using AutoMapper;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Profiles
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
        }
    }
}
