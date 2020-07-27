using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SprintActive.API.Entities;
using SprintActive.API.Repositories.Interfaces;

namespace SprintActive.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SprintActiveController : ControllerBase
    {
        private readonly ISprintActiveRepository _repository;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<SprintActiveController> _logger;
        private readonly IMapper _mapper;

        public SprintActiveController(ISprintActiveRepository repository, EventBusRabbitMQProducer eventBus, IMapper mapper, ILogger<SprintActiveController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]/{projectId}")]
        [HttpGet]
        [ProducesResponseType(typeof(Sprint), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sprint>> GetSprintActive(string projectId)
        {
            var sprint = await _repository.GetSprintActive(projectId);
            return Ok(sprint);
        }

        [Route("[action]/{sprintId}")]
        [HttpGet]
        [ProducesResponseType(typeof(Sprint), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sprint>> GetSprintDetailActives(int sprintId)
        {
            var sprint = await _repository.GetSprintDetailActives(sprintId);
            return Ok(sprint);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sprint), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sprint>> UpdateSprintDetailActives([FromBody] Sprint sprint)
        {
            return Ok(await _repository.UpdateSprintDetailActives(sprint));
        }

        [HttpDelete("{sprintId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSprintDetailActives(int sprintId)
        {
            return Ok(await _repository.DeleteSprintDetailActives(sprintId));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateIssueInSprint([FromBody] SprintDetail sprintDetail)
        {
            // send UpdateIssueInSprintCommand event to rabbitMq 
            //sprintDetail must have TaskId,SprintId

            var sprint = await _repository.GetSprintDetailActives(sprintDetail.SprintId);
            if (sprint == null)
            {
                _logger.LogError("Sprint not exist with this user : {EventId}", sprintDetail.SprintId);
                return BadRequest();
            }

            var eventMessage = _mapper.Map<UpdateIssueInSprintEvent>(sprintDetail);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.SprintId = sprint.Id;

            try
            {
                _eventBus.PublishUpdateIssueInSprint(EventBusConstants.UpdateIssueInSprintQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "SprintActive");
                throw;
            }

            return Accepted();
        }
    }
}