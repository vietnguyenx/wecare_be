using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;
using Wecare.Repositories.Repositories.Repositories.Interface;
using WeCare.Repositories.Data;

namespace Wecare.Repositories.Repositories.Repositories.Repository
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        private readonly WeCareDbContext _context;

        public DishRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Dish>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable.Include(m => m.MenuDishes).ToListAsync();
        }

        public async Task<(List<Dish>, long)> Search(Dish Dish, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);
            // dieu kien loc tung buoc
            if (queryable.Any())
            {
                if (Dish.Id != Guid.Empty && Dish.Id != null)
                {
                    queryable = queryable.Where(m => m.Id == Dish.Id);
                }
                if (!string.IsNullOrEmpty(Dish.DishName))
                {
                    queryable = queryable.Where(m => m.DishName.ToUpper().Contains(Dish.DishName.ToUpper()));
                }

                if (!string.IsNullOrEmpty(Dish.Ingredients))
                {
                    queryable = queryable.Where(m => m.Ingredients.ToLower().Trim().Contains(Dish.Ingredients.ToLower().Trim()));
                }
                if (Dish.Calories != null)
                {
                    queryable = queryable.Where(m => m.Calories == Dish.Calories);
                }
                if (Dish.Carbs != null)
                {
                    queryable = queryable.Where(m => m.Carbs == Dish.Carbs);
                }
                if (Dish.Protein != null)
                {
                    queryable = queryable.Where(m => m.Protein == Dish.Protein);
                }
                if (Dish.Fat != null)
                {
                    queryable = queryable.Where(m => m.Fat == Dish.Fat);
                }
            }

            var totalOrigin = queryable.Count();

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var dishes = await queryable.Include(m => m.MenuDishes).ToListAsync();

            return (dishes, totalOrigin);
        }

        public async Task<List<Dish>> SearchDish(string name)
        {
            var queryable = base.GetQueryable(x => x.DishName.StartsWith(name) || x.Id.Equals(name));

            if (queryable.Any())
            {
                queryable = queryable.Where(x => !x.IsDeleted);
            }

            if (queryable.Any())
            {
                var results = await queryable.Include(m => m.MenuDishes).ToListAsync();

                return results;
            }

            return null;
        }

        public async new Task<Dish?> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var user = await query.Include(m => m.MenuDishes).SingleOrDefaultAsync();

            return user;
        }

        public async Task<(List<Dish>, long)> GetAllPaginationByListId(List<Guid> guids, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // dieu kien loc theo tung buoc
            if (queryable.Any())
            {
                queryable = queryable.Where(x => guids.Contains(x.Id));
            }
            var totalOrigin = queryable.Count();

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var dishes = await queryable.Include(m => m.MenuDishes).ToListAsync();

            return (dishes, totalOrigin);
        }

        public async Task<List<Dish>> GetAllExceptListId(List<Guid> guids)
        {
            var queryable = GetQueryable();

            if (queryable.Any())
            {
                queryable = queryable.Where(x => !guids.Contains(x.Id));
            }

            var dishes = await queryable.Include(m => m.MenuDishes).ToListAsync();

            return dishes;
        }

    }
}
