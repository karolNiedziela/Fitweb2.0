using AutoMapper;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Fitweb.Application.Responses;
using Fitweb.Domain.ValueObjects;

namespace Fitweb.Application.Commands.Trainings.Add
{
    public class AddTrainingCommandHandler : IRequestHandler<AddTrainingCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public AddTrainingCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(AddTrainingCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);

            athlete.AddTraining(new Training(Information.Create(request.Name, request.Description), request.Day, request.Date));
            await _athleteRepository.UpdateAsync(athlete);

            return Response.Added(nameof(Training));
        }
    }
}
