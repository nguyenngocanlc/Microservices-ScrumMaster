using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintDetailActivesQuery : IRequest<IEnumerable<SprintDetailResponse>>
    {
        public int SprintId { get; set; }

        public GetSprintDetailActivesQuery(int sprintId)
        {
            SprintId = sprintId;
        }
    }
}
