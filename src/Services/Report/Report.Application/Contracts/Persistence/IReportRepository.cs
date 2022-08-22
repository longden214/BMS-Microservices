using Report.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.Application.Contracts.Persistence
{
    public interface IReportRepository : IAsyncRepository<ReportModel>
    {
        Task<IEnumerable<ReportModel>> GetReportByName(string userName);
        Task<bool> UpdateReportByStatus(int id);
    }
}
