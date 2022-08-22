using AutoMapper;
using Report.Application.Features.Reports.Commands.CreateReport;
using Report.Application.Features.Reports.Commands.UpdateReport;
using Report.Application.Features.Reports.Queries.ViewModel;
using Report.Domain.Entities;

namespace Report.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ReportModel, ReportListVM>().ReverseMap();
            CreateMap<ReportModel, CreateReportCommand>().ReverseMap();
            CreateMap<ReportModel, UpdateReportCommand>().ReverseMap();
        }  
    }
}
