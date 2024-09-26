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
    public class MenuService : BaseService<Menu>, IMenuService
    {
        private readonly IMenuRepository _repository;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _repository = unitOfWork.MenuRepository;
        }

        public async Task<bool> Add(MenuModel menuModel)
        {
            var Menu = _mapper.Map<Menu>(menuModel);
            var menu = await SetBaseEntityToCreateFunc(Menu);
            return await _repository.Add(menu);
        }

        public async Task<bool> Update(MenuModel menuModel)
        {
            var entity = await _repository.GetById(menuModel.Id);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(menuModel, entity);
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

            var Menu = _mapper.Map<Menu>(entity);
            return await _repository.Delete(Menu);
        }

        public async Task<List<MenuModel>> GetAll()
        {
            var Menus = await _repository.GetAll();

            if (!Menus.Any())
            {
                return null;
            }

            return _mapper.Map<List<MenuModel>>(Menus);
        }

        public async Task<MenuModel?> GetById(Guid id)
        {
            var Menu = await _repository.GetById(id);

            if (Menu == null)
            {
                return null;
            }

            return _mapper.Map<MenuModel>(Menu);
        }

        public async Task<List<MenuModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var menus = await _repository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder);

            if (!menus.Any())
            {
                return null;
            }

            return _mapper.Map<List<MenuModel>>(menus);

        }

        public async Task<List<MenuModel>?> GetAllMenuByDietitianId(Guid idDietitian)
        {
            var menusByDietitianId = await _repository.GetAllMenuByDietitianId(idDietitian);
            if (!menusByDietitianId.Any())
            {
                return null;
            }
            return _mapper.Map<List<MenuModel>>(menusByDietitianId);
        }

        public async Task<(List<MenuModel>?, long)> Search(MenuModel menuModel, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var menus = _mapper.Map<Menu>(menuModel);
            var menuWithTotalOrigin = await _repository.Search(menus, pageNumber, pageSize, sortField, sortOrder);

            if (!menuWithTotalOrigin.Item1.Any())
            {
                return (null, menuWithTotalOrigin.Item2);
            }
            var courseModels = _mapper.Map<List<MenuModel>>(menuWithTotalOrigin.Item1);

            return (courseModels, menuWithTotalOrigin.Item2);
        }

        public async Task<List<MenuModel>?> GetMenusByHealthMetrics(List<HealthMetricModel> healthMetrics)
        {
            var healthMetricsEntities = _mapper.Map<List<HealthMetric>>(healthMetrics);
            var menus = await _repository.GetMenusByHealthMetrics(healthMetricsEntities);

            if (!menus.Any())
            {
                return null;
            }

            return _mapper.Map<List<MenuModel>>(menus);
        }

    }
 
}
