using AutoMapper;
using Wecare.API.RequestModel;
using Wecare.API.SearchModel;
using Wecare.Repositories.Data.Entities;
using Wecare.Services.Model;

namespace Wecare.API.Tools.Mapping
{
    public partial class Mapper : Profile
    {
        public void HealthMetricMapping()
        {
            CreateMap<HealthMetric, HealthMetricModel>().ReverseMap();
            CreateMap<HealthMetricModel, HealthMetricRequest>().ReverseMap();
            CreateMap<HealthMetricSearchRequest, HealthMetricModel>().ReverseMap();
        }
    }
}
