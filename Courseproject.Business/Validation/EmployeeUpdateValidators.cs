using Courseproject.Common.Dtos.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Validation
{
    public class EmployeeUpdateValidators : AbstractValidator<EmployeeUpdate>
    {
        public EmployeeUpdateValidators()
        {
            RuleFor(employeeUpdate => employeeUpdate.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(employeeUpdate => employeeUpdate.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
