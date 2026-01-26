namespace lproject.MAUI.CustomControls.DatePickerControl.Appearances;

public class CalendarHeaderAppearance : AppearanceBase
{
    public Color? SelectedTextColor { get; set; }
    public Color? DisabledTextColor { get; set; }
    public Color? SecondaryTextColor { get; set; }

    public Color? BackgroundColor { get; set; }
    public static CalendarHeaderAppearance CreateDefaultValue()
    {
        return new();
    }
}
