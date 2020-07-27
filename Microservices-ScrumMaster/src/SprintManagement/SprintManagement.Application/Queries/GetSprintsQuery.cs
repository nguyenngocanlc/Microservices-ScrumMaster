using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintsQuery : IRequest<IEnumerable<SprintResponse>>
    {
        public string ProjectId { get; set; }

        public GetSprintsQuery(string projectId)
        {
            ProjectId = projectId;
        }
    }
}
