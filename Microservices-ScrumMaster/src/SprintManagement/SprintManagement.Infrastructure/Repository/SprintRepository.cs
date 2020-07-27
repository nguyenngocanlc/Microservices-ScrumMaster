using Microsoft.EntityFrameworkCore;
using SprintManagement.Core.Entities;
using SprintManagement.Core.Repositories;
using SprintManagement.Infrastructure.Data;
using SprintManagement.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintManagement.Infrastructure.Repository
{
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository(SprintContext dbContext) : base(dbContext)
        {
        }
        public async Task<Sprint> GetSprint(int id)
        {
            var sprint = await _dbContext.Sprints.FindAsync(id);
            return sprint;
        }
        public async Task<Sprint> GetSprintActive(string projectId)
        {
            var sprint = await _dbContext.Sprints
                   .Where(o => o.ProjectId == projectId
                        && o.Active == true
                        && o.Completed == false)
                   .Include(c => c.SprintDetails)
                   .FirstOrDefaultAsync();
            return sprint;
        }
        public async Task<IEnumerable<Sprint>> GetSprints(string projectId)
        {
            var sprints = await _dbContext.Sprints
                  .Where(o => o.ProjectId == projectId
                        && o.Completed == false)
                   .ToListAsync();

            return sprints;
        }
        public async Task<IEnumerable<Sprint>> GetSprintCompleteds(string projectId)
        {
            var sprints = await _dbContext.Sprints
                  .Where(o => o.ProjectId == projectId
                        && o.Completed == true)
                   .ToListAsync();

            return sprints;
        }
    }
}