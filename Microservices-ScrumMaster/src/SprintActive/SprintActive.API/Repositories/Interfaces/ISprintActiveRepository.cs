using SprintActive.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Repositories.Interfaces
{
    public interface ISprintActiveRepository
    {
        Task<Sprint> GetSprintActive(string projectId);
        Task<Sprint> GetSprintDetailActives(int sprintId);
        Task<Sprint> UpdateSprintDetailActives(Sprint sprint);
        Task<bool> DeleteSprintDetailActives(int sprintId);
    }
}
