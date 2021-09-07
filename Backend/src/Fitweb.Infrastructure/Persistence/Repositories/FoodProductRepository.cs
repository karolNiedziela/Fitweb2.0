using Fitweb.Domain.Filters;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class FoodProductRepository : BaseRepository<FoodProduct>, IFoodProductRepository
    {
        public FoodProductRepository(FitwebDbContext context) : base(context)
        {

        }

        public async Task<FoodProduct> GetByNameAsync(string name)
            => await _context.FoodProducts.SingleOrDefaultAsync(x => x.Information.Name == name);


        public async Task AddRangeAsync(List<FoodProduct> foodProducts)
        {
            await _context.AddRangeAsync(foodProducts);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync()
            => await _context.FoodProducts.AnyAsync();
    }
}
