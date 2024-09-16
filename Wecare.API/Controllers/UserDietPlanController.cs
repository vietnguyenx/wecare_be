using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/userplan")]
    [ApiController]
    [Authorize]
    public class UserDietPlanController : ControllerBase
    {
        private readonly IUserDietPlanService _service;
        private readonly IMapper _mapper;

        public UserDietPlanController(IUserDietPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
