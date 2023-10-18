using Courseproject.Common.Dtos.Job;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Validation
{
    public class JobCreateValidator : AbstractValidator<JobCreate>
    {
        public JobCreateValidator()
        {
            RuleFor(jobCreate => jobCreate.Name).NotEmpty().MaximumLength(50);
            RuleFor(JobCreate => JobCreate.Description).NotEmpty().MaximumLength(250);
        }
    }
}
