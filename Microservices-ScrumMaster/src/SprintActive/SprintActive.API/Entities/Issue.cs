using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Entities
{
    public class Issue
    {
        public string Id { get; set; }

        public string TypeId { get; set; }
 
        public string Summary { get; set; }
        public string Description { get; set; }
     
        public string Labels { get; set; }
        public decimal StoryPoint { get; set; }
      
        public string SprintId { get; set; }
        public string LinkedIssues { get; set; }
        public bool Flagged { get; set; }
       
        public string AssigneeUserId { get; set; }

        public string ReporterUserId { get; set; }
        public string Status { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
