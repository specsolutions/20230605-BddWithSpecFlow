﻿using System;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    [Binding]
    public class Conversions
    {
        // DATE

        [StepArgumentTransformation("today")]
        public DateTime ConvertToday()
        {
            return DateTime.Today;
        }

        [StepArgumentTransformation("tomorrow")]
        public DateTime ConvertTomorrow()
        {
            return DateTime.Today.AddDays(1);
        }

        [StepArgumentTransformation("(.*) days later")]
        public DateTime ConvertDaysLater(int days)
        {
            return DateTime.Today.AddDays(days);
        }

        // TIME

        [StepArgumentTransformation(@"(\d+):(\d+)")]
        public TimeSpan ConvertTimeSpan(int hours, int minutes)
        {
            return new TimeSpan(hours, minutes, 0);
        }

        [StepArgumentTransformation("noon")]
        public TimeSpan ConvertNoon()
        {
            return TimeSpan.FromHours(12);
        }

        [StepArgumentTransformation(@"(\d+)(am|pm)")]
        public TimeSpan ConvertTimeSpanAmPm(int hours, string ampm)
        {
            if (ampm == "pm" && hours < 12) hours = hours + 12;
            if (ampm == "am" && hours == 12) hours = hours - 12;
            return new TimeSpan(hours, 0, 0);
        }

        [StepArgumentTransformation(@"(?!earliest)(.*)")]
        public TimeSpan? ConvertTimeToOptional(TimeSpan timeSpan)
        {
            return timeSpan;
        }

        [StepArgumentTransformation(@"earliest")]
        public TimeSpan? ConvertDefaultTime()
        {
            return null;
        }
    }
}
