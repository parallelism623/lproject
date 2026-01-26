namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CalendarGridLayout
{
    public int Column { get; set; } = 7;
    public int Row { get; set; } = 6;
    public int CellHeight { get; set; } = 44;
    public int CellWidth { get; set; }
    public int HeaderHeight { get; set; } = 80;
    public int DaysOfWeekHeight { get; set; }= 48;

    public static CalendarGridLayout GetGridLayoutByViewType(ActiveViewType  viewType)
    {
        var result = new CalendarGridLayout();
        if (viewType is not ActiveViewType.Month)
        {
            result.Row = 4;
            result.Column = 3;
            result.DaysOfWeekHeight = 0;
        }

        return result;
    }
}