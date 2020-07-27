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
    public class SprintDetailRepository : Repository<SprintDetail>, ISprintDetailRepository
    {
        public SprintDetailRepository(SprintContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SprintDetail>> GetSprintDetailByTaskId(int sprintId, string taskId)
        {
            var sprintDetails = await _dbContext.SprintDetails
                     .Where(o => o.SprintId == sprintId
                        && o.TaskId.Equals(taskId)                       
                     )
                     .ToListAsync();

            return sprintDetails;
        }
        public async Task<IEnumerable<SprintDetail>> GetSprintDetailActiveByTaskId(int sprintId, string taskId)
        {
            var sprintDetails = await _dbContext.SprintDetails
                     .Where(o => o.SprintId == sprintId
                        && o.TaskId.Equals(taskId)
                        && o.Active == true
                     )
                     .ToListAsync();

            return sprintDetails;
        }
        public async Task<IEnumerable<SprintDetail>> GetSprintDetails(int sprintId)
        {
            var sprintDetails = await _dbContext.SprintDetails
                     .Where(o => o.SprintId == sprintId)
                     .ToListAsync();

            return sprintDetails;
        }
        public async Task<IEnumerable<SprintDetail>> GetSprintDetailActives(int sprintId)
        {
            var sprintDetails = await _dbContext.SprintDetails
                     .Where(o => o.SprintId == sprintId && o.Active == true)
                     .ToListAsync();

            return sprintDetails;
        }

        public async Task Create(SprintDetail sprintDetail)
        {
            _dbContext.SprintDetails.Add(sprintDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(SprintDetail sprintDetail)
        {
            var result = _dbContext.SprintDetails.Update(sprintDetail);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var detail = _dbContext.SprintDetails.Find(id);
            if (detail == null) return false;
            _dbContext.SprintDetails.Remove(detail);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
