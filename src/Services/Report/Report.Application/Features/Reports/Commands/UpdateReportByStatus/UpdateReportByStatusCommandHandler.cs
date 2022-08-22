using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Report.Application.Contracts.Persistence;
using Report.Application.Exceptions;
using Report.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.UpdateReportByStatus
{
    public class UpdateReportByStatusCommandHandler : IRequestHandler<UpdateReportByStatusCommand>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReportByStatusCommandHandler> _logger;

        public UpdateReportByStatusCommandHandler(IReportRepository repository, IMapper mapper, ILogger<UpdateReportByStatusCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateReportByStatusCommand request, CancellationToken cancellationToken)
        {
            var reportToUpdate = await _repository.GetByIdAsync(request.Id);

            if (reportToUpdate == null)
            {
                throw new NotFoundException(nameof(ReportModel), request.Id);
            }

            await _repository.UpdateReportByStatus(reportToUpdate.Id);

            _logger.LogInformation($"Report {reportToUpdate.Id} is successfully updated status.");

            return Unit.Value;
        }
    }
}
