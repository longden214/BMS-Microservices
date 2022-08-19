using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.API.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Entities.Project>> GetProjects();
        Task<IEnumerable<Entities.Project>> GetProjectsByName(string name);
        Task<Entities.Project> GetProjectById(int id);
        Task<Entities.Project> CreateProject(Entities.Project project);
        Task<bool> UpdateProject(Entities.Project project);
        Task<bool> DeleteProject(int id);
    }
}
