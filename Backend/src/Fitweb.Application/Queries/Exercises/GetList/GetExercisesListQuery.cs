using Fitweb.Application.DTO;
using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exercises;
using MediatR;

namespace Fitweb.Application.Queries.Exercises.GetList
{
    public class GetExercisesListQuery : IRequest<PagedResponse<ExerciseDto>>
    {
        public PaginationQuery Pagination { get; set; }

        public string SearchName { get; set; }

        public PartOfBody? PartOfBody { get; set; } 

    }
}
