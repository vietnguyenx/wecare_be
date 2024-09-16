using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        private readonly IMapper _mapper;

        public MenuController(IMenuService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
