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
    public class HealthMetricService : BaseService<HealthMetric>, IHealthMetricService
    {
        private readonly IHealthMetricRepository _healthMetricRepository;

        public HealthMetricService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _healthMetricRepository = unitOfWork.HealthMetricRepository;
        }

        public async Task<bool> Add(HealthMetricModel model)
        {
            try
            {
                var healthMetric = _mapper.Map<HealthMetric>(model);
                var setHealthMetric = await SetBaseEntityToCreateFunc(healthMetric);
                return await _healthMetricRepository.Add(setHealthMetric);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _healthMetricRepository.GetById(id);

            if (entity == null)
            {
                return false;
            }

            var healthMetric = _mapper.Map<HealthMetric>(entity);
            return await _healthMetricRepository.Delete(healthMetric);
        }

        public async Task<List<HealthMetricModel>?> GetAll()
        {
            var healthMetric = await _healthMetricRepository.GetAll();

            if (!healthMetric.Any())
            {
                return null;
            }

            return _mapper.Map<List<HealthMetricModel>>(healthMetric);
        }

        public async Task<List<HealthMetricModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var healthMetric = await _healthMetricRepository.GetAllPagination(pageNumber, pageSize, sortField, sortOrder);

            if (!healthMetric.Any())
            {
                return null;
            }

            return _mapper.Map<List<HealthMetricModel>>(healthMetric);
        }

        public async Task<HealthMetricModel?> GetById(Guid id)
        {
            var healthMetric = await _healthMetricRepository.GetById(id);

            if (healthMetric == null)
            {
                return null;
            }

            return _mapper.Map<HealthMetricModel>(healthMetric);
        }

        public async Task<List<HealthMetricModel>> GetHealthMetricByUserId(Guid id)
        {
            try
            {
                return _mapper.Map<List<HealthMetricModel>>(await _healthMetricRepository.GetHealthMetricByUserId(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(HealthMetricModel model)
        {
            var entity = await _healthMetricRepository.GetById(model.Id);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(model, entity);
            entity = await SetBaseEntityToUpdateFunc(entity);

            return await _healthMetricRepository.Update(entity);
        }
    }
}
