using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Business.Validation
{
    public class AddressCreateValidator : AbstractValidator<AddressCreate>
    {
        public AddressCreateValidator()
        {
            RuleFor(addressCreate => addressCreate.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(addressCreate => addressCreate.City).NotEmpty().MaximumLength(100);
            RuleFor(addressCreate => addressCreate.Street).NotEmpty().MaximumLength(100);
            RuleFor(addressCreate => addressCreate.Zip).NotEmpty().MaximumLength(16);
            RuleFor(addressCreate => addressCreate.Phone).MaximumLength(32);
        }
    }
}
