using TaskIssue.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System;

namespace TaskIssue.API.Data
{
    public class TaskIssueContextSeed
    {
        public static void SeedData(IMongoCollection<Issue> issueCollection)
        {
            bool existIssue = issueCollection.Find(p => true).Any();
            if (!existIssue)
            {
                issueCollection.InsertManyAsync(GetPreconfiguredIssues());
            }
        }

        private static IEnumerable<Issue> GetPreconfiguredIssues()
        {
            return new List<Issue>()
            {
                 new Issue()
                {
                     TypeId="Task",
                     Summary="First task demo",
                     Description="Description task detail",
                     Labels="WebPortal",
                     StoryPoint=3,
                     SprintId="1",
                     Status="Todo",
                     CreatedOn=DateTime.Now
                },
                 new Issue()
                {
                     TypeId="Bug",
                     Summary="First bug demo",
                     Description="Description bug detail",
                     Labels="WebPortal",
                     StoryPoint=3,
                     SprintId="1",
                     Status="Progress",
                     CreatedOn=DateTime.Now
                }
            };
        }
    }
}
