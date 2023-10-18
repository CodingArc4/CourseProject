using Courseproject.Common.Dtos.Job;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Validation
{
    public class JobUpdateValidator : AbstractValidator<JobUpdate>
    {
        public JobUpdateValidator()
        {
            RuleFor(jobUpdate => jobUpdate.Name).NotEmpty().MaximumLength(50);
            RuleFor(JobUpdate => JobUpdate.Description).NotEmpty().MaximumLength(250);
        }
    }
}
