using TaskIssue.API.Data.Interfaces;
using MongoDB.Driver;
using TaskIssue.API.Entities;
using TaskIssue.API.Settings;

namespace TaskIssue.API.Data
{
    public class TaskIssueContext : ITaskIssueContext
    {
        public TaskIssueContext(ITaskIssueDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Issues = database.GetCollection<Issue>(settings.CollectionName);
            //TaskIssueContextSeed.SeedData(Issues);
        }
        public IMongoCollection<Issue> Issues { get; }
    }
}
