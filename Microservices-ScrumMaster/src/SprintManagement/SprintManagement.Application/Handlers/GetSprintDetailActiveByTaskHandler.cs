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
    public class GetSprintDetailActiveByTaskHandler : IRequestHandler<GetSprintDetailActiveByTaskIdQuery, IEnumerable<SprintDetailResponse>>
    {

        private readonly ISprintDetailRepository _sprintDetailRepository;

        public GetSprintDetailActiveByTaskHandler(ISprintDetailRepository sprintRepository)
        {
            _sprintDetailRepository = sprintRepository ?? throw new ArgumentNullException(nameof(sprintRepository));
        }

        public async Task<IEnumerable<SprintDetailResponse>> Handle(GetSprintDetailActiveByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var sprintDetailLists = await _sprintDetailRepository.GetSprintDetailActiveByTaskId(request.SprintId, request.TaskId);

            var sprintDetailResponses = SprintMapper.Mapper.Map<IEnumerable<SprintDetailResponse>>(sprintDetailLists);
            return sprintDetailResponses;
        }
    }
}
