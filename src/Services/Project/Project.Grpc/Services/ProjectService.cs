using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Project.Grpc.Protos;
using Project.Grpc.Repositories;
using System;
using System.Threading.Tasks;

namespace Project.Grpc.Services
{
    public class ProjectService : ProjectProtoService.ProjectProtoServiceBase
    {
        private readonly IProjectRepository _repository;
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, ILogger<ProjectService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task GetProjects(GetProjectsRequest request, IServerStreamWriter<ProjectModel> responseStream, ServerCallContext context)
        {
            var projects = await _repository.GetProjects();

            foreach (var project in projects)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);

                await Task.Delay(1000);
                await responseStream.WriteAsync(projectModel);
            }
        }

        public override async Task GetProjectsByName(GetProjectsByNameRequest request, IServerStreamWriter<ProjectModel> responseStream, ServerCallContext context)
        {
            var projects = await _repository.GetProjectsByName(request.Name);

            foreach (var project in projects)
            {
                var projectModel = _mapper.Map<ProjectModel>(project);

                await Task.Delay(1000);
                await responseStream.WriteAsync(projectModel);
            }
        }

        public override async Task<ProjectModel> GetProjectById(GetProjectByIdRequest request, ServerCallContext context)
        {
            var project = await _repository.GetProjectById(request.Id);

            if (project == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Project with ProjectId = {request.Id} is not found."));
            }

            var projectModel = _mapper.Map<ProjectModel>(project);

            return projectModel;
        }

        public override async Task<ProjectModel> CreateProject(CreateProjectRequest request, ServerCallContext context)
        {
            var project = _mapper.Map<Entities.Project>(request.Project);

            await _repository.CreateProject(project);

            _logger.LogInformation("Project is successfully create. ProjectName : {projectName}", project.ProjectName);

            var projectModel = _mapper.Map<ProjectModel>(project);

            return projectModel;
        }

        public override async Task<UpdateProjectResponse> UpdateProject(UpdateProjectRequest request, ServerCallContext context)
        {
            var project = _mapper.Map<Entities.Project>(request.Project);

            var updated = await _repository.UpdateProject(project);

            var response = new UpdateProjectResponse
            {
                Success = updated
            };

            return response;
        }

        public override async Task<DeleteProjectResponse> DeleteProject(DeleteProjectRequest request, ServerCallContext context)
        {

            var deleted = await _repository.DeleteProject(request.Id);

            var response = new DeleteProjectResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
