using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskIssue.API.Data.Interfaces;
using TaskIssue.API.Entities;
using TaskIssue.API.Repositories.interfaces;
using MongoDB.Driver;

namespace TaskIssue.API.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly ITaskIssueContext _context;

        public IssueRepository(ITaskIssueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Issue>> GetIssues()
        {
            return await _context
                             .Issues
                             .Find(p => true)
                             .ToListAsync();
        }

        public async Task<Issue> GetIssue(string id)
        {
            return await _context
                              .Issues
                              .Find(p => p.Id == id)
                              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueBySummary(string summary)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.ElemMatch(p => p.Summary, summary);

            return await _context
                          .Issues
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueByLabel(string label)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.ElemMatch(p => p.Labels, label);

            return await _context
                          .Issues
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueBySprint(string sprintId)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.ElemMatch(p => p.SprintId, sprintId);

            return await _context
                          .Issues
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueByAssigneeUserId(string userId)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.ElemMatch(p => p.AssigneeUserId, userId);

            return await _context
                          .Issues
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueByReporterUserId(string userId)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.ElemMatch(p => p.ReporterUserId, userId);

            return await _context
                          .Issues
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task Create(Issue issue)
        {
            await _context.Issues.InsertOneAsync(issue);
        }

        public async Task<bool> Update(Issue issue)
        {
            var updateResult = await _context.Issues.ReplaceOneAsync(filter: i => i.Id == issue.Id, replacement: issue);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Issue> filter = Builders<Issue>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Issues.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
      
    }
}
