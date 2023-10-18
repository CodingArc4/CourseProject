using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Dtos.Team;

public record TeamUpdate(int id,string Name, List<int> Employees);