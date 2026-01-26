namespace lproject.MAUI.CustomControls.DatePickerControl.Appearances;

public class AppearanceBase : Element
{
    public Color TextColor { get; set; } = Colors.Black;
    public Color BackgroundColor { get; set; } = Colors.White;

    public double? FontSize { get; set; } = 16;
    public FontAttributes? FontAttributes { get; set; } = 0;
    public string FontFamily { get; set; } = null;

    public TextAlignment? HorizontalTextAlignment { get; set; }
        = TextAlignment.Center;

    public TextAlignment? VerticalTextAlignment { get; set; }
        = TextAlignment.Center;

    // Border
    public Color BorderColor { get; set; } = Colors.Transparent;
    public double? BorderThickness { get; set; } = 0;
    public double? CornerRadius { get; set; } = 0;

    // Layout
    public Thickness? Padding { get; set; } = Thickness.Zero;
    
}