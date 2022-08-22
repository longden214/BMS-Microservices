using Microsoft.EntityFrameworkCore;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using Report.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Infrastructure.Repositories
{
    public class ReportRepository : RepositoryBase<ReportModel>, IReportRepository
    {
        public ReportRepository(ReportContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ReportModel>> GetReportByName(string userName)
        {
            var reports = await _dbcontext.Reports
                            .Where(r => r.Position.Contains(userName))
                            .ToListAsync();

            return reports;
        }

        public async Task<bool> UpdateReportByStatus(int id)
        {
            var report = await _dbcontext.Reports.FirstOrDefaultAsync(r => r.Id == id);

            if (report.Status == 1)
            {
                report.Status = 0;
            }
            else
            {
                report.Status = 1;
            }

            _dbcontext.Entry(report).State = EntityState.Modified;
            int updResult = await _dbcontext.SaveChangesAsync();

            return updResult > 0;
        }
    }
}
