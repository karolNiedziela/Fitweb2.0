using AutoMapper;
using Fitweb.Application.Commands.Trainings.Add;
using Fitweb.Application.Mapping;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.Trainings.Add
{
    public class AddTrainingCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;
        private readonly AddTrainingCommandHandler _sut;

        public AddTrainingCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            var configurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(TrainingProfile).Assembly);
            });
            _mapper = configurationProvider.CreateMapper();

            _sut = new AddTrainingCommandHandler(_athleteRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldAddTrainingToAthlete_WhenAthleteExists()
        {
            var athlete = new Athlete("testUserId");

            _athleteRepository.GetByUserId(Arg.Any<string>()).Returns(athlete);

            var command = new AddTrainingCommand
            {
                UserId = "testUserId",
                Name = "Test training",
                DayId = (int)Day.Sunday
            };

            var result = await _sut.Handle(command);

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Training added successfully.");
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is<Athlete>(x =>
            x.UserId == "testUserId" &&
            x.Trainings.FirstOrDefault().Information.Name == "Test training" &&
            x.Trainings.FirstOrDefault().Information.Description == null));
            athlete.Trainings.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAthleteDoesNotExist()
        {
            _athleteRepository.GetByUserId(Arg.Any<string>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new AddTrainingCommand
            {
                UserId = "testUserId"
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Athlete with user id: 'testUserId' was not found.");
        }
    }
}
