using AutoMapper;
using EventBusRabbitMQ.Events;
using SprintManagement.Application.Commands;

namespace SprintManagement.API.Mapping
{
    public class SprintMapping : Profile
    {
        public SprintMapping()
        {
            CreateMap<UpdateIssueInSprintEvent, UpdateIssueInSprintCommand>().ReverseMap();
        }
    }
}
