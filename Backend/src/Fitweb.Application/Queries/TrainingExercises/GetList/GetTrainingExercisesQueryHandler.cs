using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Responses;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.TrainingExercises.GetList
{
    public class GetTrainingExercisesQueryHandler 
        : IRequestHandler<GetTrainingExercisesQuery, Response<TrainingExercisesWithSetsDto>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingExercisesQueryHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<Response<TrainingExercisesWithSetsDto>> Handle(GetTrainingExercisesQuery request, CancellationToken cancellationToken)
        {
            var trainingExercises = await _trainingRepository.GetExercisesWithSets(request.UserId, request.TrainingId);

            var trainingExercisesDto = _mapper.Map<TrainingExercisesWithSetsDto>(trainingExercises);

            return new Response<TrainingExercisesWithSetsDto>(trainingExercisesDto);
        }
    }
}
