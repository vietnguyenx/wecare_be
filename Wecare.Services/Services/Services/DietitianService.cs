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
    public class DietitianService : BaseService<Dietitian>, IDietitianService
    {
        private readonly IDietitianRepository _dietitianRepository;

        public DietitianService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _dietitianRepository = unitOfWork.DietitianRepository;
        }

        public async Task<bool> Add(DietitianModel model)
        {
            try
            {
                var dietitian = _mapper.Map<Dietitian>(model);
                var setdietitian = await SetBaseEntityToCreateFunc(dietitian);
                return await _dietitianRepository.Add(setdietitian);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _dietitianRepository.GetById(id);

            if (entity == null)
            {
                return false;
            }

            var dietitian = _mapper.Map<Dietitian>(entity);
            return await _dietitianRepository.Delete(dietitian);
        }

        public async Task<List<DietitianModel>> GetAll()
        {
            try
            {
                var dietitian = await _dietitianRepository.GetAll();
                if (!dietitian.Any())
                {
                    return null;
                }
                return _mapper.Map<List<DietitianModel>>(dietitian);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DietitianModel>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            try
            {
                return _mapper.Map<List<DietitianModel>>(await _dietitianRepository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DietitianModel?> GetById(Guid id)
        {
            var dietitian = await _dietitianRepository.GetById(id);

            if (dietitian == null)
            {
                return null;
            }

            return _mapper.Map<DietitianModel>(dietitian);
        }

        public async Task<(List<DietitianModel>?, long)> Search(DietitianModel dietitianModel, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var dietitian = _mapper.Map<Dietitian>(dietitianModel);
            var dietitiansWithTotalOrigin = await _dietitianRepository.Search(dietitian, pageNumber, pageSize, sortField, sortOrder);

            if (!dietitiansWithTotalOrigin.Item1.Any())
            {
                return (null, dietitiansWithTotalOrigin.Item2);
            }
            var dietitianModels = _mapper.Map<List<DietitianModel>>(dietitiansWithTotalOrigin.Item1);

            return (dietitianModels, dietitiansWithTotalOrigin.Item2);
        }

        public async Task<bool> Update(DietitianModel dietitianModel)
        {
            try
            {
                var entity = await _dietitianRepository.GetById(dietitianModel.Id);

                if (entity == null)
                {
                    return false;
                }
                _mapper.Map(dietitianModel, entity);
                entity = await SetBaseEntityToUpdateFunc(entity);

                return await _dietitianRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
