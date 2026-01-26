namespace lproject.MAUI.CustomControls.DatePickerControl;

public class CalendarMonth : CalendarData
{
    public string Name { get; set; }
    public string AbbreviatedName  { get; set; }
    public int Month { get; set; }
    public override string GetCellLabel()
    {
        return AbbreviatedName;
    }

    public override string GetCellData()
    {
        return Month.ToString();
    }
}