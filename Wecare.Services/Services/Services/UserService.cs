﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Data.Entities.Enum;
using Wecare.Repositories.Repositories.Repositories.Interface;
using Wecare.Repositories.Repositories.UnitOfWork.Interface;
using Wecare.Services.Base;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.Services.Services.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _repository;

        private readonly IConfiguration _configuration;

        private DateTime countDown = DateTime.Now.AddDays(0.5);

        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> OtpStore = new();

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHttpContextAccessor _httpContextAccessor) : base(mapper, unitOfWork, _httpContextAccessor)
        {
            _repository = unitOfWork.UserRepository;
            _configuration = configuration;
        }

        public async Task<bool> Add(UserModel userModel)
        {
            userModel.DOB = userModel.DOB != null ? userModel.DOB.Value.ToLocalTime() : null;
            var user = _mapper.Map<User>(userModel);
            user.Password = userModel.Password;
            var setUser = await SetBaseEntityToCreateFunc(user);

            return await _repository.Add(setUser);
        }

        public async Task<bool> Update(UserModel userModel)
        {
            var entity = await _repository.GetById(userModel.Id);

            if (entity == null)
            {
                return false;
            }

            userModel.DOB = userModel.DOB.Value.ToLocalTime();
            _mapper.Map(userModel, entity);
            entity = await SetBaseEntityToUpdateFunc(entity);

            return await _repository.Update(entity);
        }

        public async Task<bool> UpdateDiseaseType(Guid userId, DiseaseType? diseaseType)
        {
            var entity = await _repository.GetById(userId);

            if (entity == null)
            {
                return false;
            }

            entity.DiseaseType = diseaseType; 
            entity = await SetBaseEntityToUpdateFunc(entity);

            return await _repository.Update(entity);
        }

        public async Task<bool> UpdateUserType(Guid userId, UserType? userType)
        {
            var entity = await _repository.GetById(userId);

            if (entity == null)
            {
                return false;
            }

            entity.UserType = userType;
            entity = await SetBaseEntityToUpdateFunc(entity);

            return await _repository.Update(entity);
        }


        public async Task<bool> UpdatePassword(UserModel userModel)
        {
            var entity = await _repository.GetById(userModel.Id);

            if (entity == null)
            {
                return false;
            }
            var newPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
            userModel.DOB = userModel.DOB.Value.ToLocalTime();

            _mapper.Map(userModel, entity);

            if (!string.IsNullOrEmpty(userModel.Password))
            {
                // Example: Hashing the password before saving
                entity.Password = newPassword;
            }

            entity = await SetBaseEntityToUpdateFunc(entity);

            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                return false;
            }

            var user = _mapper.Map<User>(entity);
            return await _repository.Delete(user);
        }

        public async Task<List<UserModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var users = await _repository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder);

            if (!users.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<List<UserModel>?> GetAll()
        {
            var users = await _repository.GetAll();

            if (!users.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<(List<UserModel>?, long)> Search(UserModel userModel, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var user = _mapper.Map<User>(userModel);
            var usersWithTotalOrigin = await _repository.Search(user, pageNumber, pageSize, sortField, sortOrder);

            if (!usersWithTotalOrigin.Item1.Any())
            {
                return (null, usersWithTotalOrigin.Item2);
            }
            var userModels = _mapper.Map<List<UserModel>>(usersWithTotalOrigin.Item1);

            return (userModels, usersWithTotalOrigin.Item2);
        }

        public async Task<UserModel?> GetById(Guid id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Login(AuthModel authModel)
        {
            User userHasUsernameOrEmail = new User
            {
                Email = authModel.UsernameOrEmail,
                Username = authModel.UsernameOrEmail,
                Password = authModel.Password,
            };
            // check username or email
            User user = await _repository.FindUsernameOrEmail(userHasUsernameOrEmail);

            if (user == null)
            {
                return null;
            }

            // check password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(authModel.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            UserModel userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }   


        public void StoreOtp(string email, string otp)
        {
            var expiry = DateTime.UtcNow.AddMinutes(5); // OTP expires in 5 minutes
            OtpStore[email] = (otp, expiry);
        }

        public bool VerifyOtp(string email, string otp)
        {
            if (OtpStore.ContainsKey(email))
            {
                var (storedOtp, expiry) = OtpStore[email];
                if (expiry > DateTime.UtcNow && storedOtp == otp)
                {
                    OtpStore.Remove(email); // Remove OTP after successful verification
                    return true;
                }
            }
            return false;
        }


        public async Task<UserModel> Register(UserModel userModel)
        {
            if (userModel.Password != null)
            {
                userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
            }

            bool isUser = await Add(userModel);

            UserModel _userModel = await GetUserByEmailOrUsername(userModel);

            if (!isUser)
            {
                return null;
            }

            return _userModel;
        }

        public JwtSecurityToken CreateToken(UserModel userModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
            };
            // Conditional addition of claim based on function result
            if (!string.IsNullOrEmpty(userModel.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, userModel.Email));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: countDown
                );

            return token;
        }

        public async Task<UserModel?> GetUserByEmailOrUsername(UserModel userModel)
        {
            var user = await _repository.FindUsernameOrEmail(_mapper.Map<User>(userModel));
            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel?> GetUserByEmail(UserModel userModel)
        {
            var user = await _repository.GetUserByEmail(_mapper.Map<User>(userModel));
            return _mapper.Map<UserModel>(user);
        }
    }
}
