using MediatR;
using Report.Application.Features.Reports.Queries.ViewModel;
using System.Collections.Generic;

namespace Report.Application.Features.Reports.Queries.GetReportList
{
    public class GetReportListQuery : IRequest<List<ReportListVM>>
    {
        public GetReportListQuery() { }
    }
}
