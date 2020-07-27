using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintCompletedsQuery : IRequest<IEnumerable<SprintResponse>>
    {
        public string ProjectId { get; set; }

        public GetSprintCompletedsQuery(string projectId)
        {
            ProjectId = projectId;
        }
    }
}
