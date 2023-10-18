using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Dtos.Team;

public record TeamCreate(string Name,List<int> Employees);