using SprintManagement.Core.Entities;
using SprintManagement.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SprintManagement.Core.Repositories
{
    public interface ISprintDetailRepository : IRepository<SprintDetail>
    {
        Task<IEnumerable<SprintDetail>> GetSprintDetailByTaskId(int sprintId, string taskId);
        Task<IEnumerable<SprintDetail>> GetSprintDetailActiveByTaskId(int sprintId, string taskId);
        Task<IEnumerable<SprintDetail>> GetSprintDetails(int sprintId);
        Task<IEnumerable<SprintDetail>> GetSprintDetailActives(int sprintId);
        Task Create(SprintDetail sprintDetail);
        Task<bool> Update(SprintDetail sprintDetail);
        Task<bool> Delete(int id);
    }
}
