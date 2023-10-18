using Courseproject.Common.Dtos.Team;
using Courseproject.Common.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courseproject.Common.Interface
{
    public interface ITeamService
    {
        Task<int> CreateTeamAsync(TeamCreate teamCreate);
        Task UpdateTeamAsync(TeamUpdate teamUpdate);
        Task<List<TeamGet>> GetTeamsAsync();
        Task<TeamGet> GetTeamAsync(int id);
        Task DeleteTeamAsync(TeamDelete teamDelete);
    }
}
