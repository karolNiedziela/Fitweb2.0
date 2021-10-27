using Fitweb.Application.Commands.TrainingExercises.Delete;
using Fitweb.Application.Responses;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.TrainingExercises.Delete
{
    public class DeleteTrainingExerciseCommandHandlerTests
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly DeleteTrainingExerciseCommandHandler _sut;

        public DeleteTrainingExerciseCommandHandlerTests()
        {
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _sut = new DeleteTrainingExerciseCommandHandler(_trainingRepository);
        }

        [Fact]
        public async Task Handle_ShouldRemoveTrainingExercise()
        {
            var training = TrainingBuilder.BuildWithExercisesAndSets();

            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>()).Returns(training);

            var result = await _sut.Handle(new DeleteTrainingExerciseCommand
            {
                TrainingId = 1,
                ExerciseId = 1
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Training exercise removed successfully.");
            await _trainingRepository.Received(1).RemoveTrainingExercise(Arg.Is<TrainingExercise>(x => x.TrainingId == 1 &&
            x.ExerciseId == 1));
        }
    }
}
