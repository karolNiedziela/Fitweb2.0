using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Update
{
    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public UpdateTrainingCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Response<string>> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken = default)
        {
            var training = await _trainingRepository.GetByIdAsync(request.TrainingId);
            if (training is null) 
            {
                throw new NotFoundException(nameof(Training), request.TrainingId);
            }
            training.Update(new Training(Information.Create(request.Name, request.Description), request.Day, request.Date));

            await _trainingRepository.UpdateAsync(training);

            return Response.Updated(nameof(Training));
        }
    }
}
