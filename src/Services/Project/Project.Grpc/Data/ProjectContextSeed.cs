using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Grpc.Data
{
    public class ProjectContextSeed
    {
        public static async Task SeedAsync(ProjectContext projectContext, ILogger<ProjectContextSeed> logger)
        {
            if (!projectContext.Projects.Any())
            {
                projectContext.Projects.AddRange(GetPreconfiguredOrders());
                await projectContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ProjectContext).Name);
            }
        }

        private static IEnumerable<Entities.Project> GetPreconfiguredOrders()
        {
            return new List<Entities.Project>
            {
                new Entities.Project() 
                {
                    ProjectName = "BMS",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Status = 1,
                    IsDeleted = false
                },
                new Entities.Project()
                {
                    ProjectName = "BIM",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Status = 0,
                    IsDeleted = false
                }
            };
        }
    }
}
