using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Model;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Address Addresses { get; set; } = default!;
    public Job Jobs { get; set; } = default!;
    public List<Team> Teams { get; set; } = default!;
}
