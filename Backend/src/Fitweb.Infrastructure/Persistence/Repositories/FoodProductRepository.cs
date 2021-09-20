using Fitweb.Domain.Filters;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitweb.Infrastructure.Persistence.Extensions;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class FoodProductRepository : BaseRepository<FoodProduct>, IFoodProductRepository
    {
        public FoodProductRepository(FitwebDbContext context) : base(context)
        {
        }
        public async Task<FoodProduct> GetByNameAsync(string name)
            => await _context.FoodProducts.SingleOrDefaultAsync(x => x.Information.Name == name);

        public async Task<(IEnumerable<FoodProduct>, int TotalItems)> GetAllAsync(PaginationFilter pagination, OrderFilter order)
        {
            var queryable = _context.FoodProducts.AsNoTracking();

            if (!string.IsNullOrEmpty(order.ColumnName))
            {
                queryable = queryable.ApplyOrderBy(MapColumnName(order.ColumnName), order.IsAscending);
            }

            var totalItems = await queryable.CountAsync();

            var data = await queryable.ApplyPaging(pagination.PageSize, pagination.PageNumber);

            return (data, totalItems);
        }

        public async Task AddRangeAsync(List<FoodProduct> foodProducts)
        {
            await _context.AddRangeAsync(foodProducts);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync()
            => await _context.FoodProducts.AnyAsync();

        private static string MapColumnName(string columnName)
            => columnName switch
            {
                "protein"               => "Nutrient.Protein",
                "carbohydrate"          => "Nutrient.Carbohydrate",
                "fat"                   => "Nutrient.Fat",
                _                       => "Information.Name"
            };
    }
}
