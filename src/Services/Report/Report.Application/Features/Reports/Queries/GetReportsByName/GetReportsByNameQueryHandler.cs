using AutoMapper;
using MediatR;
using Report.Application.Contracts.Persistence;
using Report.Application.Features.Reports.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Queries.GetReportsByName
{
    public class GetReportsByNameQueryHandler : IRequestHandler<GetReportsByNameQuery, List<ReportListVM>>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public GetReportsByNameQueryHandler(IReportRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ReportListVM>> Handle(GetReportsByNameQuery request, CancellationToken cancellationToken)
        {
            var reports = await _repository.GetReportByName(request.name);

            return _mapper.Map<List<ReportListVM>>(reports);
        }
    }
}
