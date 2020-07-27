using Newtonsoft.Json;
using SprintActive.API.Data.Interfaces;
using SprintActive.API.Entities;
using SprintActive.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Repositories
{
    public class SprintActiveRepository : ISprintActiveRepository
    {
        private readonly ISprintActiveContext _context;
        public SprintActiveRepository(ISprintActiveContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Sprint> GetSprintActive(string projectId)
        {
            var sprint = await _context
                                .Redis
                                .StringGetAsync(projectId);
            if (sprint.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Sprint>(sprint);
        }

        public async Task<Sprint> GetSprintDetailActives(int sprintId)
        {
            var sprint = await _context
                                 .Redis
                                 .StringGetAsync(sprintId.ToString());
            if (sprint.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Sprint>(sprint);
        }

        public async Task<Sprint> UpdateSprintDetailActives(Sprint sprint)
        {

            var updated = await _context
                              .Redis
                              .StringSetAsync(sprint.Id.ToString(), JsonConvert.SerializeObject(sprint));
            if (!updated)
            {
                return null;
            }
            return await GetSprintDetailActives(sprint.Id);
        }

        public async Task<bool> DeleteSprintDetailActives(int sprintId)
        {
            return await _context
                            .Redis
                            .KeyDeleteAsync(sprintId.ToString());
        }
    }
}
