using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/dietplan")]
    [ApiController]
    [Authorize]
    public class DietPlanController : ControllerBase
    {
        private readonly IDietPlanService _service;
        private readonly IMapper _mapper;

        public DietPlanController(IDietPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
