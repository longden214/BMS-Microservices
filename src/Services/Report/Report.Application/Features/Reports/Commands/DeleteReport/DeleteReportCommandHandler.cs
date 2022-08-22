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

namespace Report.Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand>
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteReportCommandHandler> _logger;

        public DeleteReportCommandHandler(IReportRepository repository, ILogger<DeleteReportCommandHandler> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var reportToDelete = await _repository.GetByIdAsync(request.Id);

            if (reportToDelete == null)
            {
                throw new NotFoundException(nameof(ReportModel), request.Id);
            }

            await _repository.DeleteAsync(reportToDelete);
            _logger.LogInformation($"Report {reportToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
