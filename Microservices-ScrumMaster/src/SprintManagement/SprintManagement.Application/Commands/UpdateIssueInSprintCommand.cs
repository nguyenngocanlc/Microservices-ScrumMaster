using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Commands
{
    public class UpdateIssueInSprintCommand : IRequest<SprintDetailResponse>
    {
        public int Id { get; set; }
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
    }
}
