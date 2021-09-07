using Fitweb.Application.Interfaces.Utilities.Csv;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Infrastructure.Utilities.Csv.Maps;
using Fitweb.Infrastructure.Utilities.Csv.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Initializers
{
    public class FoodProductInitializer : IDataInitializer
    {
        private readonly ICsvService<FoodProduct, FoodProductMap> _csvService;
        private readonly IFoodProductRepository _foodProductRepository;

        public FoodProductInitializer(ICsvService<FoodProduct, FoodProductMap> csvService, 
            IFoodProductRepository foodProductRepository)
        {
            _csvService = csvService;
            _foodProductRepository = foodProductRepository;
        }

        public async Task SeedAsync()
        {
            if (await _foodProductRepository.AnyAsync())
            {
                return;
            }

            var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            filePath += @"/Files/food_products.csv";

            var foodProducts = _csvService.ReadCsvAsync(filePath);

            await _foodProductRepository.AddRangeAsync(foodProducts);
        }
    }
}
