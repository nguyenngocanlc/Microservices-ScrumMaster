using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskIssue.API.Settings
{
    public interface ITaskIssueDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
