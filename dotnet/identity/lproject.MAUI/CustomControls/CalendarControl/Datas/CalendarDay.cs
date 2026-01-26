using System;
using System.Globalization;

namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CalendarDay : CalendarData
{
    public DateTime Date { get; set; }
    public bool IsToday { get; set; }
    public bool IsOutOfMonth { get; set; }
    public bool IsSelected { get; set; }
    public bool IsStartedDate { get; set; }
    public CultureInfo? CultureInfo { get; set; }
    public bool IsEndDate { get; set; }
    public override string GetCellLabel()
    {
        return Date.Day.ToString(); 
    }
    

    public override string GetCellData()
    {
        return CalendarHelper.DateToString(Date, CultureInfo);
    }
}