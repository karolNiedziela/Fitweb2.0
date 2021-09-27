﻿using Fitweb.Domain.Common;
using Fitweb.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Athletes
{
    public class DietInformation : Entity
    {
        public double TotalCalories { get; private set; }

        public double TotalProteins { get; private set; }

        public double TotalCarbohydrates { get; private set; }

        public double TotalFats { get; private set; }

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public bool IsCurrent
        {
            get
            {
                return DateTime.UtcNow.IsInRange(StartDate, EndDate);
            }
        }

        public Athlete Athlete { get; set; }

        public int AthleteId { get; set; }

        protected DietInformation()
        {

        }

        public DietInformation(double totalCalories, double totalProteins, double totalCarbohydrates, double totalFats,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            SetTotalCalories(totalCalories);
            SetTotalProteins(totalProteins);
            SetTotalCarbohydrates(totalCarbohydrates);
            SetTotalFats(totalFats);
            SetPeriodTime(startDate, endDate);
        }

        public void SetTotalCalories(double totalCalories)
            => TotalCalories = DomainValidator.AgainstNegativeAndZeroNumber(totalCalories, "Total calories");

        public void SetTotalProteins(double totalProteins)
            => TotalProteins = DomainValidator.AgainstNegativeAndZeroNumber(totalProteins, "Total proteins");

        public void SetTotalCarbohydrates(double totalCarbohydrates)
            => TotalCarbohydrates = DomainValidator.AgainstNegativeAndZeroNumber(totalCarbohydrates, "Total carbohydrates");

        public void SetTotalFats(double totalFats)
            => TotalFats = DomainValidator.AgainstNegativeAndZeroNumber(totalFats, "Total fats");

        public void SetPeriodTime(DateTime? startDate, DateTime? endDate)
        {
            DomainValidator.AgainstImproperPeriod(startDate, endDate);

            StartDate = startDate;
            EndDate = endDate;
        }

        public void Update(double totalCalories, double totalProteins, double totalCarbohydrates, double totalFats,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            SetTotalCalories(totalCalories);
            SetTotalProteins(totalProteins);
            SetTotalCarbohydrates(totalCarbohydrates);
            SetTotalFats(totalFats);
            SetPeriodTime(startDate, endDate);
        }
    }
}
