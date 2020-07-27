using AutoMapper;
using EventBusRabbitMQ.Events;
using SprintActive.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SprintActive.API.Mapping
{
    public class SprintActiveMapping : Profile
    {
        public SprintActiveMapping()
        {
            CreateMap<SprintDetail, UpdateIssueInSprintEvent>().ReverseMap();
        }
    }
}
