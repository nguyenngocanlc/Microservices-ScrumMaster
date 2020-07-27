using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskIssue.API.Entities;
using TaskIssue.API.Repositories.interfaces;

namespace TaskIssue.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskIssueController : ControllerBase
    {
        private readonly IIssueRepository _repository;
        private readonly ILogger<TaskIssueController> _logger;

        public TaskIssueController(IIssueRepository repository, ILogger<TaskIssueController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Issue>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues()
        {
            var issues = await _repository.GetIssues();
            return Ok(issues);
        }

        [HttpGet("{id:length(128)}", Name = "GetIssue")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Issue>> GetIssue(string id)
        {
            var issue = await _repository.GetIssue(id);

            if (issue == null)
            {
                _logger.LogError($"Issue with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(issue);
        }

        [Route("[action]/{summary}")]
        [HttpGet]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueBySummary(string summary)
        {
            var issues = await _repository.GetIssueBySummary(summary);
            return Ok(issues);
        }

        [Route("[action]/{label}")]
        [HttpGet]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueByLabel(string label)
        {
            var issues = await _repository.GetIssueByLabel(label);
            return Ok(issues);
        }

        [Route("[action]/{sprintId}")]
        [HttpGet]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueBySprint(string sprintId)
        {
            var issues = await _repository.GetIssueBySprint(sprintId);
            return Ok(issues);
        }

        [Route("[action]/{userId}")]
        [HttpGet]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueByAssigneeUserId(string userId)
        {
            var issues = await _repository.GetIssueByAssigneeUserId(userId);
            return Ok(issues);
        }

        [Route("[action]/{userId}")]
        [HttpGet]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueByReporterUserId(string userId)
        {
            var issues = await _repository.GetIssueByReporterUserId(userId);
            return Ok(issues);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Issue>> CreateIssue([FromBody] Issue issue)
        {
            await _repository.Create(issue);

            return CreatedAtRoute("GetIssue", new { id = issue.Id }, issue);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Issue), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIssue([FromBody] Issue issue)
        {
            return Ok(await _repository.Update(issue));
        }

        [HttpDelete("{id:length(128)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteIssueById(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
