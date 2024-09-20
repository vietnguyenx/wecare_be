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
    public class DishService : BaseService<Dish>, IDishService
    {
        private readonly IDishRepository _repository;

        public DishService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _repository = unitOfWork.DishRepository;
        }

        public async Task<bool> Add(DishModel model)
        {
            var Dish = _mapper.Map<Dish>(model);
            var dish = await SetBaseEntityToCreateFunc(Dish);
            return await _repository.Add(dish);
        }

        public async Task<bool> Update(DishModel model)
        {

            var entity = await _repository.GetById(model.Id);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(model, entity);
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

            var Dish = _mapper.Map<Dish>(entity);
            return await _repository.Delete(Dish);
        }

        public async Task<List<DishModel>> GetAll()
        {
            var Dishes = await _repository.GetAll();

            if (!Dishes.Any())
            {
                return null;
            }

            return _mapper.Map<List<DishModel>>(Dishes);
        }

        public async Task<DishModel?> GetById(Guid id)
        {
            var Dishes = await _repository.GetById(id);

            if (Dishes == null)
            {
                return null;
            }

            return _mapper.Map<DishModel>(Dishes);
        }

        public async Task<List<DishModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var dishes = await _repository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder);

            if (!dishes.Any())
            {
                return null;
            }

            return _mapper.Map<List<DishModel>>(dishes);
        }

        public async Task<(List<DishModel>?, long)> Search(DishModel dishModel, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var dishes = _mapper.Map<Dish>(dishModel);
            var dishesWithTotalOrigin = await _repository.Search(dishes, pageNumber, pageSize, sortField, sortOrder);

            if (!dishesWithTotalOrigin.Item1.Any())
            {
                return (null, dishesWithTotalOrigin.Item2);
            }
            var dishModels = _mapper.Map<List<DishModel>>(dishesWithTotalOrigin.Item1);

            return (dishModels, dishesWithTotalOrigin.Item2);
        }

        public async Task<(List<DishModel>?, long)> GetAllPaginationByListId(List<Guid> guids, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var dishesWithTotalOrigin = await _repository.GetAllPaginationByListId(guids, pageNumber, pageSize, sortField, sortOrder);

            if (!dishesWithTotalOrigin.Item1.Any())
            {
                return (null, dishesWithTotalOrigin.Item2);
            }
            var dishModels = _mapper.Map<List<DishModel>>(dishesWithTotalOrigin.Item1);

            return (dishModels, dishesWithTotalOrigin.Item2);
        }
        public async Task<List<DishModel>?> GetAllExceptListId(List<Guid> guids)
        {
            var dishesWithTotalOrigin = await _repository.GetAllExceptListId(guids);

            if (!dishesWithTotalOrigin.Any())
            {
                return null;
            }
            var dishModels = _mapper.Map<List<DishModel>>(dishesWithTotalOrigin);

            return dishModels;
        }
    }
}
