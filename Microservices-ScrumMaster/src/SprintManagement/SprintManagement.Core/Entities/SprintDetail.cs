using SprintManagement.Core.Entities.Base;
using System;

namespace SprintManagement.Core.Entities
{
    public class SprintDetail : Entity
    {      
        public DateTime IssueDate { get; set; }
        public string TaskId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }        
    }
}
