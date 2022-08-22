using Microsoft.Extensions.Logging;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Persistence
{
    public class ReportContextSeed
    {
        public static async Task SeedAsync(ReportContext reportContext, ILogger<ReportContextSeed> logger)
        {
            if (!reportContext.Reports.Any())
            {
                reportContext.Reports.AddRange(GetPreconfiguredReports());
                await reportContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ReportContext).Name);
            }
        }

        private static IEnumerable<ReportModel> GetPreconfiguredReports()
        {
            return new List<ReportModel>
            {
                new ReportModel() {
                    WorkingDay = DateTime.Now,
                    WorkingHour = 8,
                    RateValue = 150,
                    Note = "Thực hành dự án Microservices",
                    UserId = "602d2149e773f2a3990b47f5",
                    ProjectId = 1,
                    Position = "Developer",
                    WorkingType = "offline",
                    Status = 1
                },
                new ReportModel() {
                    WorkingDay = DateTime.Now,
                    WorkingHour = 8,
                    RateValue = 200,
                    Note = "Viết API",
                    UserId = "602d2149e773f2a3990b47f6",
                    ProjectId = 2,
                    Position = "Developer",
                    WorkingType = "offline",
                    Status = 1
                }
            };
        }
    }
}
