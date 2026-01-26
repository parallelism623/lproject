namespace lproject.MAUI.CustomControls.DatePickerControl.Appearances;

public class CalendarDayCellAppearance : AppearanceBase
{
    public Color? TodayTextColor { get; set; } = Color.FromRgb(94, 53, 177);
    public Color? DisabledTextColor { get; set; } = Colors.Gray;
    public Color? OutOfMonthTextColor { get; set; } = Color.FromRgb(189, 189, 189);
    public Color? SelectedTextColor { get; set; } = Colors.White;
    public Color? StartedTextColor { get; set; } = Colors.White;
    public Color? EndTextColor { get; set; } = Colors.White;

    public Color? SelectedBackgroundColor { get; set; } = Color.FromRgb(240, 98, 146);
    public Color? InRangeBackgroundColor { get; set; } = Colors.LightBlue;
    public Color? EndTextBackgroundColor { get; set; } = Colors.Blue;
    public Color? StartedTextBackgroundColor { get; set; } = Colors.Blue;
    public Color? TodayBackgroundColor { get; set; } = Color.FromRgb(237, 231, 246);

    public static CalendarDayCellAppearance CreateDefaultValue()
        => new();
    
}

