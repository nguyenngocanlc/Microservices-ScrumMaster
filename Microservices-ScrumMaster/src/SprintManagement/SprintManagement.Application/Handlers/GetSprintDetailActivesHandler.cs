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
    public class GetSprintDetailActivesHandler : IRequestHandler<GetSprintDetailActivesQuery, IEnumerable<SprintDetailResponse>>
    {

        private readonly ISprintDetailRepository _sprintDetailRepository;

        public GetSprintDetailActivesHandler(ISprintDetailRepository sprintRepository)
        {
            _sprintDetailRepository = sprintRepository ?? throw new ArgumentNullException(nameof(sprintRepository));
        }

        public async Task<IEnumerable<SprintDetailResponse>> Handle(GetSprintDetailActivesQuery request, CancellationToken cancellationToken)
        {
            var sprintDetailLists = await _sprintDetailRepository.GetSprintDetailActives(request.SprintId);

            var sprintDetailResponses = SprintMapper.Mapper.Map<IEnumerable<SprintDetailResponse>>(sprintDetailLists);
            return sprintDetailResponses;
        }
    }
}
