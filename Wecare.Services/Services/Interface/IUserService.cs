﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IUserService
    {
        Task<bool> Add(UserModel userModel);

        Task<bool> Update(UserModel userModel);

        Task<bool> UpdateDiseaseType(Guid userId, DiseaseType? diseaseType);
        Task<bool> UpdateUserType(Guid userId, UserType? userType);

        Task<bool> Delete(Guid id);
        public Task<UserModel?> GetById(Guid id);

        public Task<List<UserModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<List<UserModel>?> GetAll();

        public Task<(List<UserModel>?, long)> Search(UserModel userModel, int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<long> GetTotalCount();

        Task<UserModel> Login(AuthModel authModel);

        Task<UserModel> Register(UserModel userModel);

        public JwtSecurityToken CreateToken(UserModel userModel);

        public Task<UserModel?> GetUserByEmailOrUsername(UserModel userModel);
        public Task<UserModel?> GetUserByEmail(UserModel userModel);
        bool VerifyOtp(string email, string otp);
        void StoreOtp(string email, string otp);
        string GenerateOTP();
        Task<bool> UpdatePassword(UserModel userModel);
    }
}
