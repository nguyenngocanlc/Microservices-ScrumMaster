using MongoDB.Driver;
using TaskIssue.API.Entities;

namespace TaskIssue.API.Data.Interfaces
{
    public interface ITaskIssueContext
    {
        IMongoCollection<Issue> Issues { get; }
    }
}
