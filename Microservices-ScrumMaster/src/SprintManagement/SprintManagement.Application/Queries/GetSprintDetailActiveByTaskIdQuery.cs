using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintDetailActiveByTaskIdQuery : IRequest<IEnumerable<SprintDetailResponse>>
    {
        public int SprintId { get; set; }
        public string TaskId { get; set; }

        public GetSprintDetailActiveByTaskIdQuery(int sprintId,string taskId)
        {
            SprintId = sprintId;
            TaskId = taskId;
        }
    }
}
