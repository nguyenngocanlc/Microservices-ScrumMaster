using MediatR;
using SprintManagement.Application.Commands;
using SprintManagement.Application.Responses;
using SprintManagement.Application.Mapper;
using SprintManagement.Core.Entities;
using SprintManagement.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SprintManagement.Application.Handlers
{
    public class UpdateIssueInSprintHandle : IRequestHandler<UpdateIssueInSprintCommand, SprintDetailResponse>
    {
        private readonly ISprintDetailRepository _repository;
        public UpdateIssueInSprintHandle(ISprintDetailRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }    
        public async Task<SprintDetailResponse> Handle(UpdateIssueInSprintCommand request, CancellationToken cancellationToken)
        {
            var sDetailEntity = SprintMapper.Mapper.Map<SprintDetail>(request);
            if (sDetailEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");            

            //process logic business
            //inactive all old task in sprint
            var oldSprintDetails = await _repository.GetSprintDetailByTaskId(request.SprintId, request.TaskId);
            foreach(var item in oldSprintDetails)
            {
                item.Active = false;
                item.UpdatedOn = DateTime.Now;
                item.UpdatedBy = request.UpdatedBy;
                await _repository.UpdateAsync(item);
            }

            //add new sprintdetail of task
            sDetailEntity.Active = true;
            sDetailEntity.CreatedOn = DateTime.Now;
            sDetailEntity.UpdatedOn = DateTime.Now;
            var newSprintDetail = await _repository.AddAsync(sDetailEntity);

            var sDetailResponse = SprintMapper.Mapper.Map<SprintDetailResponse>(newSprintDetail);
            return sDetailResponse;
        }
    }
}
