using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wecare.API.RequestModel;
using Wecare.API.ResponseModel;
using Wecare.API.SearchModel;
using Wecare.API.Tools.Constant;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;
using Wecare.Services.Services.Services;

namespace Wecare.API.Controllers
{
    [Route("api/dietitian")]
    [ApiController]
    //[Authorize]
    public class DietitianController : ControllerBase
    {
        private readonly IDietitianService _dietitianService;
        private readonly IMapper _mapper;

        public DietitianController(IDietitianService dietitianService, IMapper mapper)
        {
            _dietitianService = dietitianService;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dietitians = await _dietitianService.GetAll();

                return dietitians switch
                {
                    null => Ok(new ItemListResponse<DietitianModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<DietitianModel>(ConstantMessage.Success, dietitians))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-all-pagination")]
        public async Task<IActionResult> GetAllPagination(PaginatedRequest paginatedRequest)
        {
            try
            {
                var dietitians = await _dietitianService.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _dietitianService.GetTotalCount();

                return dietitians switch
                {
                    null => Ok(new PaginatedListResponse<DietitianModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<DietitianModel>(ConstantMessage.Success, dietitians, totalOrigin,
                                        paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var model = await _dietitianService.GetById(id);

                return model switch
                {
                    null => Ok(new ItemResponse<DietitianModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<DietitianModel>(ConstantMessage.Success, model))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetAllDietitianSearch(PaginatedRequest<DietitianSearchRequest> paginatedRequest)
        {
            try
            {
                var dietitian = _mapper.Map<DietitianModel>(paginatedRequest.Result);
                var dietitians = await _dietitianService.Search(dietitian, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);

                return dietitians.Item1 switch
                {
                    null => Ok(new PaginatedListResponse<DietitianModel>(ConstantMessage.NotFound, dietitians.Item1, dietitians.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField)),
                    not null => Ok(new PaginatedListResponse<DietitianModel>(ConstantMessage.Success, dietitians.Item1, dietitians.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(DietitianRequest request)
        {
            try
            {
                bool isSuccess = await _dietitianService.Add(_mapper.Map<DietitianModel>(request));
                return isSuccess switch
                {
                    true => Ok(new BaseResponse(isSuccess, ConstantMessage.Success)),
                    false => Ok(new BaseResponse(isSuccess, ConstantMessage.Fail))
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
                    var isDietitian = await _dietitianService.Delete(id);

                    return isDietitian switch
                    {
                        true => Ok(new BaseResponse(isDietitian, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isDietitian, ConstantMessage.Fail))
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
        public async Task<IActionResult> Update(DietitianRequest request)
        {
            try
            {
                bool isSuccess = await _dietitianService.Update(_mapper.Map<DietitianModel>(request));
                return isSuccess switch
                {
                    true => Ok(new BaseResponse(isSuccess, ConstantMessage.Success)),
                    false => Ok(new BaseResponse(isSuccess, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}