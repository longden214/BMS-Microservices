using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Report.Application.Contracts.Persistence;
using Report.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, int>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateReportCommandHandler> _logger;

        public CreateReportCommandHandler(IReportRepository repository, IMapper mapper, ILogger<CreateReportCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var reportEntity = _mapper.Map<ReportModel>(request);

            var newReport = await _repository.AddAsync(reportEntity);

            _logger.LogInformation($"Report {newReport.Id} is successfully created.");

            return newReport.Id;
        }
    }
}
