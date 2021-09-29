using Fitweb.Domain.Athletes;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Athletes
{
    public class AthleteTests
    {
        [Theory]
        [InlineData("123asd", 198, 95, 2)]
        [InlineData("123456778", 180, 80, 4)]
        public void CallingAthleteConstructor_ShouldCreateNewAthlete_WhenParametersAreValid(string userId, int height,
            int weight, int numberOfTrainings)
        {
            var athlete = new Athlete(userId, height, weight, numberOfTrainings);

            athlete.UserId.Should().Be(userId);
            athlete.Height.Should().Be(height);
            athlete.Weight.Should().Be(weight);
            athlete.NumberOfTrainings.Should().Be(numberOfTrainings);
            athlete.Trainings.Should().BeEmpty();
        }

        [Fact]
        public void CallingAthleteConstructor_ShouldFail_WhenUserIdIsEmpty()
        {
            var userId = string.Empty;

            var exception = Record.Exception(() =>  new Athlete(userId));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("UserId cannot be null or empty.");
        }

        [Fact]
        public void CallingAthleteConstructor_ShouldFail_WhenHeightIsNegative()
        {
            var height = -150;

            var exception = Record.Exception(() => new Athlete("userId", height));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Height cannot be negative.");
        }

        [Fact]
        public void CallingAthleteConstructor_ShouldFail_WhenWeightIsNegative()
        {
            var weight = -10;

            var exception = Record.Exception(() => new Athlete("userId", weight: weight));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Weight cannot be negative.");
        }

        [Fact]
        public void CallingAthleteConstructor_ShouldFail_WhenNumberOfTrainingsIsNegative()
        {
            var numberOfTrainings = -3;

            var exception = Record.Exception(() => new Athlete("userId", numberOfTrainings: numberOfTrainings));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Number of trainings cannot be negative.");
        }

        [Fact]
        public void AddingTraining_ShouldAddTrainingToAthlete()
        {
            var athlete = new Athlete("userId");

            athlete.AddTraining(new Training(Information.Create("Test", "Test"), Day.Monday));
            athlete.AddTraining(new Training(Information.Create("Test", "Test"), Day.Monday));

            athlete.Trainings.Count.Should().Be(2);
        }

        [Fact]
        public void RemoveTraining_ShouldRemoveTraining()
        {
            var athlete = new Athlete("userId");
            var training1 = new Training(Information.Create("Test", "Test"), Day.Monday)
            {
                Id = 1
            };
            var training2 = new Training(Information.Create("Test", "Test"), Day.Monday)
            {
                Id = 2
            };

            athlete.AddTraining(training1);
            athlete.AddTraining(training2);

            athlete.RemoveTraining(training1.Id);

            athlete.Trainings.Count.Should().Be(1);
        }

        [Fact]
        public void RemoveTraining_ShouldThrowException_WhenTrainingDoesNotExist()
        {
            var athlete = new Athlete("userId");
            var training1 = new Training(Information.Create("Test", "Test"), Day.Monday)
            {
                Id = 1
            };
            var training2 = new Training(Information.Create("Test2", "Test2"), Day.Monday)
            {
                Id = 2
            };

            athlete.AddTraining(training1);
            athlete.AddTraining(training2);

            var exception = Record.Exception(() => athlete.RemoveTraining(10));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Training with id: '{10}' was not found.");
        }

        [Fact]
        public void AddDietInformation_ShouldAddDietInformation_WhenDietInformationDoesInGivenPeriod()
        {
            var athlete = new Athlete("userId");
            var dietInformation = new DietInformation(1500, 80, 100, 30);

            athlete.AddDietInformation(dietInformation);

            athlete.DietInformations.Count.Should().Be(1);
        }

        // FROM1    FROM2   TO1     TO2
        // (TO2 = EMPTY  OR FROM <= TO2) AND
        // (TO1 = EMPTY OR TO1 >= FROM2)
        [Theory]
        [InlineData("2019-10-12", "2020-05-10", "", "")]
        [InlineData("2019-10-12", "2019-10-15", "2019-10-15", "")]  
        [InlineData("2019-10-12", "2019-10-15", "2019-11-01", "2019-10-27")]
        [InlineData("2019-10-12", "2019-10-12", "", "")] 
        [InlineData("2019-10-12", "2019-09-09", "2019-10-20", "2019-11-11")] 
        [InlineData("", "", "", "")]
        public void AddDietInformation_ShouldThrowException_WhenExistsDietInformationWithBeginningDateAndEndDateEqualToNull(
          string start, string start2, string end, string end2)
        {
            DateTime? startDate = string.IsNullOrEmpty(start) ? null : DateTime.Parse(start);
            DateTime? startDate2 = string.IsNullOrEmpty(start2) ? null : DateTime.Parse(start2);
            DateTime? endDate = string.IsNullOrEmpty(end) ? null : DateTime.Parse(end);
            DateTime? endDate2 = string.IsNullOrEmpty(end2) ? null : DateTime.Parse(end2);

            var athlete = new Athlete("userId");
            var dietInformation = new DietInformation(1500, 80, 100, 30, startDate, endDate);
            athlete.AddDietInformation(dietInformation);

            var existingDietInformation = new DietInformation(2000, 100, 150, 40, startDate2, endDate2);

            var exception = Record.Exception(() => athlete.AddDietInformation(existingDietInformation));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<AlreadyExistsException>();
            exception.Message.Should().Be("Diet information already exists for the given time period."); 
        }

        [Fact]
        public void RemoveDietInformation_ShouldRemoveDietInformation_WhenDietInformationExists()
        {
            var athlete = new Athlete("userId");
            var dietInformation = new DietInformation(1500, 80, 100, 30, null, null)
            {
                Id = 1
            };

            athlete.AddDietInformation(dietInformation);
            var countBeforeRemoving = athlete.DietInformations.Count;

            var toRemove = athlete.RemoveDietInformation(dietInformation.Id);

            countBeforeRemoving.Should().Be(1);
            athlete.DietInformations.Count.Should().Be(0);
            toRemove.Id.Should().Be(1);
        }

        [Fact]
        public void RemoveDietInformation_ShouldThrowException_WhenDietInformationDoesNotExist()
        {
            var athlete = new Athlete("userId");

            var exception = Record.Exception(() => athlete.RemoveDietInformation(1));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Diet information with id: '1' was not found.");
        }

        [Fact]
        public void UpdateDietInformation_ShouldUpdateCurrentDietInformation_WhenDietInformationFromGivenPeriodOfTimeDoesNotExist()
        {
            var athlete = new Athlete("userId");
            var dietInformation = new DietInformation(1500, 80, 100, 30, new DateTime(2020, 10, 10),
                new DateTime(2021, 2, 10))
            {
                Id = 1
            };
            athlete.AddDietInformation(dietInformation);

            athlete.UpdateDietInformation(new DietInformation(2000, 85, 150, 30, new DateTime(2020, 10, 10),
                new DateTime(2021, 2, 10))
            {
                Id = 1
            });

            athlete.DietInformations.FirstOrDefault().TotalCalories.Should().Be(2000);
            athlete.DietInformations.FirstOrDefault().TotalProteins.Should().Be(85);
            athlete.DietInformations.FirstOrDefault().TotalCarbohydrates.Should().Be(150);
            athlete.DietInformations.FirstOrDefault().TotalFats.Should().Be(30);
        }

        [Fact]
        public void Update_ShouldThrowException_WhenDietInformationFromGivenPeriodTimeExists()
        {
            var athlete = new Athlete("userId");
            var dietInformation = new DietInformation(1500, 80, 100, 30, new DateTime(2020, 10, 10),
                new DateTime(2021, 2, 10))
            {
                Id = 1
            };

            var dietInformation2 = new DietInformation(2500, 150, 200, 50, new DateTime(2005, 5, 10),
                new DateTime(2007, 10, 10))
            {
                Id = 2
            };

            athlete.AddDietInformation(dietInformation);
            athlete.AddDietInformation(dietInformation2);

            var exception = Record.Exception(() => athlete.UpdateDietInformation(new DietInformation(2000, 85, 150, 30, new DateTime(2006, 2, 10),
            new DateTime(2006, 3, 10))
            {
                Id = 1
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<AlreadyExistsException>();
            exception.Message.Should().Be("Diet information already exists for the given time period.");
        }

        [Fact]
        public void Update_ShouldThrowException_WhenDietInformationDoesNotExist()
        {
            var athlete = new Athlete("userId");

            var exception = Record.Exception(() => athlete.UpdateDietInformation(new DietInformation(2000, 85, 150, 30, new DateTime(2006, 2, 10),
            new DateTime(2006, 3, 10))
            {
                Id = 1
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Diet information with id: '1' was not found.");
        }

    }
}
