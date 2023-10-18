using AutoMapper;
using Courseproject.Business.Validation;
using Courseproject.Common.Dtos.Employee;
using Courseproject.Common.Interface;
using Courseproject.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace Courseproject.Business.Service
{
    internal class EmployeeService : IEmployeeService
    {
        private IGenericRepository<Employee> EmployeeRepository { get; }
        public IGenericRepository<Job> JobRepository { get; }
        public IGenericRepository<Address> AddressRepository { get; }
        private IMapper Mapper { get; }

        private EmployeeCreateValidator EmployeeCreateValidator { get; }
        private EmployeeUpdateValidators EmployeeUpdateValidator { get; }

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Job> jobRepository,
            IGenericRepository<Address> addressRepository, IMapper mapper, 
            EmployeeUpdateValidators employeeUpdateValidator,EmployeeCreateValidator employeeCreateValidator)
        {
            EmployeeRepository = employeeRepository;
            JobRepository = jobRepository;
            AddressRepository = addressRepository;
            Mapper = mapper;
            EmployeeUpdateValidator = employeeUpdateValidator;
            EmployeeCreateValidator = employeeCreateValidator;
        }

        public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
        {
            await EmployeeCreateValidator.ValidateAndThrowAsync(employeeCreate);

            var address = await AddressRepository.getByIdAsync(employeeCreate.AddressId);
            var job = await JobRepository.getByIdAsync(employeeCreate.JobId);
            var entity = Mapper.Map<Employee>(employeeCreate);
            entity.Addresses = address;
            entity.Jobs = job;
            await EmployeeRepository.InsertAsync(entity);
            await EmployeeRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
        {
            var entity = await EmployeeRepository.getByIdAsync(employeeDelete.id);
            EmployeeRepository.Delete(entity);
            await EmployeeRepository.SaveChangesAsync();

        }

        public async Task<EmployeeDetails> GetEmployeeAsync(int id)
        {
            var entity = await EmployeeRepository.getByIdAsync(id, (employee) => employee.Addresses, (employee) => employee.Jobs, (employee) => employee.Teams);
            return Mapper.Map<EmployeeDetails>(entity);
        }

        public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
        {
            await EmployeeUpdateValidator.ValidateAndThrowAsync(employeeUpdate);

            var address = await AddressRepository.getByIdAsync(employeeUpdate.AddressId);
            var job = await JobRepository.getByIdAsync(employeeUpdate.JobId);
            var entity = Mapper.Map<Employee>(employeeUpdate);
            entity.Addresses = address;
            entity.Jobs = job;
            EmployeeRepository.Update(entity);
            await EmployeeRepository.SaveChangesAsync();
        }

        public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter)
        {
            Expression<Func<Employee, bool>> firstNameFilter = (employee) => employeeFilter.FirstName == null ? true :
            employee.FirstName.StartsWith(employeeFilter.FirstName);
            Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeeFilter.LastName == null ? true :
            employee.LastName.StartsWith(employeeFilter.LastName);
            Expression<Func<Employee, bool>> jobfilter = (employee) => employeeFilter.Job == null ? true :
            employee.Jobs.Name.StartsWith(employeeFilter.Job);
            Expression<Func<Employee, bool>> teamFilter = (employee) => employeeFilter.Team == null ? true :
            employee.Teams.Any(team => team.Name.StartsWith(employeeFilter.Team));

            var entities = await EmployeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
            {
            firstNameFilter, lastNameFilter, jobfilter, teamFilter
            }, employeeFilter.Skip, employeeFilter.Take,
            (employee) => employee.Addresses, (employee) => employee.Jobs, (employee) => employee.Teams);

            return Mapper.Map<List<EmployeeList>>(entities);
        }
    }
}
 