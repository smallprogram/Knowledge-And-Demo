using AutoMapper;
using RESTFul.Data.Entities.AppDbEntities;
using RESTFul.Models.Dto;
using System;

namespace RESTFul.MappingProfiles
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

            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();

        }
    }
}
