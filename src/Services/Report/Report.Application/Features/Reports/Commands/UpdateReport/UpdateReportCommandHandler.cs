using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Report.Application.Contracts.Persistence;
using Report.Application.Exceptions;
using Report.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReportCommandHandler> _logger;

        public UpdateReportCommandHandler(IReportRepository repository, IMapper mapper, ILogger<UpdateReportCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            var reportToUpdate = await _repository.GetByIdAsync(request.Id);

            if (reportToUpdate == null)
            {
                throw new NotFoundException(nameof(ReportModel), request.Id);
            }

            _mapper.Map(request, reportToUpdate, typeof(UpdateReportCommand), typeof(ReportModel));

            await _repository.UpdateAsync(reportToUpdate);

            _logger.LogInformation($"Report {reportToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
