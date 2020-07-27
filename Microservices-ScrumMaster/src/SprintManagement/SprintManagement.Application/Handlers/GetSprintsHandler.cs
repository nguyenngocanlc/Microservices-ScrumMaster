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
    public class GetSprintsHandler : IRequestHandler<GetSprintsQuery, IEnumerable<SprintResponse>>
    {

        private readonly ISprintRepository _sprintRepository;

        public GetSprintsHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository ?? throw new ArgumentNullException(nameof(sprintRepository));
        }

        public async Task<IEnumerable<SprintResponse>> Handle(GetSprintsQuery request, CancellationToken cancellationToken)
        {
            var sprintLists = await _sprintRepository.GetSprints(request.ProjectId);

            var sprintResponseList = SprintMapper.Mapper.Map<IEnumerable<SprintResponse>>(sprintLists);
            return sprintResponseList;
        }
    }
}
