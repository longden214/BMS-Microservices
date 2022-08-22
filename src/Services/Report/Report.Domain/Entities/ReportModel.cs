using Report.Domain.Common;
using System;

namespace Report.Domain.Entities
{
    public class ReportModel : EntityBase
    {
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
