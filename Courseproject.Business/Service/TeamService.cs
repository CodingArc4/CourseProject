using AutoMapper;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Team;
using Courseproject.Common.Dtos.Teams;
using Courseproject.Common.Interface;
using Courseproject.Common.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Service
{
    public class TeamService : ITeamService
    {
        private IGenericRepository<Team> TeamRepository { get; }
        private IGenericRepository<Employee> EmployeeRepository { get; }
        private IMapper Mapper { get; }
        private TeamCreateValidator TeamCreateValidator { get; }
        private TeamUpdateValidator TeamUpdateValidator { get; }

        public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository,
            IMapper mapper, TeamUpdateValidator teamUpdateValidator, TeamCreateValidator teamCreateValidato)
        {
            TeamRepository = teamRepository;
            EmployeeRepository = employeeRepository;
            Mapper = mapper;
            TeamUpdateValidator = teamUpdateValidator;
            TeamCreateValidator = teamCreateValidato;
        }


        public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
        {
            await TeamCreateValidator.ValidateAndThrowAsync(teamCreate);
                
            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
            var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
            var entity = Mapper.Map<Team>(teamCreate);
            entity.Employees = employees;
            await TeamRepository.InsertAsync(entity);
            await TeamRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteTeamAsync(TeamDelete teamDelete)
        {
            var entity = await TeamRepository.getByIdAsync(teamDelete.id);
            TeamRepository.Delete(entity);
            await TeamRepository.SaveChangesAsync();
        }

        public async Task<TeamGet> GetTeamAsync(int id)
        {
            var entity = await TeamRepository.getByIdAsync(id, (team) => team.Employees);
            return Mapper.Map<TeamGet>(entity);
        }

        public async Task<List<TeamGet>> GetTeamsAsync()
        {
            var entities = await TeamRepository.GetAsync(null, null, (team) => team.Employees);
            return Mapper.Map<List<TeamGet>>(entities);
        }

        public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
        {
            await TeamUpdateValidator.ValidateAndThrowAsync(teamUpdate);

            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
            var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
            var existingEntity = await TeamRepository.getByIdAsync(teamUpdate.id, (team) => team.Employees);
            Mapper.Map(teamUpdate, existingEntity);
            existingEntity.Employees = employees;
            TeamRepository.Update(existingEntity);
            await TeamRepository.SaveChangesAsync();
        }
    }
}
