using Fitweb.Application.Requests;
using MediatR;

namespace Fitweb.Application.Commands.Trainings.Delete
{
    public class DeleteTrainingCommand : AuthorizeRequest, IRequest
    {
        public int Id { get; set; }
    }
}
