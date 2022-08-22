using Report.Application.Features.Reports.Queries.ViewModel;
using System.Collections.Generic;
using MediatR;
using System;

namespace Report.Application.Features.Reports.Queries.GetReportsById
{
    public class GetReportsByIdQuery : IRequest<List<ReportListVM>>
    {
        public int Id { get; set; }

        public GetReportsByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
