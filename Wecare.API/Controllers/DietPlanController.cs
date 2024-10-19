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
    [Route("api/dietplan")]
    [ApiController]
    //[Authorize]
    public class DietPlanController : ControllerBase
    {
        private readonly IDietPlanService _service;
        private readonly IMapper _mapper;

        public DietPlanController(IDietPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dietPlans = await _service.GetAll();

                return dietPlans switch
                {
                    null => Ok(new ItemListResponse<DietPlanModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<DietPlanModel>(ConstantMessage.Success, dietPlans))
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
                var dietPlans = await _service.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _service.GetTotalCount();

                return dietPlans switch
                {
                    null => Ok(new PaginatedListResponse<DietPlanModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<DietPlanModel>(ConstantMessage.Success, dietPlans, totalOrigin,
                                        paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetDietPlan(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var dietPlanModel = await _service.GetById(id);

                return dietPlanModel switch
                {
                    null => Ok(new ItemResponse<DietPlanModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<DietPlanModel>(ConstantMessage.Success, dietPlanModel))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpGet("get-by-user-id/{id}")]
        public async Task<IActionResult> GetDietPlanByUserId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("UserId is empty");
                }
                var dietPlanModel = await _service.GetByUserId(id);

                return dietPlanModel switch
                {
                    null => Ok(new ItemResponse<DietPlanModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<DietPlanModel>(ConstantMessage.Success, dietPlanModel))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDietPlan(DietPlanRequest dietPlan)
        {
            try
            {
                var isDietPlan = await _service.Add(_mapper.Map<DietPlanModel>(dietPlan));

                return isDietPlan switch
                {
                    true => Ok(new BaseResponse(isDietPlan, ConstantMessage.Success)),
                    false => BadRequest(new BaseResponse(false, "Diet plan for this date already exists.")),
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
                    var isDietPlan = await _service.Delete(id);

                    return isDietPlan switch
                    {
                        true => Ok(new BaseResponse(isDietPlan, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isDietPlan, ConstantMessage.Fail))
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
        public async Task<IActionResult> Update(DietPlanRequest dietPlan)
        {
            try
            {
                var dietPlanModel = _mapper.Map<DietPlanModel>(dietPlan);

                var isDietPlan = await _service.Update(dietPlanModel);

                return isDietPlan switch
                {
                    true => Ok(new BaseResponse(isDietPlan, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isDietPlan, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
