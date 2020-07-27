using MediatR;
using SprintManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Queries
{
    public class GetSprintQuery : IRequest<SprintResponse>
    {
        public int Id { get; set; }

        public GetSprintQuery(int id)
        {
            Id = id;
        }
    }
}
