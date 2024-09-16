using AutoMapper;
using Wecare.API.RequestModel;
using Wecare.Repositories.Data.Entities;
using Wecare.Services.Model;

namespace Wecare.API.Tools.Mapping
{
    public partial class Mapper : Profile
    {
        public void DietitianMapping()
        {
            CreateMap<Dietitian, DietitianModel>().ReverseMap();
            CreateMap<DietitianModel, DietitianRequest>().ReverseMap();
        }
    }
}
