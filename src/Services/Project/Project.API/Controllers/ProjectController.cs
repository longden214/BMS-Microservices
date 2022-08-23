using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.API.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectRepository repository, ILogger<ProjectController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Project>>> GetProjects()
        {
            var projects = await _repository.GetProjects();
            return Ok(projects);
        }

        [HttpGet("{name}", Name = "GetProjectsByName")]
        [ProducesResponseType(typeof(IEnumerable<Entities.Project>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Project>>> GetProjectsByName(string name)
        {
            var projects = await _repository.GetProjectsByName(name);

            return Ok(projects);
        }

        [HttpGet("GetProjectById/{id}", Name = "GetProjectById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Entities.Project), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Project>> GetProjectById(int id)
        {
            var project = await _repository.GetProjectById(id);

            if (project == null)
            {
                _logger.LogError($"Project with id: {id}, not found.");

                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Entities.Project), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Project>> CreateProject(Entities.Project project)
        {
             await _repository.CreateProject(project);

            return CreatedAtRoute("GetProjectById", new { id = project.ProjectId }, project);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProject([FromBody] Entities.Project project)
        {
            var obj = await _repository.GetProjectById(project.ProjectId);

            if (obj == null) return NotFound();

            return Ok(await _repository.UpdateProject(project));
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var obj = await _repository.GetProjectById(id);

            if (obj == null) return NotFound();

            return Ok(await _repository.DeleteProject(id));
        }

    }
}
