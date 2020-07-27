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
    public class GetSprintActiveHandler : IRequestHandler<GetSprintActiveQuery, SprintResponse>
    {

        private readonly ISprintRepository _sprintRepository;

        public GetSprintActiveHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository ?? throw new ArgumentNullException(nameof(sprintRepository));
        }

        public async Task<SprintResponse> Handle(GetSprintActiveQuery request, CancellationToken cancellationToken)
        {
            var sprintList = await _sprintRepository.GetSprintActive(request.ProjectId);

            var sprintResponseList = SprintMapper.Mapper.Map<SprintResponse>(sprintList);
            return sprintResponseList;
        }
    }
}
