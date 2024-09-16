using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/menudish")]
    [ApiController]
    [Authorize]
    public class MenuDishController : ControllerBase
    {
        private readonly IMenuDishService _service;
        private readonly IMapper _mapper;

        public MenuDishController(IMenuDishService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
