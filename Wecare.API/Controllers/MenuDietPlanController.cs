using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/userplan")]
    [ApiController]
    [Authorize]
    public class MenuDietPlanController : ControllerBase
    {
        private readonly IMenuDietPlanService _service;
        private readonly IMapper _mapper;

        public MenuDietPlanController(IMenuDietPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
