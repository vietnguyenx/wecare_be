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
    public class MenuDishService : BaseService<MenuDish>, IMenuDishService
    {
        private readonly IMenuDishRepository _repository;

        public MenuDishService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _repository = unitOfWork.MenuDishRepository;
        }
        public async Task<bool> Add(MenuDishModel menuDishModel)
        {
            var entity = await _repository.GetByDishIdAndMenuId(menuDishModel.DishId, menuDishModel.MenuId);
            if (entity != null) { return false; }
            var Cr = _mapper.Map<MenuDish>(menuDishModel);
            var cr = await SetBaseEntityToCreateFunc(Cr);
            return await _repository.Add(cr);
        }

        public async Task<bool> Delete(Guid dishId, Guid menuId)
        {
            var entity = await _repository.GetByDishIdAndMenuId(dishId, menuId);
            if (entity == null)
            {
                return false;
            }

            var dish = _mapper.Map<MenuDish>(entity);
            return await _repository.Delete(dish);
        }

        public async Task<List<MenuDishModel>?> GetAllByMenuId(Guid menuId)
        {
            var menuDish = await _repository.GetAllByMenuId(menuId);
            if (!menuDish.Any())
            {
                return null;
            }
            return _mapper.Map<List<MenuDishModel>>(menuDish);
        }
    }
}
