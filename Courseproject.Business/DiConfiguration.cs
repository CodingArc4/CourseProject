using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Courseproject.Common.Interface;
using Courseproject.Business.Service;
using Courseproject.Business.Validation;

namespace Courseproject.Business;

public class DiConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITeamService, TeamService>();
        
        services.AddScoped<AddressCreateValidator>();
        services.AddScoped<AddressUpdateValidator>();
        services.AddScoped<EmployeeCreateValidator>();
        services.AddScoped<EmployeeUpdateValidators>();
        services.AddScoped<JobCreateValidator>();
        services.AddScoped<JobUpdateValidator>();
        services.AddScoped<TeamCreateValidator>();
        services.AddScoped<TeamUpdateValidator>();
    }
}
