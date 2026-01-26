using System.Globalization;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CalendarCultureInfo
{
    public int CurrentYear { get; set; }
    public int CurrentMonth { get; set; }
    public int CurrentDecade { get; set; } 
    public List<string> DayNames { get; set; } = new();
    public CultureInfo  CurrentCulture { get; set; } = CultureInfo.InvariantCulture;
    public List<CalendarDaysOfWeek> DaysOfWeek { get; set; } = new();
    public List<CalendarDay> Days { get; set; } = new();
    public List<CalendarYear> Years { get; set; } = new();
    public List<CalendarMonth> Months { get; set; } = new();

    public ActiveViewType ActiveViewType { get; set; } = ActiveViewType.Month;
    
    public void ChangeMonth(int step)
    {
        CurrentMonth += step;
        if (CurrentMonth == 0)
        {
            CurrentYear -= 1;
            CurrentMonth = 12;
        }

        if (CurrentMonth == 13)
        {
            CurrentMonth = 1;
            CurrentYear += 1;
        }
    }

    public string GetCurrentMonthName()
    {
        return Months[CurrentMonth - 1].Name;
    }
    public IReadOnlyList<CalendarData> CalendarData
        => ActiveViewType switch
        {
            ActiveViewType.Month  => Days,
            ActiveViewType.Year   => Months,
            ActiveViewType.Decade => Years,
            _ => Array.Empty<CalendarData>()
        };

    public static CalendarCultureInfo CreateCalendarCultureInfo(DateTime displayDate,
        CultureInfo culture)
    {
        var calendar = new CalendarCultureInfo();
        calendar.CurrentYear = displayDate.Year;
        calendar.CurrentMonth = displayDate.Month;
        calendar.CurrentDecade = displayDate.Year / 10;
        calendar.CurrentCulture = culture;
        calendar.GetMonthViewDays();
        calendar.GetDaysOfWeek();
        calendar.GetMonthViewMonths();
        calendar.GetDecadeViewYears();
        return calendar;
    }


}