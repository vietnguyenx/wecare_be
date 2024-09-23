using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Repositories.Interface;
using Wecare.Repositories.Repositories.UnitOfWork.Interface;
using Wecare.Services.Base;
using Wecare.Services.Model;
using Wecare.Services.Services.Interface;

namespace Wecare.Services.Services.Services
{
    public class MenuDietPlanService : BaseService<MenuDietPlan>, IMenuDietPlanService
    {
        private readonly IMenuDietPlanRepository _repository;

        public MenuDietPlanService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _repository = unitOfWork.MenuDietPlanRepository;
        }
        public async Task<bool> Add(MenuDietPlanModel menuDietPlanModel)
        {
            var entity = await _repository.GetByDietPlanIdAndMenuId(menuDietPlanModel.DietPlanId, menuDietPlanModel.MenuId);
            if (entity != null) { return false; }
            var Cr = _mapper.Map<MenuDietPlan>(menuDietPlanModel);
            var cr = await SetBaseEntityToCreateFunc(Cr);
            return await _repository.Add(cr);
        }

        public async Task<bool> Delete(Guid dietPlanId, Guid menuId)
        {
            var entity = await _repository.GetByDietPlanIdAndMenuId(dietPlanId, menuId);
            if (entity == null)
            {
                return false;
            }

            var dietPlan = _mapper.Map<MenuDietPlan>(entity);
            return await _repository.Delete(dietPlan);
        }

        public async Task<List<MenuDietPlanModel>?> GetAllByMenuId(Guid menuId)
        {
            var menuDietPlan = await _repository.GetAllByMenuId(menuId);
            if (!menuDietPlan.Any())
            {
                return null;
            }
            return _mapper.Map<List<MenuDietPlanModel>>(menuDietPlan);
        }
    }
}
