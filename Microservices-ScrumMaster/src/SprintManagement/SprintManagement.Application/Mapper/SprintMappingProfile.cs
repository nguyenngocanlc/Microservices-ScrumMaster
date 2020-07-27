using AutoMapper;
using SprintManagement.Application.Commands;
using SprintManagement.Application.Responses;
using SprintManagement.Core.Entities;

namespace SprintManagement.Application.Mapper
{
    public class SprintMappingProfile : Profile
    {
        public SprintMappingProfile()
        {            
            CreateMap<Sprint, SprintResponse>().ReverseMap();
            CreateMap<SprintDetail, SprintDetailResponse>().ReverseMap();
            CreateMap<SprintDetail, UpdateIssueInSprintCommand>().ReverseMap();
            CreateMap<SprintDetailResponse, UpdateIssueInSprintCommand>().ReverseMap();
        }
    }
}
