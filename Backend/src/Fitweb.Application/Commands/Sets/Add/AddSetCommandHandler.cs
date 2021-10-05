using AutoMapper;
using Fitweb.Domain.Common;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Add
{
    public class AddSetCommandHandler : IRequestHandler<AddSetCommand>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public AddSetCommandHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddSetCommand request, CancellationToken cancellationToken)
        {
            var trainingWithSets = await _trainingRepository
                .GetExerciseWithSets(request.UserId, request.TrainingId, request.ExerciseId);

            var set = _mapper.Map<Set>(request);

            trainingWithSets.AddSet(request.ExerciseId, set);

            await _trainingRepository.UpdateAsync(trainingWithSets);

            return Unit.Value;
        }
    }
}
