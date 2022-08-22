using MediatR;
using Report.Application.Features.Reports.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Queries.GetReportsByName
{
    public class GetReportsByNameQuery : IRequest<List<ReportListVM>>
    {
        public string name { get; set; }

        public GetReportsByNameQuery(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
