using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.API.RequestModel;
using Wecare.API.ResponseModel;
using Wecare.API.Tools.Constant;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/menudietplan")]
    [ApiController]
    //[Authorize]
    public class MenuDietPlanController : ControllerBase
    {
        private readonly IMenuDietPlanService _service;
        private readonly IMapper _mapper;

        public MenuDietPlanController(IMenuDietPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all-by-menu-id/{menuId}")]
        public async Task<IActionResult> GetAllById(Guid menuId)
        {
            try
            {
                if (menuId == Guid.Empty)
                {
                    return Ok("Must be input menu id");
                }
                List<MenuDietPlanModel> menuDietPlans = await _service.GetAllByMenuId(menuId);
                return menuDietPlans switch
                {
                    null => Ok(new ItemListResponse<MenuDietPlanModel>(ConstantMessage.Fail)),
                    not null => Ok(new ItemListResponse<MenuDietPlanModel>(ConstantMessage.Success, menuDietPlans))
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddMenuDietPlan(MenuDietPlanRequest diet)
        {
            try
            {
                var isDiet = await _service.Add(_mapper.Map<MenuDietPlanModel>(diet));

                return isDiet switch
                {
                    true => Ok(new BaseResponse(isDiet, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isDiet, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete")]
        public async Task<IActionResult> Delete(Guid idDietPlan, Guid idMenu)
        {
            try
            {
                if (idMenu != Guid.Empty && idDietPlan != Guid.Empty)
                {
                    var isDiet = await _service.Delete(idDietPlan, idMenu);

                    return isDiet switch
                    {
                        true => Ok(new BaseResponse(isDiet, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isDiet, ConstantMessage.Fail))
                    };
                }
                else
                {
                    return BadRequest("It's not empty");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
