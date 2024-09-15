﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Data;
using Wecare.Repositories.Repositories.Base;
using Wecare.Repositories.Repositories.UnitOfWork.Interface;

namespace Wecare.Repositories.Repositories.UnitOfWork.Repository
{
    public class BaseUnitOfWork<TContext> : IBaseUnitOfWork
        where TContext : BaseDbContext
    {
        private readonly TContext _context;
        private readonly IServiceProvider _serviceProvider;

        protected BaseUnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        #region Dispose()
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        #endregion

        #region GetRepository<TRepository>() + GetRepositoryByEntity<TEntity>()
        public TRepository GetRepository<TRepository>() where TRepository : IBaseRepository
        {
            if (_serviceProvider != null)
            {
                var result = _serviceProvider.GetService<TRepository>();
                return result;
            }
            return default;
        }

        public IBaseRepository<TEntity> GetRepositoryByEntity<TEntity>() where TEntity : BaseEntity
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var type = typeof(IBaseRepository<TEntity>);
            foreach (var property in properties)
            {
                if (type.IsAssignableFrom(property.PropertyType))
                {
                    var value = (IBaseRepository<TEntity>)property.GetValue(this);
                    return value;
                }
            }
            return new BaseRepository<TEntity>(_context);
        }
        #endregion

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
