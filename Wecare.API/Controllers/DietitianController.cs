using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/dietitian")]
    [ApiController]
    [Authorize]
    public class DietitianController : ControllerBase
    {
        private readonly IDietitianService _service;
        private readonly IMapper _mapper;

        public DietitianController(IDietitianService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}