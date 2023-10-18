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
    public class AddressUpdateValidator : AbstractValidator<AddressUpdate>
    {
        public AddressUpdateValidator()
        {
            RuleFor(addressUpdate => addressUpdate.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(addressUpdate => addressUpdate.City).NotEmpty().MaximumLength(100);
            RuleFor(addressUpdate => addressUpdate.Street).NotEmpty().MaximumLength(100);
            RuleFor(addressUpdate => addressUpdate.Zip).NotEmpty().MaximumLength(16);
            RuleFor(addressUpdate => addressUpdate.Phone).MaximumLength(32);
        }
    }
}
