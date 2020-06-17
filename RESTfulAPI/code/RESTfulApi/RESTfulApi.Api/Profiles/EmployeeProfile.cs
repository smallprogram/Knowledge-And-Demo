using AutoMapper;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember
                (
                target => target.Name,
                src => src.MapFrom(src => $"{src.FirstName} {src.LastName}")  //$"{src.FirstName} {src.LastName}"
                )
                .ForMember
                (
                target => target.GenderDisplay,
                src => src.MapFrom(src => src.Gender.ToString())
                )
                .ForMember
                (
                target => target.Age,
                src => src.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year)
                );
        }
    }
}
