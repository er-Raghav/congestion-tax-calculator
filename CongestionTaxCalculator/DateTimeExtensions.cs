﻿using System;
using System.Linq;

internal static class DateTimeExtensions
{
    private static readonly DateTime[] _holidays = new DateTime[]
    {
        // January
        DateTime.Parse("2013-01-01"),

        // March
        DateTime.Parse("2013-03-28"),
        DateTime.Parse("2013-03-29"),

        // April
        DateTime.Parse("2013-04-01"),
        DateTime.Parse("2013-04-30"),

        // May
        DateTime.Parse("2013-05-01"),
        DateTime.Parse("2013-05-08"),
        DateTime.Parse("2013-05-09"),

        // June
        DateTime.Parse("2013-06-05"),
        DateTime.Parse("2013-06-06"),
        DateTime.Parse("2013-06-21"),

        // November
        DateTime.Parse("2013-11-01"),

        // December
        DateTime.Parse("2013-12-24"),
        DateTime.Parse("2013-12-25"),
        DateTime.Parse("2013-12-26"),
        DateTime.Parse("2013-12-31"),
    };

    public static bool IsHoliday(this DateTime date)
    {
        return _holidays.Any(holiday =>
            holiday.Month == date.Month &&
            holiday.Day == date.Day);
    }

    public static bool IsWeekend(this DateTime date)
    {
        return 
            date.DayOfWeek == DayOfWeek.Saturday || 
            date.DayOfWeek == DayOfWeek.Sunday;
    }
}