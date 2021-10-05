using Fitweb.Domain.Common;
using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.FoodProducts.Repositories
{
    public interface IFoodProductRepository : IBaseRepository<FoodProduct>
    {
        Task<FoodProduct> GetByNameAsync(string name);

        Task<(IEnumerable<FoodProduct>, int TotalItems)> GetAllAsync(PaginationFilter pagination, 
            string searchName = null, string userId = null, FoodGroup? foodGroup = null);

        Task AddRangeAsync(List<FoodProduct> foodProducts);

        Task<bool> AnyAsync();
    }
}
