using MediatR;
using SprintManagement.Application.Mapper;
using SprintManagement.Application.Queries;
using SprintManagement.Application.Responses;
using SprintManagement.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SprintManagement.Application.Handlers
{
    public class GetSprintHandler : IRequestHandler<GetSprintQuery, SprintResponse>
    {

        private readonly ISprintRepository _sprintRepository;

        public GetSprintHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository ?? throw new ArgumentNullException(nameof(sprintRepository));
        }

        public async Task<SprintResponse> Handle(GetSprintQuery request, CancellationToken cancellationToken)
        {
            var sprintList = await _sprintRepository.GetSprint(request.Id);

            var sprintResponseList = SprintMapper.Mapper.Map<SprintResponse>(sprintList);
            return sprintResponseList;
        }
    }
}
