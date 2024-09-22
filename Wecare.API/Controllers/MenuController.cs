using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.API.RequestModel;
using Wecare.API.ResponseModel;
using Wecare.API.SearchModel;
using Wecare.API.Tools.Constant;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        private readonly IMapper _mapper;

        public MenuController(IMenuService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var menus = await _service.GetAll();

                return menus switch
                {
                    null => Ok(new ItemListResponse<MenuModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<MenuModel>(ConstantMessage.Success, menus))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-by-dietitian-id/{dietitianId}")]
        public async Task<IActionResult> GetAllbyDietitianId(Guid dietitianId)
        {
            try
            {
                if (dietitianId == Guid.Empty)
                {
                    return Ok("Id is empty");
                }
                var menus = await _service.GetAllMenuByDietitianId(dietitianId);

                return menus switch
                {
                    null => Ok(new ItemListResponse<MenuModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<MenuModel>(ConstantMessage.Success, menus))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetMenu(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var menuModel = await _service.GetById(id);

                return menuModel switch
                {
                    null => Ok(new ItemResponse<MenuModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<MenuModel>(ConstantMessage.Success, menuModel))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMenu(MenuRequest menu)
        {
            try
            {
                var isMenu = await _service.Add(_mapper.Map<MenuModel>(menu));

                return isMenu switch
                {
                    true => Ok(new BaseResponse(isMenu, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isMenu, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var isMenu = await _service.Delete(id);

                    return isMenu switch
                    {
                        true => Ok(new BaseResponse(isMenu, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isMenu, ConstantMessage.Fail))
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

        [HttpPut("update")]
        public async Task<IActionResult> Update(MenuRequest package)
        {
            try
            {
                var menuModel = _mapper.Map<MenuModel>(package);

                var isMenu = await _service.Update(menuModel);

                return isMenu switch
                {
                    true => Ok(new BaseResponse(isMenu, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isMenu, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetAllMenuSearch(PaginatedRequest<MenuSearchRequest> paginatedRequest)
        {
            try
            {
                var package = _mapper.Map<MenuModel>(paginatedRequest.Result);
                var packages = await _service.Search(package, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);

                return packages.Item1 switch
                {
                    null => Ok(new PaginatedListResponse<MenuModel>(ConstantMessage.NotFound, packages.Item1, packages.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField)),
                    not null => Ok(new PaginatedListResponse<MenuModel>(ConstantMessage.Success, packages.Item1, packages.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("get-all-pagination")]
        public async Task<IActionResult> GetAllMenus(PaginatedRequest paginatedRequest)
        {
            try
            {
                var menus = await _service.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _service.GetTotalCount();
                return menus switch
                {
                    null => Ok(new PaginatedListResponse<MenuModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<MenuModel>(ConstantMessage.Success, menus, totalOrigin, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }
    }
}
