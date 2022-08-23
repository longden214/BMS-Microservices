using Project.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Report.API.GrpcServices
{
    public class ProjectGrpcService
    {
        private readonly ProjectProtoService.ProjectProtoServiceClient _projectProtoService;

        public ProjectGrpcService(ProjectProtoService.ProjectProtoServiceClient projectProtoService)
        {
            _projectProtoService = projectProtoService ?? throw new ArgumentNullException(nameof(projectProtoService));
        }
        public async Task<ProjectModel> GetProject(int id)
        {
            var projectRequest = new GetProjectByIdRequest { Id = id };

            return await _projectProtoService.GetProjectByIdAsync(projectRequest);
        }
    }
}
