using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;

namespace Fitweb.Application.Commands.Trainings.Delete
{
    public class DeleteTrainingCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
