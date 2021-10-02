using AutoMapper;
using Fitweb.Application.Commands.DietInformations.Update;
using Fitweb.Application.Mapping;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.DietInformations.Update
{
    public class UpdateDietInformationCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;
        private readonly UpdateDietInformationCommandHandler _sut;

        public UpdateDietInformationCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _mapper = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(DietInformationProfile).Assembly);
            }).CreateMapper();
            _sut = new UpdateDietInformationCommandHandler(_athleteRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldUpdateDietInformation()
        {
            var athlete = AthleteBuilder.Build();
            var dietInformation = new DietInformation(1500, 50, 100, 30, new DateTime(2020, 3, 5),
                new DateTime(2020, 10, 15))
            {
                Id = 1
            };
            athlete.AddDietInformation(dietInformation);

            _athleteRepository.GetDietInformations(Arg.Any<string>()).Returns(athlete);

            var response = await _sut.Handle(new UpdateDietInformationCommand
            {
                DietInformationId = 1,
                TotalCalories = 1500,
                TotalProteins = 50,
                TotalCarbohydrates = 70,
                TotalFats = 35,
                StartDate = dietInformation.StartDate,
                EndDate = new DateTime(2021, 1, 10)
            });

            response.Message.Should().Be("Diet information updated successfully.");
            await _athleteRepository.Received(1).UpdateAsync(athlete);
            athlete.DietInformations[0].TotalCarbohydrates.Should().Be(70);
        }
    }
}
