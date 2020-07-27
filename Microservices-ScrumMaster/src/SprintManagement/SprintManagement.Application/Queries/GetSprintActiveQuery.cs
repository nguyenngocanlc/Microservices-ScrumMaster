using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintActiveQuery : IRequest<SprintResponse>
    {
        public string ProjectId { get; set; }

        public GetSprintActiveQuery(string projectId)
        {
            ProjectId = projectId;
        }
    }
}
