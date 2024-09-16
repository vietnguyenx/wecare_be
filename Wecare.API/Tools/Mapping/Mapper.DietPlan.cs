using AutoMapper;
using Wecare.API.RequestModel;
using Wecare.API.SearchModel;
using Wecare.Repositories.Data.Entities;
using Wecare.Services.Model;

namespace Wecare.API.Tools.Mapping
{
    public partial class Mapper : Profile
    {
        public void DietPlanMapping()
        {
            CreateMap<DietPlan, DietPlanModel>().ReverseMap();
            CreateMap<DietPlanModel, DietPlanRequest>().ReverseMap();
            CreateMap<DietPlanSearchRequest, DietPlanModel>().ReverseMap();
        }
    }
}
