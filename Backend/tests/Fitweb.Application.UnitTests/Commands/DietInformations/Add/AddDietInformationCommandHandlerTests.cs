using AutoMapper;
using Fitweb.Application.Commands.DietInformations.Add;
using Fitweb.Application.Mapping;
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

namespace Fitweb.Application.UnitTests.Commands.DietInformations.Add
{
    public class AddDietInformationCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;
        private readonly AddDietInformationCommandHandler _sut;

        public AddDietInformationCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            var configurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(DietInformationProfile).Assembly);
            });
            _mapper = configurationProvider.CreateMapper();
            _sut = new AddDietInformationCommandHandler(_athleteRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldAddNewDietInformation()
        {
            var athlete = AthleteBuilder.Build();
            _athleteRepository.GetDietInformations(Arg.Any<string>()).Returns(athlete);

            var response = await _sut.Handle(new AddDietInformationCommand
            {
                UserId = AthleteBuilder.DefaultUserId,
                TotalCalories = 3000,
                TotalProteins = 150,
                TotalCarbohydrates = 250,
                TotalFats = 80,
                StartDate = new DateTime(2020, 5, 10),
                EndDate = null
            });

            response.Message.Should().Be("Diet information added successfully.");
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is(athlete));
        }
    }
}
