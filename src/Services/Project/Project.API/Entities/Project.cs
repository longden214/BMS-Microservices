using System;

namespace Project.API.Entities
{
    public class Project
    {
        public int ProjectId { get; protected set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
