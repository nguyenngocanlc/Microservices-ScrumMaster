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
    public class GetSprintDetailsHandler : IRequestHandler<GetSprintDetailsQuery, IEnumerable<SprintDetailResponse>>
    {

        private readonly ISprintDetailRepository _sprintDetailRepository;

        public GetSprintDetailsHandler(ISprintDetailRepository sprintRepository)
        {
            _sprintDetailRepository = _sprintDetailRepository ?? throw new ArgumentNullException(nameof(_sprintDetailRepository));
        }

        public async Task<IEnumerable<SprintDetailResponse>> Handle(GetSprintDetailsQuery request, CancellationToken cancellationToken)
        {
            var sprintDetailLists = await _sprintDetailRepository.GetSprintDetails(request.SprintId);

            var sprintDetailResponses = SprintMapper.Mapper.Map<IEnumerable<SprintDetailResponse>>(sprintDetailLists);
            return sprintDetailResponses;
        }
    }
}
