using Microsoft.EntityFrameworkCore;
namespace Project.Grpc.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public virtual DbSet<Entities.Project> Projects { get; set; }
    }
}
