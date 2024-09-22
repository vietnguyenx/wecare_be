using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wecare.API.RequestModel;
using Wecare.API.ResponseModel;
using Wecare.API.SearchModel;
using Wecare.API.Tools.Constant;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/dish")]
    [ApiController]
    //[Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;
        private readonly IMapper _mapper;
        
        public DishController(IDishService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dishes = await _service.GetAll();

                return dishes switch
                {
                    null => Ok(new ItemListResponse<DishModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<DishModel>(ConstantMessage.Success, dishes))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetDish(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var dishModel = await _service.GetById(id);

                return dishModel switch
                {
                    null => Ok(new ItemResponse<DishModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<DishModel>(ConstantMessage.Success, dishModel))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDish(DishRequest dish)
        {
            try
            {
                var isDish = await _service.Add(_mapper.Map<DishModel>(dish));

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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var isDish = await _service.Delete(id);

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

        [HttpPut("update")]
        public async Task<IActionResult> Update(DishRequest dish)
        {
            try
            {
                var dishModel = _mapper.Map<DishModel>(dish);

                var isDish = await _service.Update(dishModel);

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

        [HttpPost("search")]
        public async Task<IActionResult> GetAllDishSearch(PaginatedRequest<DishSearchRequest> paginatedRequest)
        {
            try
            {
                var dish = _mapper.Map<DishModel>(paginatedRequest.Result);
                var dishes = await _service.Search(dish, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);

                return dishes.Item1 switch
                {
                    null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.NotFound, dishes.Item1, dishes.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField)),
                    not null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.Success, dishes.Item1, dishes.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("get-all-pagination")]
        public async Task<IActionResult> GetAllDishes(PaginatedRequest paginatedRequest)
        {
            try
            {
                var dishes = await _service.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _service.GetTotalCount();
                return dishes switch
                {
                    null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.Success, dishes, totalOrigin, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }


        [HttpPost("get-all-pagination-by-list-id")]
        public async Task<IActionResult> GetAllDishes(PaginatedRequest<List<Guid>> paginatedRequest)
        {
            try
            {
                var dishes = await _service.GetAllPaginationByListId(paginatedRequest.Result, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);

                return dishes.Item1 switch
                {
                    null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.NotFound, dishes.Item1, dishes.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField)),
                    not null => Ok(new PaginatedListResponse<DishModel>(ConstantMessage.Success, dishes.Item1, dishes.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

    }
}
