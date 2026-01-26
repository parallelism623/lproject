namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CalendarYear : CalendarData
{
    public int Year { get; set; }
    public bool IsOutOfDecade { get; set; }
    public override string GetCellLabel()
    {
        return Year.ToString();
    }

    public override string GetCellData()
    {
        return Year.ToString();
    }
}