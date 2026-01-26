using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public static class CalendarHelper
{
    
    
    public static void GetDaysOfWeek(this CalendarCultureInfo calendar)
    {
        calendar.DaysOfWeek = GetDaysOfWeekList(calendar.CurrentCulture);
    }
    
    public static void GetMonthViewDays(this CalendarCultureInfo calendarCultureInfo)
    {
        calendarCultureInfo.Days = GetMonthViewDays(calendarCultureInfo.CurrentMonth,  
            calendarCultureInfo.CurrentYear, CultureInfo.CurrentCulture);
    }
    public static List<CalendarDay> GetMonthViewDays(int month, int year, CultureInfo culture)
    {
        var firstOfMonth   = new DateTime(year, month, 1);
        var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

        var firstDate = firstOfMonth;

        while (firstDate.DayOfWeek != firstDayOfWeek)
        {
            firstDate = firstDate.AddDays(-1);
        }

        var result = new List<CalendarDay>(42);
       
        for (int i = 0; i < 42; i++)
        {
            var date = firstDate.AddDays(i);

            result.Add(new CalendarDay
            {
                Date = date,
                IsOutOfMonth = date.Month != month,
                IsToday = date.Date == DateTime.Today,
                IsSelected = false,
                CultureInfo =  culture
            });
        }

        return result;
    }
    

    public static List<CalendarDaysOfWeek> GetDaysOfWeekList(CultureInfo culture)
    {
        var dtf = culture.DateTimeFormat;
        var start = (int)dtf.FirstDayOfWeek;

        return Enumerable.Range(0, 7)
            .Select(i =>
            {
                var dow = (DayOfWeek)((start + i) % 7);

                return new CalendarDaysOfWeek
                {
                    Name = dtf.GetDayName(dow),
                    AbbreviatedName = dtf.GetAbbreviatedDayName(dow)
                };
            })
            .ToList();
    }

    public static void GetDecadeViewYears(this CalendarCultureInfo calendar)
    {
        calendar.Years = GetDecadeViewYears(calendar.CurrentDecade);
    }
    public static string DateToString(DateTime date, CultureInfo currentCulture, string? format = null)
    {
        format ??= currentCulture.DateTimeFormat.ShortDatePattern;

        return date.ToString(format, currentCulture);
    }
    
    public static bool TryParseDate(
        string text,
        CultureInfo currentCulture,
        out DateTime date,
        string? format = null)
    {
        format ??= currentCulture.DateTimeFormat.ShortDatePattern;
        
        if (DateTime.TryParseExact(
                text,
                format,
                currentCulture,
                DateTimeStyles.None,
                out date))
        {
            return true;
        }
        
        return DateTime.TryParse(
            text,
            currentCulture,
            DateTimeStyles.None,
            out date);
    }
    public static void GetMonthViewMonths(this CalendarCultureInfo calendar)
    {
        calendar.Months = GetMonthViewMonths(calendar.CurrentCulture);
    }
    public static List<CalendarMonth> GetMonthViewMonths(CultureInfo culture)
    {
        var dtf = culture.DateTimeFormat;

        var result = new List<CalendarMonth>(12);

        for (int month = 1; month <= 12; month++)
        {
            result.Add(new CalendarMonth
            {
                Month = month,
                Name = dtf.GetMonthName(month),
                AbbreviatedName = dtf.GetAbbreviatedMonthName(month)
            });
        }

        return result;
    }

    public static List<CalendarYear> GetDecadeViewYears(int currentDecade)
    {
        var decadeStart = currentDecade * 10;

        var result = new List<CalendarYear>(12);

        for (int i = 0; i < 12; i++)
        {
            var y = decadeStart + i;

            result.Add(new CalendarYear
            {
                Year = y,
                IsOutOfDecade = i >= 10 
            });
        }

        return result;
    }



}