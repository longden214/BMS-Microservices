using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.UpdateReportByStatus
{
    public class UpdateReportByStatusCommand : IRequest
    {
        public int Id { get; set; }
    }
}
