using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Helpers;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Filters;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.Trainings.GetList
{
    public class GetTrainingsListQueryHandler : IRequestHandler<GetTrainingsListQuery, PagedResponse<TrainingDto>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingsListQueryHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TrainingDto>> Handle(GetTrainingsListQuery request, CancellationToken cancellationToken)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(request.Pagination);

            var (trainings, totalItems) = await _trainingRepository.GetPagedTrainings(request.UserId, paginationFilter,
                request.Date);

            var trainingsDto = _mapper.Map<List<TrainingDto>>(trainings);

            return PaginationHelper.CreatePagedResponse(trainingsDto, paginationFilter, totalItems);
        }
    }
}
