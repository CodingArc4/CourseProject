using Courseproject.Common.Dtos.Employee;

namespace Courseproject.Common.Dtos.Teams;

public record TeamGet(int id,string Name,List<EmployeeList> Employees);