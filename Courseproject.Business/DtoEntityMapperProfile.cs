using AutoMapper;
using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Dtos.Job;
using Courseproject.Common.Dtos.Team;
using Courseproject.Common.Dtos.Teams;
using Courseproject.Common.Interface;
using Courseproject.Common.Model;

namespace Courseproject.Business
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            //for address
            CreateMap<AddressCreate, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AddressUpdate, Address>();
            CreateMap<Address, AddressGet>();

            //for job
            CreateMap<JobCreate, Job>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<JobUpdate, Job>();
            CreateMap<Job, JobGet>();

            //for employee
            CreateMap<EmployeeCreate, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Teams, opt => opt.Ignore())
            .ForMember(dest => dest.Jobs, opt => opt.Ignore());

            CreateMap<EmployeeUpdate, Employee>()
                .ForMember(dest => dest.Teams, opt => opt.Ignore())
                .ForMember(dest => dest.Jobs, opt => opt.Ignore());


            CreateMap<Employee, EmployeeList>();

            CreateMap<Employee, EmployeeDetails>()
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Teams, opt => opt.Ignore()) //todo: add teams
                .ForMember(dest => dest.Jobs, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.Ignore());

            //for team
            CreateMap<TeamCreate, Team>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
            
            CreateMap<TeamUpdate, Team>()
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
            
            CreateMap<Team, TeamGet>();
        }
    }
}