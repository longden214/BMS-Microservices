using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime WorkingDay { get; set; }
        public int WorkingHour { get; set; }
        public int RateValue { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string Position { get; set; }
        public string WorkingType { get; set; }
        public int Status { get; set; }
    }
}
