using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Project.Grpc.Protos;
using System;

namespace Project.Grpc.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Entities.Project, ProjectModel>().ReverseMap();
            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));
        }
    }
}
