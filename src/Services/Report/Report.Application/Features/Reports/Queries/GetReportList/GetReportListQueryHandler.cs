using AutoMapper;
using MediatR;
using Report.Application.Contracts.Persistence;
using Report.Application.Features.Reports.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Queries.GetReportList
{
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, List<ReportListVM>>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public GetReportListQueryHandler(IReportRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ReportListVM>> Handle(GetReportListQuery request, CancellationToken cancellationToken)
        {
            var reports = await _repository.GetAllAsync();

            return _mapper.Map<List<ReportListVM>>(reports);
        }
    }
}
