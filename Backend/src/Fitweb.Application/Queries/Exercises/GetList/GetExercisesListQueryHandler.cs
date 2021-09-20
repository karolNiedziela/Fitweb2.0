using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Helpers;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Domain.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.Exercises.GetList
{
    public class GetExercisesListQueryHandler : IRequestHandler<GetExercisesListQuery, PagedResponse<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetExercisesListQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ExerciseDto>> Handle(GetExercisesListQuery request, CancellationToken cancellationToken)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(request.Pagination);

            var (exercises, totalItems) = await _exerciseRepository.GetAll(paginationFilter);

            var exercisesDto = _mapper.Map<List<ExerciseDto>>(exercises);

            return PaginationHelper.CreatePagedResponse(exercisesDto, paginationFilter, totalItems);
        }
    }
}
