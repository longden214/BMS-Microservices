using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.API.GrpcServices;
using Report.Application.Features.Reports.Commands.CreateReport;
using Report.Application.Features.Reports.Commands.DeleteReport;
using Report.Application.Features.Reports.Commands.UpdateReport;
using Report.Application.Features.Reports.Commands.UpdateReportByStatus;
using Report.Application.Features.Reports.Queries.GetReportList;
using Report.Application.Features.Reports.Queries.GetReportsById;
using Report.Application.Features.Reports.Queries.GetReportsByName;
using Report.Application.Features.Reports.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ProjectGrpcService _projectGrpcService;

        public ReportController(IMediator mediator, ProjectGrpcService projectGrpcService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _projectGrpcService = projectGrpcService ?? throw new ArgumentNullException(nameof(projectGrpcService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReportListVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ReportListVM>>> GetReports()
        {
            var query = new GetReportListQuery();
            var reports = await _mediator.Send(query);

            foreach (var item in reports)
            {
                var project = await _projectGrpcService.GetProject(item.ProjectId);

                item.ProjectName = project.ProjectName;
            }

            return Ok(reports);
        }

        [HttpGet("GetReportsByName/{name}", Name = "GetReportsByName")]
        [ProducesResponseType(typeof(IEnumerable<ReportListVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ReportListVM>>> GetReportsByName(string name)
        {
            var query = new GetReportsByNameQuery(name);
            var reports = await _mediator.Send(query);

            foreach (var item in reports)
            {
                var project = await _projectGrpcService.GetProject(item.ProjectId);

                item.ProjectName = project.ProjectName;
            }

            return Ok(reports);
        }

        [HttpGet("GetReportById/{id}", Name = "GetReportById")]
        [ProducesResponseType(typeof(IEnumerable<ReportListVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportListVM>> GetReportById(int id)
        {
            var query = new GetReportsByIdQuery(id);
            var report = await _mediator.Send(query);

            var project = await _projectGrpcService.GetProject(report.ProjectId);
            report.ProjectName = project.ProjectName;

            return Ok(report);
        }

        [HttpPost(Name = "CreateReport")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateReport([FromBody] CreateReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateReport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateReport([FromBody] UpdateReportCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateReportStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateReportStatus(int id)
        {
            var command = new UpdateReportByStatusCommand() { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteReport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteReport(int id)
        {
            var command = new DeleteReportCommand() { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
