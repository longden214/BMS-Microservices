using Microsoft.EntityFrameworkCore;
using Project.API.Entities;

namespace Project.API.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public virtual DbSet<Entities.Project> Projects { get; set; }
    }
}
