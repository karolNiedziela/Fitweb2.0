using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Helpers;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.Trainings.GetList
{
    public class GetTrainingsListQueryHandler : IRequestHandler<GetTrainingsListQuery, PagedResponse<TrainingDto>>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public GetTrainingsListQueryHandler(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TrainingDto>> Handle(GetTrainingsListQuery request, CancellationToken cancellationToken)
        {
            var athleteId = await _athleteRepository.GetAthleteId(request.UserId);
            var paginationFilter = _mapper.Map<PaginationFilter>(request.Pagination);

            var (trainings, totalItems) = await _athleteRepository.GetPagedTrainings(athleteId, paginationFilter);

            var trainingsDto = _mapper.Map<List<TrainingDto>>(trainings);

            return PaginationHelper.CreatePagedResponse(trainingsDto, paginationFilter, totalItems);
        }
    }
}
