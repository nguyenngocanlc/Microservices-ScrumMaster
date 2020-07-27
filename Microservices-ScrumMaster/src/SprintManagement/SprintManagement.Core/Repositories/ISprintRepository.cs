using SprintManagement.Core.Entities;
using SprintManagement.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SprintManagement.Core.Repositories
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Task<Sprint> GetSprint(int id);     
        Task<Sprint> GetSprintActive(string projectId);
        Task<IEnumerable<Sprint>> GetSprints(string projectId);
        Task<IEnumerable<Sprint>> GetSprintCompleteds(string projectId);
    }
}
