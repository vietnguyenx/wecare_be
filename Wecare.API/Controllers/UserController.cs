﻿using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Wecare.API.RequestModel;
using Wecare.API.ResponseModel;
using Wecare.API.SearchModel;
using Wecare.API.Tools.Constant;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _service.GetAll();

                return users switch
                {
                    null => Ok(new ItemListResponse<UserModel>(ConstantMessage.Fail, null)),
                    not null => Ok(new ItemListResponse<UserModel>(ConstantMessage.Success, users))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetAllUserSearch(PaginatedRequest<UserSearchRequest> paginatedRequest)
        {
            try
            {
                var user = _mapper.Map<UserModel>(paginatedRequest.Result);
                var users = await _service.Search(user, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);

                return users.Item1 switch
                {
                    null => Ok(new PaginatedListResponse<UserModel>(ConstantMessage.NotFound, users.Item1, users.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder)),
                    not null => Ok(new PaginatedListResponse<UserModel>(ConstantMessage.Success, users.Item1, users.Item2, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpPost("get-all-pagination")]
        public async Task<IActionResult> GetAllUser(PaginatedRequest paginatedRequest)
        {
            try
            {
                var users = await _service.GetAllPagination(paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField, paginatedRequest.SortOrder.Value);
                long totalOrigin = await _service.GetTotalCount();
                return users switch
                {
                    null => Ok(new PaginatedListResponse<UserModel>(ConstantMessage.NotFound)),
                    not null => Ok(new PaginatedListResponse<UserModel>(ConstantMessage.Success, users, totalOrigin, paginatedRequest.PageNumber, paginatedRequest.PageSize, paginatedRequest.SortField))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Id is empty");
                }
                var userModel = await _service.GetById(id);

                return userModel switch
                {
                    null => Ok(new ItemResponse<UserModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<UserModel>(ConstantMessage.Success, userModel))
                };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            };
        }

        [AllowAnonymous]
        [HttpGet("get-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var userModel = await _service.GetUserByEmail(new UserModel { Email = email });

                return userModel switch
                {
                    null => Ok(new ItemResponse<UserModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<UserModel>(ConstantMessage.Success, userModel))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserRequest user)
        {
            try
            {
                var isUser = await _service.Add(_mapper.Map<UserModel>(user));

                return isUser switch
                {
                    true => Ok(new BaseResponse(isUser, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isUser, ConstantMessage.Fail))
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
                    var isUser = await _service.Delete(id);

                    return isUser switch
                    {
                        true => Ok(new BaseResponse(isUser, ConstantMessage.Success)),
                        _ => Ok(new BaseResponse(isUser, ConstantMessage.Fail))
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
        public async Task<IActionResult> Update(UserRequest user)
        {
            try
            {
                var userModel = _mapper.Map<UserModel>(user);

                var isUser = await _service.Update(userModel);

                return isUser switch
                {
                    true => Ok(new BaseResponse(isUser, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isUser, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateDiseaseType/{userId}")]
        public async Task<IActionResult> UpdateDiseaseType(Guid userId, [FromBody] UpdateDiseaseTypeRequest request)
        {
            try
            {
                var isUpdated = await _service.UpdateDiseaseType(userId, request.DiseaseType);

                return isUpdated switch
                {
                    true => Ok(new BaseResponse(isUpdated, ConstantMessage.Success)),
                    _ => Ok(new BaseResponse(isUpdated, ConstantMessage.Fail))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthModel authModel)
        {
            try
            {
                var userModel = await _service.Login(authModel);

                if (userModel == null)
                {
                    return Ok(new LoginResponse<UserModel>(null, null, null, ConstantMessage.Fail));
                }

                JwtSecurityToken token = _service.CreateToken(userModel);

                return Ok(new LoginResponse<UserModel>(ConstantMessage.Success, userModel, new JwtSecurityTokenHandler().WriteToken(token)
                , token.ValidTo.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "311399879185-vic40gludgaeulfo790m0h48h1cvul7u.apps.googleusercontent.com" }
            };

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }


        [AllowAnonymous]
        // POST api/<AuthController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                UserModel _userModel = await _service.GetUserByEmailOrUsername(_mapper.Map<UserModel>(userRequest));

                if (_userModel != null)
                {
                    return Ok(new ItemResponse<UserModel>(ConstantMessage.Duplicate));
                }

                UserModel userModelMapping = _mapper.Map<UserModel>(userRequest);
                userModelMapping.Password = userRequest.Password;

                UserModel userModel = await _service.Register(userModelMapping);

                return userModel switch
                {
                    null => Ok(new ItemResponse<UserModel>(ConstantMessage.NotFound)),
                    not null => Ok(new ItemResponse<UserModel>(ConstantMessage.Success, userModel))
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
