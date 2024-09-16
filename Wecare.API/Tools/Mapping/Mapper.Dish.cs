using AutoMapper;
using Wecare.API.RequestModel;
using Wecare.API.SearchModel;
using Wecare.Repositories.Data.Entities;
using Wecare.Services.Model;

namespace Wecare.API.Tools.Mapping
{
    public partial class Mapper : Profile
    {
        public void DishMapping()
        {
            CreateMap<Dish, DishModel>().ReverseMap();
            CreateMap<DishModel, DishRequest>().ReverseMap();
            CreateMap<DishSearchRequest, DishModel>().ReverseMap();
        }
    }
}
