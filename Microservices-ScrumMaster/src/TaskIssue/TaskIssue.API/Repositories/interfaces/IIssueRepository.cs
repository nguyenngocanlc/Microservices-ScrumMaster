using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskIssue.API.Entities;

namespace TaskIssue.API.Repositories.interfaces
{
    public interface IIssueRepository
    {
        Task<IEnumerable<Issue>> GetIssues();
        Task<Issue> GetIssue(string id);
        Task<IEnumerable<Issue>> GetIssueBySummary(string summary);
        Task<IEnumerable<Issue>> GetIssueByLabel(string label);
        Task<IEnumerable<Issue>> GetIssueBySprint(string sprintId);
        Task<IEnumerable<Issue>> GetIssueByAssigneeUserId(string userId);
        Task<IEnumerable<Issue>> GetIssueByReporterUserId(string userId);

        Task Create(Issue issue);
        Task<bool> Update(Issue issue);
        Task<bool> Delete(string id);
    }
}
