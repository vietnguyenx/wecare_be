using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/healthmetric")]
    [ApiController]
    [Authorize]
    public class HealthMetricController : ControllerBase
    {
        private readonly IHealthMetricService _service;
        private readonly IMapper _mapper;

        public HealthMetricController(IHealthMetricService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
