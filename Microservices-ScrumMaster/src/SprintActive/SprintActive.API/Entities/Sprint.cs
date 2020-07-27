using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Entities
{
    public class Sprint
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public int IssueOpen { get; set; }
        public int IssueCompleted { get; set; }
        public bool? Completed { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public bool? Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<SprintDetail> SprintDetails { get; set; }
    }
}
