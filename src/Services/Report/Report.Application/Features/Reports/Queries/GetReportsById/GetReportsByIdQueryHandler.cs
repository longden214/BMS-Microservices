using AutoMapper;
using MediatR;
using Report.Application.Contracts.Persistence;
using Report.Application.Features.Reports.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Queries.GetReportsById
{
    public class GetReportsByIdQueryHandler : IRequestHandler<GetReportsByIdQuery, ReportListVM>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public GetReportsByIdQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _repository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ReportListVM> Handle(GetReportsByIdQuery request, CancellationToken cancellationToken)
        {
            var reports = await _repository.GetByIdAsync(request.Id);

            return _mapper.Map<ReportListVM>(reports);
        }
    }
}
