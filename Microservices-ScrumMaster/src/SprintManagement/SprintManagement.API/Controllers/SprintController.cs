using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SprintManagement.Application.Commands;
using SprintManagement.Application.Queries;
using SprintManagement.Application.Responses;

namespace SprintManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly IMediator _mediator;       

        public SprintController(IMediator mediator, ILogger<SprintController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));          
        }

        [HttpGet]
        [ProducesResponseType(typeof(SprintResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SprintResponse>> GetSprint(int id)
        {
            var query = new GetSprintQuery(id);
            var sprint = await _mediator.Send(query);
            return Ok(sprint);
        }

        [Route("[action]/{projectId}")]
        [HttpGet]
        [ProducesResponseType(typeof(SprintResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SprintResponse>> GetSprintActive(string projectId)
        {
            var query = new GetSprintActiveQuery(projectId);
            var sprint = await _mediator.Send(query);
            return Ok(sprint);
        }

        [Route("[action]/{projectId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SprintResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SprintResponse>>> GetSprints(string projectId)
        {
            var query = new GetSprintsQuery(projectId);
            var sprints = await _mediator.Send(query);
            return Ok(sprints);
        }

        [Route("[action]/{projectId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SprintResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SprintResponse>>> GetSprintCompleteds(string projectId)
        {
            var query = new GetSprintCompletedsQuery(projectId);
            var sprints = await _mediator.Send(query);
            return Ok(sprints);
        }

        //Added for testing purpose
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(SprintDetailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIssueInSprintCommand([FromBody] UpdateIssueInSprintCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Route("[action]/{sprintId}")]
        [HttpGet]
        [ProducesResponseType(typeof(SprintResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SprintDetailResponse>> GetSprintDetailActives(int sprintId)
        {
            var query = new GetSprintDetailActivesQuery(sprintId);
            var sprint = await _mediator.Send(query);
            return Ok(sprint);
        }
    }
}
