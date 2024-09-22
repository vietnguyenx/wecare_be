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
    [Route("api/menudish")]
    [ApiController]
    //[Authorize]
    public class MenuDishController : ControllerBase
    {
        private readonly IMenuDishService _service;
        private readonly IMapper _mapper;

        public MenuDishController(IMenuDishService service, IMapper mapper)
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
                List<MenuDishModel> menuDishes = await _service.GetAllByMenuId(menuId);
                return menuDishes switch
                {
                    null => Ok(new ItemListResponse<MenuDishModel>(ConstantMessage.Fail)),
                    not null => Ok(new ItemListResponse<MenuDishModel>(ConstantMessage.Success, menuDishes))
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddCourse(MenuDishRequest dish)
        {
            try
            {
                var isDish = await _service.Add(_mapper.Map<MenuDishModel>(dish));

                return isDish switch
                {
                    true => Ok(new BaseResponse(isDish, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isDish, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete")]
        public async Task<IActionResult> Delete(Guid idDish, Guid idMenu)
        {
            try
            {
                if (idMenu != Guid.Empty && idDish != Guid.Empty)
                {
                    var isDish = await _service.Delete(idDish, idMenu);

                    return isDish switch
                    {
                        true => Ok(new BaseResponse(isDish, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isDish, ConstantMessage.Fail))
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
