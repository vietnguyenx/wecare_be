using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
    }
}
