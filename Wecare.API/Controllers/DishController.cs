using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/dish")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;
        private readonly IMapper _mapper;
        
        public DishController(IDishService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
    }
}
