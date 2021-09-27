using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Add
{
    public class AddDietInformationCommandHandler : IRequestHandler<AddDietInformationCommand>
    {
        private readonly IAthleteRepository _athleteRepository;

        public AddDietInformationCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Unit> Handle(AddDietInformationCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetDietInformations(request.UserId);

            athlete.AddDietInformation(new DietInformation(request.TotalCalories, request.TotalProteins, request.TotalCarbohydrates,
                request.TotalFats, request.StartDate, request.EndDate));

            await _athleteRepository.UpdateAsync(athlete);

            return Unit.Value;
        }
    }
}
