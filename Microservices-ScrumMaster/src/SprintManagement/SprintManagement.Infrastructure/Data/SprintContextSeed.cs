using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SprintManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintManagement.Infrastructure.Data
{
    public class SprintContextSeed
    {
        public static async Task SeedAsync(SprintContext sprintContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // INFO: Run this if using a real database. Used to automaticly migrate docker image of sql server db.
                sprintContext.Database.Migrate();

                if (!sprintContext.Sprints.Any())
                {
                    sprintContext.Sprints.AddRange(GetPreconfiguredSprints());
                    await sprintContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 5)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<SprintContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(sprintContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Sprint> GetPreconfiguredSprints()
        {
            return new List<Sprint>()
            {
               new Sprint(){ ProjectId="1", Name ="Backlog", Description="Any task not in planning", Active=false, Completed=false, CreatedOn=DateTime.Now, UpdatedOn=DateTime.Now},
               new Sprint(){ ProjectId="1", Name ="Sprint 1", Description="First sprint", Active=false, Completed=false, CreatedOn=DateTime.Now, UpdatedOn=DateTime.Now}
            };
        }

    }
}
