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
    [Route("api/healthmetric")]
    [ApiController]
    //[Authorize]
    public class HealthMetricController : ControllerBase
    {
        private readonly IHealthMetricService _service;
        private readonly IMapper _mapper;

        public HealthMetricController(IHealthMetricService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var healthMetric = await _service.GetAll();

                return healthMetric switch
                {
                    null => Ok(new ItemListResponse<HealthMetricModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<HealthMetricModel>(ConstantMessage.Success, healthMetric))
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
                var healthMetric = await _service.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _service.GetTotalCount();

                return healthMetric switch
                {
                    null => Ok(new PaginatedListResponse<HealthMetricModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<HealthMetricModel>(ConstantMessage.Success, healthMetric, totalOrigin,
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
                var healthMetricModel = await _service.GetById(id);

                return healthMetricModel switch
                {
                    null => Ok(new ItemResponse<HealthMetricModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<HealthMetricModel>(ConstantMessage.Success, healthMetricModel))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        [HttpGet("get-by-userId")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var healthMetricModel = await _service.GetHealthMetricByUserId(userId);

                return healthMetricModel switch
                {
                    null => Ok(new ItemListResponse<HealthMetricModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemListResponse<HealthMetricModel>(ConstantMessage.Success, healthMetricModel))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(HealthMetricRequest request)
        {
            try
            {
                bool isSuccess = await _service.Add(_mapper.Map<HealthMetricModel>(request));
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
                    var isSession = await _service.Delete(id);

                    return isSession switch
                    {
                        true => Ok(new BaseResponse(isSession, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isSession, ConstantMessage.Fail))
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
        public async Task<IActionResult> Update(HealthMetricRequest request)
        {
            try
            {
                bool isSuccess = await _service.Update(_mapper.Map<HealthMetricModel>(request));
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
