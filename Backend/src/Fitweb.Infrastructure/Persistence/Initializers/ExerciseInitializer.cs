using Fitweb.Application.Interfaces.Utilities.Csv;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Infrastructure.Utilities.Csv.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Initializers
{
    public class ExerciseInitializer : IDataInitializer
    {
        private readonly ICsvService<Exercise, ExerciseMap> _csvService;
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseInitializer(ICsvService<Exercise, ExerciseMap> csvService, 
            IExerciseRepository exerciseRepository)
        {
            _csvService = csvService;
            _exerciseRepository = exerciseRepository;
        }

        public async Task SeedAsync()
        {
            if (await _exerciseRepository.AnyAsync())
            {
                return;
            }

            var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            filePath += @"/Files/exercises.csv";

            var exercises = _csvService.ReadCsvAsync(filePath);

            await _exerciseRepository.AddRangeAsync(exercises);
        }
    }
}
