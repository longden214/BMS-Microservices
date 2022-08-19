using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        protected readonly ProjectContext _context;

        public ProjectRepository(ProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Entities.Project> CreateProject(Entities.Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project =  _context.Projects.Where(p => p.ProjectId == id).FirstOrDefault();

            _context.Remove(project);
            await _context.SaveChangesAsync();

            return project.IsDeleted;
        }

        public async Task<Entities.Project> GetProjectById(int id)
        {
            return await _context.Projects.AsNoTracking().FirstAsync(p => p.ProjectId == id);
        }

        public async Task<IEnumerable<Entities.Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IEnumerable<Entities.Project>> GetProjectsByName(string name)
        {
            return await _context.Projects.Where(p => p.ProjectName.Equals(name)).ToListAsync();
        }

        public async Task<bool> UpdateProject(Entities.Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            var updateResult = await _context.SaveChangesAsync();

            return updateResult > 0;
        }
    }
}
