using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Repositories.Interface;
using Wecare.Repositories.Repositories.Repositories.Repository;
using Wecare.Repositories.Repositories.UnitOfWork.Interface;
using Wecare.Services.Base;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.Services.Services.Services
{
    public class DietPlanService : BaseService<DietPlan>, IDietPlanService
    {
        private readonly IDietPlanRepository _dietPlanrepository;

        public DietPlanService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _dietPlanrepository = unitOfWork.DietPlanRepository;
        }

        public async Task<bool> Add(DietPlanModel model)
        {
            model.DateAssigned = model.DateAssigned.Value;

            bool isExists = await _dietPlanrepository.IsDietPlanExists(model.UserId, model.DateAssigned.Value);
            if (isExists)
            {
                return false;
            }

            var dietPlan = _mapper.Map<DietPlan>(model);
            var setDietPlan = await SetBaseEntityToCreateFunc(dietPlan);

            return await _dietPlanrepository.Add(setDietPlan);
        }

        public async Task<bool> Update(DietPlanModel model)
        {
            var entity = await _dietPlanrepository.GetById(model.Id);

            if (entity == null)
            {
                return false;
            }
            model.DateAssigned = model.DateAssigned.Value;
            _mapper.Map(model, entity);
            entity = await SetBaseEntityToUpdateFunc(entity);

            var dietPlan = _mapper.Map<DietPlan>(model);
            return await _dietPlanrepository.Update(dietPlan);
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _dietPlanrepository.GetById(id);

            if (entity == null)
            {
                return false;
            }

            var dietPlan = _mapper.Map<DietPlan>(entity);
            return await _dietPlanrepository.Delete(dietPlan);
        }

        public async Task<List<DietPlanModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var dietPlans = await _dietPlanrepository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder);

            if (!dietPlans.Any())
            {
                return null;
            }

            return _mapper.Map<List<DietPlanModel>>(dietPlans);
        }

        public async Task<List<DietPlanModel>?> GetAll()
        {
            var dietPlans = await _dietPlanrepository.GetAll();

            if (!dietPlans.Any())
            {
                return null;
            }

            return _mapper.Map<List<DietPlanModel>>(dietPlans);
        }

        public async Task<DietPlanModel?> GetById(Guid id)
        {
            var dietPlan = await _dietPlanrepository.GetById(id);

            if (dietPlan == null)
            {
                return null;
            }

            return _mapper.Map<DietPlanModel>(dietPlan);
        }

        public async Task<DietPlanModel?> GetByUserId(Guid id)
        {
            var dietPlan = await _dietPlanrepository.GetByUserId(id);

            if (dietPlan == null)
            {
                return null;
            }

            return _mapper.Map<DietPlanModel>(dietPlan);
        }

    }
}
