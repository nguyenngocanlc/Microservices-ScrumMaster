using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintDetailsQuery : IRequest<IEnumerable<SprintDetailResponse>>
    {
        public int SprintId { get; set; }

        public GetSprintDetailsQuery(int sprintId)
        {
            SprintId = sprintId;
        }
    }
}
